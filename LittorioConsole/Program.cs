using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpClientShared.Utils;
using HttpClientShared.Models;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using Littorio.Models;

namespace LittorioConsole
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Uri url = new Uri("http://whois.kisa.or.kr/openapi/whois.jsp?query=t&key=t&answer=t");
            var values = new Dictionary<string, string> { { "query", "61.111.22.11" }, { "key", "2016122313485156001035" }, { "answer", "xml" } };
            // 아래의 표현이 가능한 이유는 아래의 ExtendQuery 저으이에 this Uri uri 라고 표기하였기 때문에 가능
            // 아래와 같은 표현을 사용할 경우 정의의 첫번째 Parameter 의 class 타입은 
            // public static Uri ExtendQuery(this Uri uri, IDictionary<string, string> values)
            var result = url.ExtendQuery(values);
            Debug.WriteLine(result);
            Debug.Indent();
            Debug.WriteLine(result);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(result);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // 응답 Stream 읽기
            Stream stReadData = response.GetResponseStream();
            // 인코딩으로 인해 JsonConvert 모듈에서 에러가 발생할 수 있음
            StreamReader srReadData = new StreamReader(stReadData, Encoding.UTF8);

            // 응답 Stream -> 응답 String 변환
            string strResult = srReadData.ReadToEnd();
            Console.WriteLine(strResult);
            XmlDocument doc = new XmlDocument();
            
            doc.LoadXml(strResult);
            Console.WriteLine(doc.OuterXml);
            XmlNode xmlNode = doc.SelectSingleNode("//user");
            Console.WriteLine(xmlNode.OuterXml);
            user myCustomer = ConvertNode<user>(xmlNode);
            Console.WriteLine(myCustomer.netInfo.orgID);
            //var response = GetWhois<WhoisRoot>(result);
            Console.ReadLine();
        }

        public static T ConvertNode<T>(XmlNode node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node.OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = (ser.Deserialize(stm) as T);
            

            return result;
        }

        public async static Task<T> GetWhois<T>(Uri url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            T model;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Json Object 로 Deserialization 을 수행한 상태에서 원하는 정보를 
                var result = await response.Content.ReadAsStringAsync();

                model = JsonConvert.DeserializeObject<T>(result);
                XmlDocument doc = JsonConvert.DeserializeXmlNode(result);
                Debug.WriteLine(doc.OuterXml);
                return model;
            }
            return default(T);
        }



        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class user
        {

            private userNetInfo netInfoField;

            private userTechContact techContactField;

            /// <remarks/>
            public userNetInfo netInfo
            {
                get
                {
                    return this.netInfoField;
                }
                set
                {
                    this.netInfoField = value;
                }
            }

            /// <remarks/>
            public userTechContact techContact
            {
                get
                {
                    return this.techContactField;
                }
                set
                {
                    this.techContactField = value;
                }
            }

            #region Property For DataGrid Controls
            public string IPRange
            {
                get
                {
                    return this.netInfo.range;
                }
                set
                {
                    this.netInfo.range = value;
                }
            }

            public string IPPrefix
            {
                get
                {
                    return this.netInfo.prefix;
                }
                set
                {
                    this.netInfo.prefix = value;
                }
            }

            public string Organization
            {
                get
                {
                    return this.netInfo.orgName;
                }
                set
                {
                    this.netInfo.orgName = value;
                }
            }

            public string OrgID
            {
                get
                {
                    return this.netInfo.orgID;
                }
                set
                {
                    this.netInfo.orgID = value;
                }
            }

            public string OrgAddr
            {
                get
                {
                    return this.netInfo.addr;
                }
                set
                {
                    this.netInfo.addr = value;
                }
            }

            public DateTime Date
            {
                get
                {
                    string date = this.netInfo.regDate.ToString();
                    return DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                set
                {
                    string date = value.ToString("yyyyMMdd");
                    this.netInfo.regDate = Convert.ToUInt32(date);

                }
            }

            public string ContactName
            {
                get
                {
                    return this.techContact.name;
                }
                set
                {
                    this.techContact.name = value;
                }
            }

            public string ContactPhone
            {
                get
                {
                    return this.techContact.phone;
                }
                set
                {
                    this.techContact.phone = value;
                }
            }

            public string ContactEmail
            {
                get
                {
                    return this.techContact.email;
                }
                set
                {
                    this.techContact.email = value;
                }
            }
            #endregion
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class userNetInfo
        {

            private string rangeField;

            private string prefixField;

            private string orgNameField;

            private string orgIDField;

            private string netTypeField;

            private string addrField;

            private ushort zipCodeField;

            private uint regDateField;

            /// <remarks/>
            public string range
            {
                get
                {
                    return this.rangeField;
                }
                set
                {
                    this.rangeField = value;
                }
            }

            /// <remarks/>
            public string prefix
            {
                get
                {
                    return this.prefixField;
                }
                set
                {
                    this.prefixField = value;
                }
            }

            /// <remarks/>
            public string orgName
            {
                get
                {
                    return this.orgNameField;
                }
                set
                {
                    this.orgNameField = value;
                }
            }

            /// <remarks/>
            public string orgID
            {
                get
                {
                    return this.orgIDField;
                }
                set
                {
                    this.orgIDField = value;
                }
            }

            /// <remarks/>
            public string netType
            {
                get
                {
                    return this.netTypeField;
                }
                set
                {
                    this.netTypeField = value;
                }
            }

            /// <remarks/>
            public string addr
            {
                get
                {
                    return this.addrField;
                }
                set
                {
                    this.addrField = value;
                }
            }

            /// <remarks/>
            public ushort zipCode
            {
                get
                {
                    return this.zipCodeField;
                }
                set
                {
                    this.zipCodeField = value;
                }
            }

            /// <remarks/>
            public uint regDate
            {
                get
                {
                    return this.regDateField;
                }
                set
                {
                    this.regDateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class userTechContact
        {

            private string nameField;

            private string phoneField;

            private string emailField;

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string phone
            {
                get
                {
                    return this.phoneField;
                }
                set
                {
                    this.phoneField = value;
                }
            }

            /// <remarks/>
            public string email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }
        }


    }
}
