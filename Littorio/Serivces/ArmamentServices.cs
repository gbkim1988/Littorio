using HttpClientShared.Utils;
using Littorio.Async;
using Littorio.Models;
using Littorio.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Littorio.Serivces
{
    public static class ArmamentServices
    {
        // http://blogs.msdn.com/b/lucian/archive/2012/12/08/await-httpclient-getstringasync-and-cancellation.aspx
        public static async Task<int> DownloadAndCountBytesAsync(string url, CancellationToken token = new CancellationToken())
        {
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            var client = new HttpClient();
            using (var response = await client.GetAsync(url, token).ConfigureAwait(false))
            {
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return data.Length;
            }
        }

        public static async Task<int> QueryAndConvertToXmlAsync(string query, AsyncObservableCollection<user> Whoises, CancellationToken token = new CancellationToken())
        {
            Uri url = new Uri("http://whois.kisa.or.kr/openapi/whois.jsp?query=t&key=t&answer=t");
            var values = new Dictionary<string, string> { { "query", query }, { "key", "2016122313485156001035" }, { "answer", "xml" } };
            var query_url = url.ExtendQuery(values);
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            var client = new HttpClient();
            using (var response = await client.GetAsync(query_url, token).ConfigureAwait(false))
            {
                var stReadData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                StreamReader srReadData = new StreamReader(stReadData, Encoding.UTF8);
                string data = srReadData.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode xmlNode = doc.SelectSingleNode("//user");
                Console.WriteLine(xmlNode.OuterXml);
                user WhoisUser = Utils.XmlUtils.ConvertNode<user>(xmlNode);
                Whoises.Add(WhoisUser);
                return data.Length;
            }
        }

    }
}
