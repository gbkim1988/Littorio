using Littorio.Final;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littorio.Models
{
    public class WhoisAPNIC
    {
        private string _ip; // init from query
        private string _inetnum; // init from inetnum
        private string _IRT; // init from irt
        private string _ADDR; // init from irt
        private string _country; // init from person
        private string _phone; // init from person
        private string _email; // init from person
        private string _source; // init from person
        public WhoisAPNIC(string query)
        {
            this.IP = query;
        }

        public string IP
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        public string INETNUM
        {
            get { return this._inetnum; }
            set { this._inetnum = value; }
        }

        public string IRT
        {
            get { return this._IRT; }
            set { this._IRT = value; }
        }

        public string ADDR
        {
            get { return this._ADDR;  }
            set { this._ADDR = value; }
        }

        public string COUNTRY
        {
            get { return this._country; }
            set {

                this._country = Countries.countryCodesMapping[value];
            }
        }

        public string PHONE
        {
            get { return this._phone; }
            set { this._phone = value; }
        }

        public string EMAIL
        {
            get { return this._email; }
            set { this._email = value; }
        }

        public string SRC
        {
            get { return this._source; }
            set { this._source = value; }
        }
    }
}
