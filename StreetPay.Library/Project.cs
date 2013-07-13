using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetPay.Library
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Payment> Payments { get; set;}
        public string Image
        {
            get
            {
                return Name.Replace(' ', '.') + ".jpg.to";
            }
        }

        public int Funded
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
