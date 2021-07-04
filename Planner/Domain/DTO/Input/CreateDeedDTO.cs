using System.Collections.Generic;

namespace Domain.DTO
{
    public class CreateDeedDTO
    {
        public string Description { get; set; }

        public List<DeedExecutionDTO> Executions { get; set; }
    }
}
