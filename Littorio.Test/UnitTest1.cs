using System;
using System.Collections.Generic;
using NUnit.Framework;
using HttpClientShared.Utils;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Littorio.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void find_Certain_Jobject_from_Jarray()
        {
            var result = "[{\"type\":\"comments\",\"comments\":[\" % APNIC found the following authoritative answer from: whois.apnic.net\"]},{\"type\":\"comments\",\"comments\":[\" % [whois.apnic.net]\",\" % Whois data copyright terms http://www.apnic.net/db/dbcopyright.html\"]},{\"type\":\"comments\",\"comments\":[\"% Information related to '61.16.0.0 - 61.16.127.255'\"]},{\"type\":\"object\",\"attributes\":[{\"name\":\"inetnum\",\"values\":[\"61.16.0.0 - 61.16.127.255\"]},{\"name\":\"netname\",\"values\":[\"MRNET\"]},{\"name\":\"descr\",\"values\":[\"MediaRing Communications Pte Ltd\"]},{\"name\":\"descr\",\"values\":[\"150 Kampong Ampat\"]},{\"name\":\"descr\",\"values\":[\"#05-02, KA Center\"]},{\"name\":\"descr\",\"values\":[\"Singapore 368324\"]},{\"name\":\"country\",\"values\":[\"SG\"]},{\"name\":\"admin-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"tech-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"notify\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"APNIC-HM\",\"url\":\"search.html?query=APNIC-HM\"}]},{\"name\":\"mnt-lower\",\"values\":[\"MAINT-SG-MRNET\"]},{\"name\":\"mnt-routes\",\"values\":[\"MAINT-SG-MRNET\"]},{\"name\":\"mnt-irt\",\"values\":[\"IRT-NETPLUS-SG\"]},{\"name\":\"status\",\"values\":[\"ALLOCATED PORTABLE\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20051101\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20110812\"]},{\"name\":\"changed\",\"values\":[\"hm-changed@apnic.net 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"inetnum\",\"primaryKey\":\"61.16.0.0 - 61.16.127.255\"},{\"type\":\"object\",\"attributes\":[{\"name\":\"irt\",\"values\":[\"IRT-NETPLUS-SG\"]},{\"name\":\"address\",\"values\":[\"Mediaring Network Services Pte Ltd.\",\"150 Kampong Ampat\",\"Singapore 368324\"]},{\"name\":\"e-mail\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"abuse-mailbox\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"admin-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"tech-c\",\"links\":[{\"text\":\"MSD1-AP\",\"url\":\"search.html?query=MSD1-AP\"}]},{\"name\":\"auth\",\"values\":[\"# Filtered\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"MAINT-SG-MRNET\",\"url\":\"search.html?query=MAINT-SG-MRNET\"}]},{\"name\":\"changed\",\"values\":[\"operations@spicei2i.com 20110506\"]},{\"name\":\"changed\",\"values\":[\"win-naing@spicei2i.com 20110810\"]},{\"name\":\"changed\",\"values\":[\"win-naing@s-i2i.com 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"irt\",\"primaryKey\":\"IRT-NETPLUS-SG\"},{\"type\":\"object\",\"attributes\":[{\"name\":\"person\",\"values\":[\"Mediaring Service Deployment\"]},{\"name\":\"address\",\"values\":[\"150 Kampong Ampat\",\"Singapore 368324\"]},{\"name\":\"country\",\"values\":[\"SG\"]},{\"name\":\"phone\",\"values\":[\"+65 65149458\"]},{\"name\":\"fax-no\",\"values\":[\"+65 64413013\"]},{\"name\":\"e-mail\",\"values\":[\"net-infra@s-i2i.com\"]},{\"name\":\"nic-hdl\",\"values\":[\"MSD1-AP\"]},{\"name\":\"mnt-by\",\"links\":[{\"text\":\"MAINT-SG-MRNET\",\"url\":\"search.html?query=MAINT-SG-MRNET\"}]},{\"name\":\"changed\",\"values\":[\"win-naing@spicei2i.com 20110810\"]},{\"name\":\"changed\",\"values\":[\"win-naing@s-i2i.com 20130220\"]},{\"name\":\"source\",\"values\":[\"APNIC\"]}],\"objectType\":\"person\",\"primaryKey\":\"MSD1-AP\"},{\"type\":\"comments\",\"comments\":[\"% This query was served by the APNIC Whois Service version 1.69.1-APNICv1r7-SNAPSHOT (WHOIS3)\"]}]";

            JArray whois = JArray.Parse(result);
            var preciseR = whois.Children<JProperty>().FirstOrDefault(x => x.Name == "ObjectType").Value;

            foreach (var item in preciseR.Children())
            {
                var itemProperties = item.Children<JProperty>();
                //you could do a foreach or a linq here depending on what you need to do exactly with the value
                var myElement = itemProperties.FirstOrDefault(x => x.Name == "url");
                var myElementValue = myElement.Value; ////This is a JValue type
            }


            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test")));
        }
        [Test]
        public void TestMethod1()
        {
            Uri url = new Uri("http://www.domain.com/test");
            var values = new Dictionary<string, string>();
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test")));
        }
        [Test]
        public void Add_to_query_string_dictionary_when_url_are_not_empty_query()
        {
            Uri url = new Uri("http://whois.kisa.or.kr/openapi/whois.jsp?query=61.111.22.11&key=2016122313485156001035&answer=json");
            var values = new Dictionary<string, string> { { "query", "61.1.1.1" }, { "key", "20161223131111111" }, { "answer", "xml" } };
            var result = url.ExtendQuery(values);
            Assert.That(result, Is.EqualTo(new Uri("http://whois.kisa.or.kr/openapi/whois.jsp?query=61.1.1.1&key=20161223131111111&answer=xml")));
        }
        [Test]
        public void Add_to_query_string_dictionary_when_url_contains_hash_and_query_string_values_are_empty_should_return_url_without_changing_it()
        {
            Uri url = new Uri("http://www.domain.com/test#div");
            var values = new Dictionary<string, string>();
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test#div")));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_contains_no_query_string_should_add_values()
        {
            Uri url = new Uri("http://www.domain.com/test");
            var values = new Dictionary<string, string> { { "param1", "val1" }, { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test?param1=val1&param2=val2")));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_contains_hash_and_no_query_string_should_add_values()
        {
            Uri url = new Uri("http://www.domain.com/test#div");
            var values = new Dictionary<string, string> { { "param1", "val1" }, { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test#div?param1=val1&param2=val2")));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_contains_query_string_should_add_values_and_keep_original_query_string()
        {
            Uri url = new Uri("http://www.domain.com/test?param1=val1");
            var values = new Dictionary<string, string> { { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test?param1=val1&param2=val2")));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_is_relative_contains_no_query_string_should_add_values()
        {
            Uri url = new Uri("/test", UriKind.Relative);
            var values = new Dictionary<string, string> { { "param1", "val1" }, { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=val1&param2=val2", UriKind.Relative)));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_is_relative_and_contains_query_string_should_add_values_and_keep_original_query_string()
        {
            Uri url = new Uri("/test?param1=val1", UriKind.Relative);
            var values = new Dictionary<string, string> { { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=val1&param2=val2", UriKind.Relative)));
        }

        [Test]
        public void Add_to_query_string_dictionary_when_url_is_relative_and_contains_query_string_with_existing_value_should_add_new_values_and_update_existing_ones()
        {
            Uri url = new Uri("/test?param1=val1", UriKind.Relative);
            var values = new Dictionary<string, string> { { "param1", "new-value" }, { "param2", "val2" } };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=new-value&param2=val2", UriKind.Relative)));
        }

        [Test]
        public void Add_to_query_string_object_when_url_contains_no_query_string_should_add_values()
        {
            Uri url = new Uri("http://www.domain.com/test");
            var values = new { param1 = "val1", param2 = "val2" };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test?param1=val1&param2=val2")));
        }


        [Test]
        public void Add_to_query_string_object_when_url_contains_query_string_should_add_values_and_keep_original_query_string()
        {
            Uri url = new Uri("http://www.domain.com/test?param1=val1");
            var values = new { param2 = "val2" };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("http://www.domain.com/test?param1=val1&param2=val2")));
        }

        [Test]
        public void Add_to_query_string_object_when_url_is_relative_contains_no_query_string_should_add_values()
        {
            Uri url = new Uri("/test", UriKind.Relative);
            var values = new { param1 = "val1", param2 = "val2" };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=val1&param2=val2", UriKind.Relative)));
        }

        [Test]
        public void Add_to_query_string_object_when_url_is_relative_and_contains_query_string_should_add_values_and_keep_original_query_string()
        {
            Uri url = new Uri("/test?param1=val1", UriKind.Relative);
            var values = new { param2 = "val2" };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=val1&param2=val2", UriKind.Relative)));
        }

        [Test]
        public void Add_to_query_string_object_when_url_is_relative_and_contains_query_string_with_existing_value_should_add_new_values_and_update_existing_ones()
        {
            Uri url = new Uri("/test?param1=val1", UriKind.Relative);
            var values = new { param1 = "new-value", param2 = "val2" };
            var result = url.ExtendQuery(values);
            NUnit.Framework.Assert.That(result, Is.EqualTo(new Uri("/test?param1=new-value&param2=val2", UriKind.Relative)));
        }
    }
}
