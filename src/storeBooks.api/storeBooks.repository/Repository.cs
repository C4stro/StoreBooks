using storeBooks.repository.Dto;
using storeBooks.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Props
        
        #endregion

        //public Repository(DbContextModels context)
        //{
        //    _context = context;
        //}

        public IEnumerable<T> GetAllActive()
        {
            throw new NotImplementedException();
        }

        public bool Inactivate(T obj)
        {
            throw new NotImplementedException();
        }

        public int Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
