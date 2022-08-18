using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFrameworkCore.Repositories
{
    public class CardsRepository: BaseRepository<Cards>,ICardsRepository
    {
        private readonly IConfiguration _configuration;
        public CardsRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
    }
}
