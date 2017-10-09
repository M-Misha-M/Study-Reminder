using System;
using System.Linq;
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
            db.Dispose();
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