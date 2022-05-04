﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Dtos.Response
{
    public class GetAddressResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string AddressDetail { get; set; }
        public string DesiredName { get; set; }
    }
}
