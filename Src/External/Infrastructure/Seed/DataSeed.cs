using Infrastructure.Context;

namespace Infrastructure.Seed
{
    public static class DataSeed
    {
        public static void Initialize(RapiPayContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
