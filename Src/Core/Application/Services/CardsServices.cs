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
    public class CardsServices: ICardsServices
    {
        private readonly ICardsRepository _Cards;
        private readonly IRecordsRepository _Records;

        public CardsServices
        (
            ICardsRepository Cards,
            IRecordsRepository Records
        )
        {
            _Cards = Cards;
            _Records = Records;
        }

        public IEnumerable<CardResponseDto> GetCards(int UserId) 
        {
            try
            {
                var Response = _Cards.Find(c=> c.UserCreatorId == UserId)
                                     .Select(card=> MapCard(card))
                                     .AsEnumerable();
                return Response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CardResponseDto CreateCard(CardRequestDto request,int UserId) 
        {
            try
            {
                var card = new Cards(request, UserId);
                card.Balance = 0;

                _Cards.Add(card);

                var Response = MapCard(card);

                return Response;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public CardBalanceResponseDto GetCardBalance(string Numbers,int UserId)
        {
            try
            {
                var Card = _Cards.Find(c => c.Numbers == Numbers && c.UserCreatorId == UserId).FirstOrDefault();

                if (Card == null) return null;

                var Records = _Records.Find(r => r.CardId == Card.Id)
                              .Select(c => MapRecord(c))
                              .OrderByDescending(c => c.Date)
                              .AsEnumerable();

                var Response = new CardBalanceResponseDto() 
                {
                    Number = Card.Numbers,
                    Balance = Card.Balance,
                    Movements = Records
                };

                return Response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CardResponseDto MapCard(Cards card) => new CardResponseDto()
        {
            Type = card.Numbers.StartsWith("4000") ? "Visa" : "MasterCard",
            Name = card.Name,
            Numbers = card.Numbers,
            CVV = card.CVV,
            ExpirationDate = new DateTime(card.ExpirationDate.Year, card.ExpirationDate.Month, 1),
            Balance = card.Balance,
        };

        public RecordsResponseDto MapRecord(Records record) => new RecordsResponseDto()
        {
            Date = record.CreationTime,
            Reference = record.PaymentReference,
            Fee = record.Fee,
            Movement = record.Amount,
            PercentageFee = record.PercentageFee,
            Total=record.Total,
            OldBalance = record.CardOldBalance,
            NewBalance = record.CardNewBalance
        };
    }
}
