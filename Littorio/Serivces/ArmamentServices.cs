using HttpClientShared.Utils;
using Littorio.Async;
using Littorio.Models;
using Littorio.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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

        public static async Task<int> QueryAndConvertToXmlAsyncSemaphore(string query, AsyncObservableCollection<WhoisAPNIC> Whoises, SemaphoreSlim sem, CancellationToken token = new CancellationToken())
        {
            Dictionary<string, string> dicIRT = new Dictionary<string, string> {
            { "inetnum", "INETNUM" },
            { "irt", "IRT" },
            { "address","ADDR" },
            { "country","COUNTRY" },
            {"phone","PHONE" },
            {"e-mail","EMAIL"},
            {"source","SRC" },
        };
            Uri url = new Uri("https://wq.apnic.net/whois-search/query?searchtext=125.209.222.141");
            var values = new Dictionary<string, string> { { "searchtext", query } };
            var query_url = url.ExtendQuery(values);
            // 시간 딜레이를 적용
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            // 세마포어를 이용해서 동시 접속 횟수 제한 적용
            await sem.WaitAsync();
            var client = new HttpClient();
            using (var response = await client.GetAsync(query_url, token).ConfigureAwait(false))
            {
                string stReadData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                // 아래의 코드부터 좀 지저분해지는 경향이 있음.... 
                // json 파일을 받아서 검색하는 방식으로 구성요소를 구축한 다음에 이를 Collection 에 등록함으로써 간편화함.
                JArray records = JArray.Parse(stReadData);
                WhoisAPNIC apnic = new WhoisAPNIC(query);
                foreach (JObject record in records)
                {

                    if (record.SelectToken("type").ToString() == "object")
                    {
                        if (record["objectType"].ToString() == "inetnum")
                        {
                            // Reflection 을 이용해서 클래스를 초기화 할 것
                            foreach (JObject obj in record["attributes"])
                            {
                                if (dicIRT.ContainsKey(obj["name"].ToString()))
                                {
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                }
                            }
                        }
                        else if (record["objectType"].ToString() == "irt")
                        {
                            // Reflection 을 이용해서 클래스를 초기화 할 것
                            foreach (JObject obj in record["attributes"])
                            {
                                if (dicIRT.ContainsKey(obj["name"].ToString()))
                                {
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                }
                            }
                        }
                        else if (record["objectType"].ToString() == "person")
                        {
                            foreach (JObject obj in record["attributes"])
                            {
                                if (dicIRT.ContainsKey(obj["name"].ToString()))
                                {
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                }
                            }
                        }

                    }
                }
                Whoises.Add(apnic);
                // 작업 종료 후 잡았던 Semaphore 를 릴리즈
                sem.Release();
                /*
                StreamReader srReadData = new StreamReader(stReadData, Encoding.UTF8);
                string data = srReadData.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode xmlNode = doc.SelectSingleNode("//user");
                Console.WriteLine(xmlNode.OuterXml);
                user WhoisUser = Utils.XmlUtils.ConvertNode<user>(xmlNode);
                Whoises.Add(WhoisUser);
                */
                return stReadData.Length;
            }
        }

        public static async Task<int> QueryAndConvertToXmlAsync(string query, AsyncObservableCollection<WhoisAPNIC> Whoises, CancellationToken token = new CancellationToken())
        {
            Dictionary<string, string> dicIRT = new Dictionary<string, string> {
            { "inetnum", "INETNUM" },
            { "irt", "IRT" },
            { "address","ADDR" },
            { "country","COUNTRY" },
            {"phone","PHONE" },
            {"e-mail","EMAIL"},
            {"source","SRC" },
        };
            Uri url = new Uri("https://wq.apnic.net/whois-search/query?searchtext=125.209.222.141");
            var values = new Dictionary<string, string> { { "searchtext", query }};
            var query_url = url.ExtendQuery(values);
            await Task.Delay(TimeSpan.FromSeconds(3), token).ConfigureAwait(false);
            var client = new HttpClient();
            using (var response = await client.GetAsync(query_url, token).ConfigureAwait(false))
            {
                string stReadData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                // 아래의 코드부터 좀 지저분해지는 경향이 있음.... 
                // json 파일을 받아서 검색하는 방식으로 구성요소를 구축한 다음에 이를 Collection 에 등록함으로써 간편화함.
                JArray records = JArray.Parse(stReadData);
                WhoisAPNIC apnic = new WhoisAPNIC(query);
                foreach (JObject record in records)
                {
                    
                    if ( record.SelectToken("type").ToString() == "object")
                    {
                        if (record["objectType"].ToString() == "inetnum" )
                        {
                            // Reflection 을 이용해서 클래스를 초기화 할 것
                            foreach (JObject obj in record["attributes"])
                            {
                                if (dicIRT.ContainsKey(obj["name"].ToString()))
                                {
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                }
                            }
                        }
                        else if ( record["objectType"].ToString() == "irt")
                        {
                            // Reflection 을 이용해서 클래스를 초기화 할 것
                            foreach(JObject obj in record["attributes"])
                            {
                                if ( dicIRT.ContainsKey( obj["name"].ToString() )){
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                } 
                            }
                        }else if(record["objectType"].ToString() == "person")
                        {
                            foreach (JObject obj in record["attributes"])
                            {
                                if (dicIRT.ContainsKey(obj["name"].ToString()))
                                {
                                    PropertyInfo prop = apnic.GetType().GetProperty(dicIRT[obj["name"].ToString()], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        prop.SetValue(apnic, string.Join(",", obj["values"]), null);
                                    }
                                }
                            }
                        }

                    }
                }
                Whoises.Add(apnic);
                /*
                StreamReader srReadData = new StreamReader(stReadData, Encoding.UTF8);
                string data = srReadData.ReadToEnd();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode xmlNode = doc.SelectSingleNode("//user");
                Console.WriteLine(xmlNode.OuterXml);
                user WhoisUser = Utils.XmlUtils.ConvertNode<user>(xmlNode);
                Whoises.Add(WhoisUser);
                */
                return stReadData.Length;
            }
        }

    }
}
