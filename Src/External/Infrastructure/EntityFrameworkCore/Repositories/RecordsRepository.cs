using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFrameworkCore.Repositories
{
    public class RecordsRepository:BaseRepository<Records>,IRecordsRepository
    {
        private readonly IConfiguration _configuration;
        public RecordsRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
    }
}
