using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFrameworkCore.Repositories
{
    public class UsersRepository:BaseRepository<Users>,IUsersRepository
    {
        private readonly IConfiguration _configuration;
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
    }
}
