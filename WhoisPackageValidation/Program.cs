using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whois.NET;
using WhoisPackageValidation;

namespace WhoisPackageValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = "[{\"type\":\"comments\",\"comments\":[\" % APNIC found the following authoritative answer from: whois.apnic.net\"]},{\"type\":\"comments\",\"comments\":[\" % [whois.apnic.net]\",\" % Whois data copyright terms http://www.apnic.net/db/dbcopyright.html\"]},{\"type\":\"comments\",\"comments\":[\"% Information related to '61.16.0.0 - 61.16.127.255'\"]},{\"type\":\"object\",\"attributes\":[{\"name\":\"inetnum\",\"values\":[\"61.16.0.0 - 61.16.127.255\"]},{\"name\":\"netname\",\"values\":[\"MRNET\"]},{\"name\":\"descr\",\"values\":[\"MediaRing Communications Pte Ltd\"]},{\"name\":\"descr\",\"values\":[\"150 Kampong Ampat\"]},{\"name\":\"descr\",\"values\":[\"#05-02, KA Center\"]},{\"name\":\"descr\",\"values\":[\"Singapore 368324\"]},{\"name\":\"country\",\"values\":[\"SG\"]},{\"name\":\"admin-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"tech-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"notify\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"APNIC-HM\",\"url\":\"search.html?query=APNIC-HM\"}]},{\"name\":\"mnt-lower\",\"values\":[\"MAINT-SG-MRNET\"]},{\"name\":\"mnt-routes\",\"values\":[\"MAINT-SG-MRNET\"]},{\"name\":\"mnt-irt\",\"values\":[\"IRT-NETPLUS-SG\"]},{\"name\":\"status\",\"values\":[\"ALLOCATED PORTABLE\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20051101\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20110812\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"inetnum\",\"primaryKey\":\"61.16.0.0 - 61.16.127.255\"},{\"type\":\"object\",\"attributes\":[{\"name\":\"irt\",\"values\":[\"IRT-NETPLUS-SG\"]},{\"name\":\"address\",\"values\":[\"Mediaring Network Services Pte Ltd.\",\"150 Kampong Ampat\",\"Singapore 368324\"]},{\"name\":\"e-mail\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"abuse-mailbox\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"admin-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"tech-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"auth\",\"values\":[\"# Filtered\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"MAINT-SG-MRNET\",\"url\":\"search.html?query=MAINT-SG-MRNET\"}]},{\"name\":\"changed\",\"values\":[\"operations@spicei2i.com 20110506\"]},{\"name\":\"changed\",\"values\":[\"win-naing@spicei2i.com 20110810\"]},{\"name\":\"changed\",\"values\":[\"win-naing@s-i2i.com 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"irt\",\"primaryKey\":\"IRT-NETPLUS-SG\"},{\"type\":\"object\",\"attributes\":[{\"name\":\"person\",\"values\":[\"Mediaring Service Deployment\"]},{\"name\":\"address\",\"values\":[\"150 Kampong Ampat\",\"Singapore 368324\"]},{\"name\":\"country\",\"values\":[\"SG\"]},{\"name\":\"phone\",\"values\":[\"+65 65149458\"]},{\"name\":\"fax-no\",\"values\":[\"+65 64413013\"]},{\"name\":\"e-mail\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"nic-hdl\",\"values\":[\"MSD1-AP\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"MAINT-SG-MRNET\",\"url\":\"search.html?query=MAINT-SG-MRNET\"}]},{\"name\":\"changed\",\"values\":[\"win-naing@spicei2i.com 20110810\"]},{\"name\":\"changed\",\"values\":[\"win-naing@s-i2i.com 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"person\",\"primaryKey\":\"MSD1-AP\"},{\"type\":\"comments\",\"comments\":[\"% This query was served by the APNIC Whois Service version 1.69.1-APNICv1r7-SNAPSHOT (WHOIS3)\"]}]";

            JArray whois = JArray.Parse(result);
            //var preciseR = whois.Children<JProperty>().FirstOrDefault(x => x.Name == "ObjectType").Value;

            //JObject person = whois.Children<JObject>().SelectMany(x => x["@ObjectType"].Value<string>() == "person");
            //Console.WriteLine(person);
            //whois.SelectMany(x => x["*"]["objectType"].Value<string>)
            
            // JProperty 검색 을 통해 type 이 object 인 것을 우선 선택
            foreach (JObject item in whois)
            {
                string distance = item.SelectToken("type").ToString();
                //Console.WriteLine(distance);
                if ( distance == "object")
                {
                    string [] kk = item["attributes"].Select(m => (string)m.SelectToken("name")).Where(n => n != null).ToArray();
                    Console.WriteLine("(*)");
                    //foreach (string aa in kk) 
                    //Console.WriteLine(item["attributes"].Children<JObject>().ToString());
                    if ( item["objectType"].ToString() == "irt") { 
                        Console.WriteLine("** " + item["objectType"].ToString());
                        foreach ( JObject obj in item["attributes"])
                        {
                            try
                            {
                                Console.WriteLine(obj["name"]);
                                Console.WriteLine(string.Join(",", obj["values"]));
                            }
                            catch (System.ArgumentNullException)
                            {

                            }
                        
                        }
                    }

                    if (item["objectType"].ToString() == "person")
                    {
                        Console.WriteLine("** " + item["objectType"].ToString());
                        foreach (JObject obj in item["attributes"])
                        {

                            try
                            {
                                Console.WriteLine(obj["name"]);
                                Console.WriteLine(string.Join(",", obj["values"]));
                            }
                            catch (System.ArgumentNullException)
                            {

                            }

                        }
                    }
                    /*
                    foreach ( JToken token in item.SelectTokens("objectType"))
                    {
                        Console.WriteLine(token.Path + ": " + token);
                    }
                    */

                }
                /*
                 * string[] firstProductNames = o["Manufacturers"].Select(m => (string)m.SelectToken("Products[1].Name"))
                 * .Where(n => n != null).ToArray();
                 
                 */

                /*
                foreach (JToken token in item.FindTokens("text"))
                {
                    Console.WriteLine(token.Path + ": " + token.ToString());
                }
                var itemProperties = item.Children<JProperty>();
                foreach(JProperty pop in itemProperties)
                {
                    Console.WriteLine(pop);
                    Console.ReadLine();
                } 
                */
                //var itemProperties = item.Children<JProperty>();
                //you could do a foreach or a linq here depending on what you need to do exactly with the value
                //var myElement = itemProperties.FirstOrDefault(x => x.Name == "url");
                //var myElementValue = myElement.Value; ////This is a JValue type
                foreach (JToken token in item.FindTokens("ObjectType"))
                {
                    Console.WriteLine(token.Path + ": " + token.ToString());
                }
            }


            Console.ReadLine();

        }

    }

    public static class JsonExtensions
    {
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }

        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }
    }
}
