using System;
using Domain.Interfaces;

namespace EfDataAccess
{
    public class Execution : IIdDbRecord
    {
        public int Id { get; set; }

        public int DeedId { get; set; }

        public int UserId { get; set; }

        public DateTime? PlannedDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public User User { get; set; }

        public Deed Deed { get; set; }
    }
}