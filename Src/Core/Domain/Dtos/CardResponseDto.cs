using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CardResponseDto
    {
        public string Name { get; set; }
        public string Numbers { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
