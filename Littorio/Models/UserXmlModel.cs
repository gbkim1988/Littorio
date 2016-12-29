using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Littorio.Models
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class UserXmlModel
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
