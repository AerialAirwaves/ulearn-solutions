using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    class AccountingModel : ModelBase
    {
        private double price, discount, total;
        private int nightsCount;

        public double Price
        {
            get => this.price;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                UpdateTotal();
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get => this.nightsCount; 
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
                UpdateTotal();
            }
        }

        public double Discount
        {
            get => this.discount;
            set
            {
                discount = value;
                Notify(nameof(Discount));
                if (Math.Abs(discount - EvaluateDiscount()) > 1e-9)
                    UpdateTotal();
            }
        }

        public double Total
        {
            get => this.total;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                if (Math.Abs(total - Price * NightsCount
                        * (1 - Discount / 100)) > 1e-9)
                    Discount = EvaluateDiscount();
                Notify(nameof(Total));
            }
        }
        
        public double EvaluateDiscount()
        {
            return 100 * (1 - (Total / (Price * NightsCount)));
        }

        public void UpdateTotal()
        {
            Total = Price * NightsCount * (1 - Discount / 100);
        }
    }
}