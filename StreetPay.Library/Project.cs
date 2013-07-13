using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPay.Library
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Payment> Payments { get; set;}
        public string ImageUrl { get; set; }
        public string Image
        {
            get
            {
                return String.IsNullOrWhiteSpace(ImageUrl) ?
                    "http://" + Name.Replace(' ', '.') + ".jpg.to"
                    : ImageUrl;
            }
        }

        public double Funded
        {
            get
            {
                if (Payments == null)
                    return 0;

                return Payments.Select(x => x.Money).Sum();
            }
        }
    }
}
