using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CardBalanceResponseDto
    {
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<RecordsResponseDto> Movements { get; set; }
    }
    public class RecordsResponseDto
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
