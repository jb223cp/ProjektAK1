﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebshopClick.Model.BLL
{
    /// <summary>
    /// Class for Payment table in the database
    /// </summary>
    public class Payment
    {
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "En betalning måste anges.")]
        [StringLength(30, ErrorMessage = "Betalningen kan bestå av som mest 30 tecken.")]
        public string PaymentType { get; set; }
    }
}