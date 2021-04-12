using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAlert
{
    public enum Company
    {
        NVIDIA,
        AMD
    }


    // Represents an item (usually a GPU).
    // Holds essential informatoin about this item
    public class Item
    {
        public Item(int Price, Company Company, string CardName)
        {
            this.Price = Price;
            this.Company = Company;
            this.CardName = CardName;
        }

        public int Price { get;  }
        public Company Company { get; }
        public string CardName { get; }

    }
}
