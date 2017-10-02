using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestIdentity.Models;

namespace TestIdentity.DAL
{
    public class StudentsRepository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Create(PersonalInformation item)
        {
            db.PersonalInformation.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get()
        {
            return db.Set<T>();
        }

        

        public void Save() 
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}