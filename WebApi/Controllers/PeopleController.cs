using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Models;
using Services.Database;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class PeopleController : ApiController
    {
        private IAsyncService<Person> _service = new Service<Person>();

        public async Task<IHttpActionResult> Get()
        {
            return Ok(await _service.ReadAsync());
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var person = await _service.ReadAsync(id);
            if (person == null)
                return NotFound();
            return Ok(person);
        }

        public async Task<IHttpActionResult> Post(Person person)
        {
            int id = await _service.CreateAsync(person);

            return CreatedAtRoute("DefaultApi", new { id = id }, id);
        }

        public async Task<IHttpActionResult> Put(int id, Person person)
        {
            if (await _service.ReadAsync(id) == null)
                return NotFound();


            await _service.UpdateAsync(id, person);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var person = await _service.ReadAsync(id);
            if (person == null)
                return NotFound();

            await _service.DeleteAsync(id);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}