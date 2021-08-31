using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InMemory
{
    public class Service<T> : IService<T> where T : Entity
    {
        private ICollection<T> Entities { get; } = new List<T>();

        public int Create(T entity)
        {
            entity.Id = (Entities.Any() ? Entities.Max(x => x.Id) : 0) + 1 ;

            Entities.Add(entity);
            return entity.Id;
        }

        public bool Delete(int id)
        {
            var entity = Read(id);
            if (entity == null)
                return false;
            return Entities.Remove(entity);
        }

        public T Read(int id)
        {
            return Entities.Where(x => x.Id == id).SingleOrDefault();
        }

        public IEnumerable<T> Read()
        {
            return Entities.ToList();
        }

        public bool Update(int id, T entity)
        {
            if (!Delete(id))
                return false;

            entity.Id = id;
            Entities.Add(entity);
            return true;
        }
    }
}
