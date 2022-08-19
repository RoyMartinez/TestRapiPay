using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class CardRequestDto
    {
        public string Name { get; set; }
        public CardTypeEnum Type { get; set; }
    }
}
