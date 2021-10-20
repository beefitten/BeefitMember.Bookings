namespace Persistence.Settings
{
    public class BookingSettings : IDatabaseSettings
    {
        public BookingSettings()
        {
            CollectionName = "Classes";
            ConnectionString = AppConfig.GetConnectionString();
            DatabaseName = "BeefitMember";
        }
        
        public string CollectionName { get; }
        public string ConnectionString { get; }
        public string DatabaseName { get; }
    }
}