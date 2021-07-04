using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDataRepository
    {
        public User GetUserByName(string name);

        public List<Domain.Entities.Deed> GetDeedsForUser(int userId, DateTime? from, DateTime? to);

        public T AddEntity<T>(T entity) where T : class;

        public void DeleteEntity<T>(IIdDbRecord entity) where T : class;

        public T GetById<T>(int id) where T : class;

        public List<T> GetAll<T>() where T : class;

        public T SaveEntity<T>(T entity) where T : class;
    }
}
