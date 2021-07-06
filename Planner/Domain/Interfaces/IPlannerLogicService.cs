using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPlannerLogicService
    {
        public User RegisterUser(RegisterUserDTO dto);

        public User GetUser(int? id, string name);

        public List<User> GetUsers();

        public List<Deed> GetUserDeeds(int userId, DateTime? from, DateTime? to);

        public Deed CompleteDeed(int deedId, int userId);

        public Deed CancelDeedCompletion(int deedId, int userId);

        public Deed CreateDeed(CreateDeedDTO dto);

        public Deed AddExecutioner(AddDeedExecutionerDTO dto);

        public void DeleteDeed(int deedId);

        public Deed DeleteExecutioner(int deedId, int userId);
    }
}
