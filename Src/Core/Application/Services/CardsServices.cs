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
        public CardsServices
        (
            ICardsRepository Cards
        )
        {
            _Cards = Cards;            
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

                _Cards.Add(card);

                var Response = MapCard(card);

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
    }
}
