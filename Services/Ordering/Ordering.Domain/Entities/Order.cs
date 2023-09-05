﻿using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // Billing Adress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        // Payment
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
