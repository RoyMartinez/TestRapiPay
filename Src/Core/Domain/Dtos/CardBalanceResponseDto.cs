using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CardBalanceResponseDto
    {
        public string Number { get; set; }
        public decimal Balance { get; set; }
    }
}
