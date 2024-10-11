using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalManagement_V2
{
    internal class Bike
    {
        public Bike(string bikeid, string brand, string modal, decimal rentprice)
        {
            BikeId = bikeid;
            this.brand = brand;
            this.modal = modal;
            Rentprice = rentprice;
        }

        public string BikeId { get; set; }
        public string brand { get; set; }
            public string modal { get; set; }
        public decimal Price { get; set; }
        public decimal Rentprice { get; }
    }

    
}
