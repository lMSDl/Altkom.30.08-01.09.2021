using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Models;
using Services.Database;

namespace WebApi.Controllers
{
    public class PeopleController : ApiController
    {
        private IService<Person> _service = new Service<Person>();

        public IHttpActionResult Get()
        {
            return Ok(_service.Read());
        }

        public IHttpActionResult Get(int id)
        {
            var person = _service.Read(id);
            if (person == null)
                return NotFound();
            return Ok(_service.Read(id));
        }

        public IHttpActionResult Post(Person person)
        {
            int id = _service.Create(person);

            return CreatedAtRoute("DefaultApi", new { id = id }, id);
        }

        public IHttpActionResult Put(int id, Person person)
        {
            if (_service.Read(id) == null)
                return NotFound();


            _service.Update(id, person);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            var person = _service.Read(id);
            if (person == null)
                return NotFound();

            _service.Delete(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}