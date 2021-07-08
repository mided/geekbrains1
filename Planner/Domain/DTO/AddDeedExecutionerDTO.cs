using Domain.DTO;

namespace Domain.DTO
{
    public class AddDeedExecutionerDTO
    {
        public int DeedId { get; set; }

        public DeedExecutionDTO Execution { get; set; }
    }
}
