using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Core.Password
{
    public class PasswordProperties
    {
        public DateTime ExpireAt { get; set; } = DateTime.Now.AddYears(1);
        public bool IsExpireOn { get; set; } = false;
        public bool AutoRenewal { get; set; } = false;
        [XmlIgnore]
        public bool IsExpired => DateTime.Now > ExpireAt;

    }
}
