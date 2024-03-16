using CipherKey.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Core.Password
{
    public class PasswordBase
    {
        [XmlAttribute]
        public PasswordType PasswordType { get; set; } = PasswordType.Other;
        [XmlAttribute]
        public string Topic { get; set; } = "General";
        [XmlAttribute]
        public string Value { get; set; } = string.Empty;
        [XmlAttribute]
        public string Note { get; set; } = string.Empty;
        [XmlAttribute]
        public StorageType StorageType { get; set; } = StorageType.Local;
        public PasswordEntryDesign PasswordEntryDesign { get; set; } = new PasswordEntryDesign();
        public PasswordProperties PasswordProperties { get; set; } = new PasswordProperties();
    }
}
