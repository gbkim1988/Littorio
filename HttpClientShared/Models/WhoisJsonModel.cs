using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientShared.Models
{
    public sealed class WhoisJsonModel
    {
    }

    public class Netinfo
    {
        public string range { get; set; }
        public string prefix { get; set; }
        public string servName { get; set; }
        public string orgName { get; set; }
        public string orgID { get; set; }
        public string addr { get; set; }
        public string zipCode { get; set; }
        public string regDate { get; set; }
    }

    public class TechContact
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class PI
    {
        public Netinfo netinfo { get; set; }
        public TechContact techContact { get; set; }
    }

    public class Netinfo2
    {
        public string range { get; set; }
        public string prefix { get; set; }
        public string netType { get; set; }
        public string orgName { get; set; }
        public string orgID { get; set; }
        public string addr { get; set; }
        public string zipCode { get; set; }
        public string regDate { get; set; }
    }

    public class TechContact2
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class User
    {
        public Netinfo2 netinfo { get; set; }
        public TechContact2 techContact { get; set; }
    }

    public class Korean
    {
        public PI PI { get; set; }
        public User user { get; set; }
    }

    public class Netinfo3
    {
        public string range { get; set; }
        public string prefix { get; set; }
        public string servName { get; set; }
        public string orgName { get; set; }
        public string orgID { get; set; }
        public string addr { get; set; }
        public string zipCode { get; set; }
        public string regDate { get; set; }
    }

    public class TechContact3
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class PI2
    {
        public Netinfo3 netinfo { get; set; }
        public TechContact3 techContact { get; set; }
    }

    public class Netinfo4
    {
        public string range { get; set; }
        public string prefix { get; set; }
        public string netType { get; set; }
        public string orgName { get; set; }
        public string orgID { get; set; }
        public string addr { get; set; }
        public string zipCode { get; set; }
        public string regDate { get; set; }
    }

    public class TechContact4
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class User2
    {
        public Netinfo4 netinfo { get; set; }
        public TechContact4 techContact { get; set; }
    }

    public class English
    {
        public PI2 PI { get; set; }
        public User2 user { get; set; }
    }

    public class Whois
    {
        public string query { get; set; }
        public string queryType { get; set; }
        public string registry { get; set; }
        public string countryCode { get; set; }
        public Korean korean { get; set; }
        public English english { get; set; }
    }

    public class WhoisRoot
    {
        public Whois whois { get; set; }
    }
}
