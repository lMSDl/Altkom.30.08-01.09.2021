using Models;
using Services.Database;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PeopleService" in both code and config file together.
    public class PeopleService : IPeopleService
    {
        private IService<Person> _service = new Service<Person>();

        public int Create(Person entity)
        {
            return _service.Create(entity);
        }

        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        public Person Read(int id)
        {
            return _service.Read(id);
        }

        public IEnumerable<Person> ReadAll()
        {
            return _service.Read();
        }

        public bool Update(int id, Person entity)
        {
            return _service.Update(id, entity);
        }
    }
}
