using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class RecordRequestDto
    {
        public string CardName { get; set; }
        public string CardNumbers { get; set; }
        public string CardCVV { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public decimal Amount { get; set; }
        public RecordTypeEnum Type { get; set; }
    }
}
