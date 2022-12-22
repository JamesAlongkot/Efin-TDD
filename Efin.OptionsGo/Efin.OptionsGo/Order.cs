using System.Text.RegularExpressions;

namespace Efin.OptionsGo
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

        public double Price { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public int Contracts { get; set; }
        public OrderSide Side { get; set; }
         public static Order? Fromtext(string text)
         {
            if(text == "")
            {
                return null;
            }
            Match match = Regex.Match(text, @"^([LS]{1})([FCP]{1})\s*@([0-9.]+)\s*x\s*(\d+)$");
            if (match.Success)
            {
                // Extract the symbol, price, and contract from the match
                OrderSide side = match.Groups[1].Value.Equals("L") ? OrderSide.Long : OrderSide.Short;
                string FCP = match.Groups[2].Value;
                string symbol = FCP.Equals("F") ? "S50X99" : FCP.Equals("C") ? "S50X99C1000" : FCP.Equals("P") ? "S50X99P950" : "unknow";
                double temp = double.Parse(match.Groups[3].Value);
                double price = Math.Round(temp,1,MidpointRounding.AwayFromZero);
                int contract = int.Parse(match.Groups[4].Value);

                // Use the extracted values
                return new Order(price, symbol, side, contract);
            }
            return null;
        }
    }

}