using Infrastructure.EntityFrameworkCore.Context;

namespace Infrastructure.EntityFrameworkCore.Seed
{
    public static class DataSeed
    {
        public static void Initialize(RapidPayContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
