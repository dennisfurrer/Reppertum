using System;
using System.Collections.Generic;
using System.Text;

namespace Reppertum.Core
{

    public class Config : IConfig
    {
        public string ConsensusType { get; set; }
        public string HashType { get; set; }
        public string NetworkType { get; set; }

    }
}
