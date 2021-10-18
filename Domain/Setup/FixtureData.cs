using Microsoft.Extensions.DependencyInjection;

namespace Domain.Setup
{
    public class FixtureData
    {
        public ServiceProvider ServiceProvider { get; }

        public FixtureData()
        {
            ServiceProvider = new ServiceCollection()
                .AddDomain()
                .BuildServiceProvider();
        }
    }
}