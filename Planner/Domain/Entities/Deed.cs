using System.Collections.Generic;

namespace Domain.Entities
{
    public class Deed
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Execution> Executions { get; set; }
    }
}