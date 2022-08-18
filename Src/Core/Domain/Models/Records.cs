using Domain.Dtos;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Records : Entity
    {
        public int CardId { get; set; }
        public int RecordType { get; set; }
        public string PaymentReference { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public decimal Total { get; set; }
        public virtual Cards Card { get; set; }

        public void SetRecords(RecordRequestDto requests,int UserId,int cardId) 
        {
            CreationTime = DateTime.Now;
            UserCreatorId = UserId;
            CardId = cardId;
            RecordType = (int)requests.Type;
            Amount = requests.Amount;
            Fee = Amount*(UFE.GetUFE().GetFee/100);
            Total = Amount + Fee;
        }


    }
}
