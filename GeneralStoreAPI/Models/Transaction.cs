﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [ForeignKey("SKU")]
        public string ProductSKU { get; set; }
        public int ItemCount { get; set; }
        public DateTime DateOfTransaction { get; set; }

    }
}