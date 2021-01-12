using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XylyicXenonXamarin.interfaces;

namespace XylyicXenonXamarin.models
{
    public class NameOptions : INameOptions
    {
        public bool UsePrefix { get; set; }
        public string Prefix { get; set; }
    }
}
