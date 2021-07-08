using System.Collections.Generic;
using Domain.Interfaces;

namespace EfDataAccess
{
    internal class Deed : IEntityWithId
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Execution> Executions { get; set; }
    }
}