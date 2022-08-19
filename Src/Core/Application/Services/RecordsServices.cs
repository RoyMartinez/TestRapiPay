using Domain.Dtos;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RecordsServices : IRecordsServices
    {
        private readonly IRecordsRepository _Records;
        private readonly ICardsRepository _Cards;
        public RecordsServices
        (
            IRecordsRepository Records,
            ICardsRepository Cards
        )
        {
            _Records = Records;
            _Cards = Cards;
        }

        public async Task<string> CreateRecord(RecordRequestDto Request,int UserId)
        {

            var Card = SearchCard(Request,UserId);

            if (Card == null)
                return $"404 Card not found";
            
            if (Request.Type == RecordTypeEnum.Payment)
                Request.Amount = Request.Amount * -1m;
            else if (Request.Type != RecordTypeEnum.Recharge)
                return "Error: Type of Transaction Invalid";

            var transaccion = new Records();
            transaccion.SetRecords(Request, UserId, Card.Id);
            transaccion.CardOldBalance = Card.Balance;
            Card.Balance += transaccion.Total;
            transaccion.CardNewBalance = Card.Balance;
            if (Card.Balance <= 0)
                return $"Error: not enough Balance in the Card";


            var task = UpdateBalance(Card);
            _Records.Add(transaccion);
            await task;

            return $"200: Transaction executed succesfully, Reference:{transaccion.PaymentReference}";
        }
        public Cards SearchCard(RecordRequestDto Request, int UserId) 
        {
            var card = _Cards.Find(c =>
                c.Name == Request.CardName &&
                c.Numbers == Request.CardNumbers &&
                c.CVV == Request.CardCVV &&
                c.ExpirationDate.Month == Request.CardExpirationDate.Month &&
                c.ExpirationDate.Year == Request.CardExpirationDate.Year &&
                c.UserCreatorId == UserId 
            ).FirstOrDefault();
            return card;
        }

        public Task UpdateBalance(Cards card) 
        {
            _Cards.Edit(card);
            return Task.CompletedTask;
        }
    }
}
