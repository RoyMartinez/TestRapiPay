using Domain.Dtos;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string CreateRecord(RecordRequestDto Request,int UserId)
        {

            var Card = SearchCard(Request,UserId);

            if (Card == null)
                return $"404 Card not found";

            if (Request.Type == RecordTypeEnum.Payment)
                Request.Amount = Request.Amount * -1m;
            else if (Request.Type == RecordTypeEnum.Recharge)
                Request.Amount = Request.Amount * -1m;
            else
                return "Error: Type of Transaction Invalid";

            var transaccion = new Records();
            transaccion.SetRecords(Request, UserId, Card.Id);

            Card.Balance += transaccion.Total;
            if (Card.Balance < 0)
                return $"Error: not enough Balance in the Card";

            UpdateBalance(Card, transaccion.Total);
            _Records.Add(transaccion);

            return $"200: Transaction executed succesfully";
        }
        public Cards SearchCard(RecordRequestDto Request, int UserId) 
        {
            var card = _Cards.Find(c =>
                c.Name == Request.CardName &&
                c.Numbers == Request.CardNumbers &&
                c.CVV == Request.CardCVV &&
                c.ExpirationDate.Month == Request.CardExpirationDate.Month &&
                c.ExpirationDate.Year == Request.CardExpirationDate.Year
            ).FirstOrDefault();

            return card;
        }

        public void UpdateBalance(Cards card, decimal newbalance) 
        {
            card.Balance = newbalance;
            _Cards.Edit(card);
        }
    }
}
