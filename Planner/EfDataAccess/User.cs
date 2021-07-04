using Domain.Interfaces;

namespace EfDataAccess
{
    public class User : IEntityWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
