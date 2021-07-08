using Domain.Interfaces;

namespace EfDataAccess
{
    internal class User : IEntityWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
