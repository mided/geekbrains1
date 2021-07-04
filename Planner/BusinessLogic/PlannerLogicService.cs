using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace BusinessLogic
{
    public class PlannerLogicService : IPlannerLogicService
    {
        private readonly IDataRepository _repository;

        public PlannerLogicService(IDataRepository repository)
        {
            _repository = repository;
        }

        public User RegisterUser(RegisterUserDTO dto)
        {
            var user = _repository.GetUserByName(dto.Name);

            if (user != null)
            {
                throw new DuplicateRecordException();
            }

            return _repository.AddEntity(new User { Name = dto.Name });
        }

        public User GetUser(int? id, string name)
        {
            var byId = id.HasValue ? _repository.GetById<User>(id.Value) : null;
            var byName = !string.IsNullOrEmpty(name) ? _repository.GetUserByName(name) : null;

            if ((id.HasValue && byId == null) || 
                (!string.IsNullOrEmpty(name) && byName == null) ||
                (byName == null && byId == null) ||
                (byName != null && byId != null && byName.Name != byId.Name))
            {
                throw new NotFoundException();
            }

            return byId ?? byName;
        }

        public List<User> GetUsers()
        {
            return _repository.GetAll<User>();
        }

        public List<Deed> GetUserDeeds(int userId, DateTime? from, DateTime? to)
        {
            return _repository.GetDeedsForUser(userId, from, to);
        }

        public Deed CompleteDeed(int deedId, int userId)
        {
            return ChangeDeedExecution(deedId, userId, true);
        }

        public Deed CancelDeedCompletion(int deedId, int userId)
        {
            return ChangeDeedExecution(deedId, userId, false);
        }

        public Deed CreateDeed(CreateDeedDTO dto)
        {
            var deed = _repository.AddEntity(new Deed { Description = dto.Description });

            if (dto.Executions != null && dto.Executions.Any())
            {
                deed.Executions = dto.Executions.Select(e => new Execution
                    {DeedId = deed.Id, UserId = e.ExecutionerId, PlannedDate = e.PlannedDate}).ToList();
            }

            _repository.SaveEntity(deed);

            return _repository.GetById<Deed>(deed.Id);
        }

        public Deed AddExecutioner(AddDeedExecutionerDTO dto)
        {
            var deed = _repository.GetById<Deed>(dto.DeedId);
            deed.Executions.Add(new Execution
                { DeedId = deed.Id, UserId = dto.Execution.ExecutionerId, PlannedDate = dto.Execution.PlannedDate });

            _repository.SaveEntity(deed);

            return _repository.GetById<Deed>(deed.Id);
        }

        private Deed ChangeDeedExecution(int deedId, int userId, bool executed)
        {
            var deed = _repository.GetById<Deed>(deedId);

            var execution = deed.Executions.FirstOrDefault(e => e.UserId == userId);

            if (execution == null)
            {
                throw new NotFoundException();
            }

            execution.ExecutionDate = executed ? DateTime.Now as DateTime? : null;

            _repository.SaveEntity(execution);

            return deed;
        }
    }
}
