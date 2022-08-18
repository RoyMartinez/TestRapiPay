using Domain.Dtos;
using Domain.Enums;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Cards : Entity
    {
        public string Name { get; set; }
        public string Numbers { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Records> Records { get; set; }

        public Cards() { }
        public Cards(CardRequestDto card,int UserId) 
        {
            UserCreatorId = UserId;
            CreationTime = DateTime.Now;
            ExpirationDate = DateTime.Now.AddDays(2);
            Name = card.Name;
            GenerateNumbers(card.Type);
            Balance = card.Amount;
        }
        public void GenerateNumbers(CardTypeEnum Type)
        {
            var Random = new Random();
            Numbers = Type == CardTypeEnum.Visa ? "40004200": "50002400";
            Numbers += Random.Next(1, 9999999).ToString().PadLeft(7, '0');
            CVV += Random.Next(1, 999).ToString().PadLeft(2, '0');
        }
    }
}
