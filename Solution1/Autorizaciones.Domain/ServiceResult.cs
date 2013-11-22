using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigs.AutorizacionesOnline.Models
{
    public class ServiceResult
    {
        public ICollection<string> Fails { get; set; }

        public bool Success { get { return Fails.Count == 0; } }

        public dynamic Data { get; set; }

        public void ThrowFail(string p)
        {
            Fails.Add(p);
        }

        public string Message { get; set; }
    }
}
