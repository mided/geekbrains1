using System.Collections.Generic;

namespace Planner.OutDTO
{
    public class DeedOutDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<DeedExecutionOutDTO> Executions { get; set; }
    }
}