using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIdentity.Models;

namespace TestIdentity.DAL
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        IQueryable<T> Get();
       
        void Create(PersonalInformation item);
        void Update(T item); 
        void Delete(int id); 
        void Save(); 
    }
}
