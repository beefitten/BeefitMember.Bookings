namespace Persistence.Settings
{
    public class BookingsSettings: IDatabaseSettings
    {
        public BookingsSettings()
        {
            CollectionName = "Bookings";
        }
        
        public string CollectionName { get; }
        public string ConnectionString { get; }
        public string DatabaseName { get; }
    }
}