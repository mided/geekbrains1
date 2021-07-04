using System.Collections.Generic;
using Domain.Interfaces;

namespace EfDataAccess
{
    public class Deed : IIdDbRecord
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Execution> Executions { get; set; }
    }
}