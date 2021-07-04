using Domain.Interfaces;

namespace EfDataAccess
{
    public class User : IIdDbRecord
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
