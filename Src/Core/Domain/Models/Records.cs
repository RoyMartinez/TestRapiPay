using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Records: Entity
    {
        public int CardId { get; set; }
        public decimal Payment { get; set; }
        public decimal Fee { get; set; }
        public decimal Total { get; set; }
        public virtual Cards Card { get; set; }
    }
}
