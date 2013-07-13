using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreetPay.Library
{
    public class Payment
    {
        [SerializeAs(Name = "nick")]
        public string Nick { get; set; }
        [SerializeAs(Name = "money")]
        public double Money { get; set; }
        [SerializeAs(Name = "project")]
        public string Project { get; set; }
        [SerializeAs(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }
}
