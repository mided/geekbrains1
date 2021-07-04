using System;
using Domain.Entities;

namespace Planner.OutDTO
{
    public class DeedExecutionOutDTO
    {
        public int Id { get; set; }

        public DateTime? PlannedDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public User User { get; set; }
    }
}