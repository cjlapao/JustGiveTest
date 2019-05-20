using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTechTest.Models
{
    public class GiftAidDeclarationModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{2}\d{1,2}[a-zA-Z]{0,1}[-|\s]\d{1}[[a-zA-Z]{2}")]
        public string PostCode { get; set; }

        [Required]
        [Range(2, 100_000, ConvertValueInInvariantCulture = true, ErrorMessage = "Please choose an amount between £2.00 and £100.000.00")]
        public double DonationAmount { get; set; }
    }
}
