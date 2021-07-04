using Domain.Interfaces;

namespace Domain.Entities
{
    public class User : IEntityWithId
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
