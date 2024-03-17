using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Core.Password
{
    public class Topic
    {
        [XmlAttribute]
        public string Name { get; set; } = "";
        [XmlAttribute]
        public string Description { get; set; } = "";
        [XmlAttribute]
		public Guid ID { get; } = Guid.NewGuid();
		public TopicEntryDesign Design { get; set; } = new TopicEntryDesign();
    }
}
