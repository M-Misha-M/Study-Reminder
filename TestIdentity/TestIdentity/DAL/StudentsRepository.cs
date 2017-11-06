using System;
using System.Linq;
using TestIdentity.Models;
using System.Data.Entity;

namespace TestIdentity.DAL
{
    public class StudentsRepository<T> : IDisposable ,  IRepository<T> where T : class
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Create(PersonalInformation item)
        {
            db.PersonalInformation.Add(item);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<T> Get()
        {
            return db.Set<T>();
        }

        public void Update(T personalInformation)
        {       
            db.Set<T>().Attach(personalInformation);
            db.Entry(personalInformation).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}