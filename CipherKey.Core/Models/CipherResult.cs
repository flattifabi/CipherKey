using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Models
{
    public class CipherResult<T>
    {
        public T? ResultData { get; set; }
        public string ErrorText { get; set; } = "NoError";
        public int ErrorCode { get; set; } = 0;
    }
}
