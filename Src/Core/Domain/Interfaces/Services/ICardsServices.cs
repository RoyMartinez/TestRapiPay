using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    public interface ICardsServices
    {
        CardResponseDto CreateCard(CardRequestDto request, int UserId);
        IEnumerable<CardResponseDto> GetCards(int UserId);
    }
}
