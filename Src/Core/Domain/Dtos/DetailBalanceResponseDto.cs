using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class DetailBalanceResponseDto
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public decimal Fee { get; set; }
        public decimal Movement { get; set; }
        public decimal PercentageFee { get; set; }
        public decimal Total { get; set; }
        public decimal OldBalance { get; set; }
        public decimal NewBalance { get; set; }
    }
}
