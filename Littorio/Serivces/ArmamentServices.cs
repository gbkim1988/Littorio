using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

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

        public static async Task<int> QueryAndConvertToXmlAsync(string url, ObservableCollection<User> users, CancellationToken token = new CancellationToken())
        {
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            var client = new HttpClient();
            using (var response = await client.GetAsync(url, token).ConfigureAwait(false))
            {
                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode 
                return data.Length;
            }
        }
    }
}
