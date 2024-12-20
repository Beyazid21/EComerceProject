using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tokens
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int TokenValidityInMunities { get; set; }


     
    }
}
