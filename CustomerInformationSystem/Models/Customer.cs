﻿using System;
using System.Collections.Generic;

namespace CustomerInformationSystem.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Tc { get; set; }
        public string? Emaİl { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
