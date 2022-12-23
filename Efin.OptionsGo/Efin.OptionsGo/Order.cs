using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Efin.OptionsGo.Models
{
    public class Order
    {

        public Order(double price, string v, OrderSide orderSide, int contract)
        {
            Price = price;
            this.Symbol = v;
            this.Side = orderSide;
            this.Contracts = contract;
        }
        public Order()
        {
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Symbol { get; } = string.Empty;
        public double Price { get;  }
        public int Contracts { get;  }
        public OrderSide Side { get; }
         public static Order? Fromtext(string text)
         {
            if(text.Trim() == "")
            {
                return null;
            }
            Match match = Regex.Match(text, @"^(?<side>([LS]{1}))(?<symbol>([FCP]{1}))\s*    @(?<price>((\d+\.\d+)|(\d+)))\s*x\s*(?<strike>((\d+\.\d+)|(\d+)))$");
            if (match.Success)
            {
                // Extract the symbol, price, and contract from the match
                OrderSide side = match.Groups["side"].Value.Equals("L") ? OrderSide.Long : OrderSide.Short;
                string FCP = match.Groups["symbol"].Value;
                string symbol = FCP.Equals("F") ? "S50X99" : FCP.Equals("C") ? "S50X99C1000" : FCP.Equals("P") ? "S50X99P950" : "unknow";
                double temp = double.Parse(match.Groups["price"].Value);
                double price = Math.Round(temp,1,MidpointRounding.AwayFromZero);
                int contract = int.Parse(match.Groups["strike"].Value);

                // Use the extracted values
                return new Order(price, symbol, side, contract);
            }
            return null;
        }

        public double calculateProfitLoss(double index)
        {
            var p = index - Price;
            return (Side == OrderSide.Long? p:p*-1) * Contracts;
        }
    }

}