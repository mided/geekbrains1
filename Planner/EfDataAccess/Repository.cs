using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Interfaces;

namespace EfDataAccess
{
    public class Repository : IDataRepository
    {
        private readonly PlannerContext _context;

        private readonly Mapper _mapper;

        public Repository()
        {
            _context = new PlannerContext();
            _mapper = MapperFactory.Mapper;
        }

        #region Generic

        public T GetById<T>(int id) where T : class
        {
            var dbType = GetDbType(typeof(T));

            var record = _context.Set(dbType).OfType<IEntityWithId>().FirstOrDefault(r => r.Id == id);

            return _mapper.Map<T>(record);
        }

        public List<T> GetAll<T>() where T : class
        {
            var dbType = GetDbType(typeof(T));

            return _context.Set(dbType).ToList().Select(r => _mapper.Map(r, r.GetType(), typeof(T)) as T).ToList();
        }

        public T SaveEntity<T>(T entity) where T : class
        {
            var dbType = GetDbType(typeof(T));
            var id = ((IEntityWithId) entity).Id;

            var dbRecord = _context.Set(dbType).OfType<IEntityWithId>().FirstOrDefault(r => r.Id == id);

            _mapper.Map(entity, dbRecord);

            _context.SaveChanges();

            return _mapper.Map<T>(dbRecord);
        }

        public T AddEntity<T>(T entity) where T : class
        {
            var dbType = GetDbType(typeof(T));

            var dbRecord = _mapper.Map(entity, typeof(T), dbType);

            _context.Add(dbRecord);

            _context.SaveChanges();

            return _mapper.Map<T>(dbRecord);
        }

        public void DeleteEntity<T>(IEntityWithId entity) where T : class
        {
            var dbType = GetDbType(typeof(T));

            var record = _context.Set(dbType).OfType<IEntityWithId>().FirstOrDefault(r => r.Id == entity.Id);

            _context.Remove(record);

            _context.SaveChanges();
        }

        #endregion

        #region Specific

        public Domain.Entities.User GetUserByName(string name)
        {
            return _mapper.Map<Domain.Entities.User>(_context.Users.FirstOrDefault(u => u.Name == name));
        }

        public List<Domain.Entities.Deed> GetDeedsForUser(int userId, DateTime? from, DateTime? to)
        {
            var executions = _context.Executions.Where(e => e.UserId == userId);

            if (from.HasValue)
            {
                executions = executions.Where(e => e.PlannedDate >= from);
            }

            if (to.HasValue)
            {
                executions = executions.Where(e => e.PlannedDate <= to);
            }

            var deeds = _context.Deeds.Where(d => executions.Any(e => e.DeedId == d.Id)).ToList();

            return deeds.Select(d => _mapper.Map<Domain.Entities.Deed>(d)).ToList();
        }

        #endregion Specific

        private Type GetDbType(Type entityType)
        {
            return _mapper.ConfigurationProvider.GetAllTypeMaps().First(t => t.SourceType == entityType).DestinationType;
        }
    }
}
