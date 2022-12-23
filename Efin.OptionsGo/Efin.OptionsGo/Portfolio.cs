using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efin.OptionsGo.Models
{
    public class Portfolio
    {
        public Portfolio()
        {
            Orders= new HashSet<Order>();
        }
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = "Portfolio";

        public virtual ICollection<Order> Orders { get; set; }
        public double ProfitLoss => Orders.Sum(x => x.calculateProfitLoss(Index));
        public double Index { get; set; }

        public static Portfolio getPL() {
            throw new NotImplementedException();
        }

        public Order? AddOrder(string v) {
            var o = Order.Fromtext(v);
            if (o != null)
            {
                Orders.Add(o);
            }
            return o;
        }

        //public double ProfitLoss()
        //{
        //    Order? o = Orders.FirstOrDefault();

        //    return Math.Abs(o.Price - Index);
        //}
    }
}
