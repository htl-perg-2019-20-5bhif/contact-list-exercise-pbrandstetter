using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Controllers
{
    [ApiController]
    public class ContactListController : ControllerBase
    {
        private static readonly List<Person> Contacts = new List<Person>();

        [HttpGet]
        [Route("/contacts")]
        public virtual IActionResult GetAllPeople()
        {
            return Ok(Contacts);
        }

        [HttpGet]
        [Route("/contacts/findByName")]
        public virtual IActionResult FindPersonByName([FromQuery][Required()]string nameFilter)
        {
            IEnumerable<Person> nameQuery =
                from person in Contacts
                where person.FirstName == nameFilter || person.LastName == nameFilter
                select person;

            if (nameQuery.Count() > 0)
            {
                return Ok(nameQuery);
            }
            else
            {
                return BadRequest("Invalid or missing name");
            }
        }

        [HttpPost]
        [Route("/contacts")]
        public virtual IActionResult AddPerson([FromBody]Person body)
        {
            if (body.Id < 0 || body.Email.Length == 0)
            {
                return Unauthorized("Invalid input (e.g. required field missing or empty)");
            }

            Contacts.Add(body);
            return Created("Person successfully created", body);
        }

        [HttpDelete]
        [Route("/contacts/{personId}")]
        public virtual IActionResult DeletePerson([FromRoute][Required]int? personId)
        {
            if (personId < 0)
            {
                return BadRequest("Invalid ID supplied");
            }

            IEnumerable<Person> deleteQuery =
                from person in Contacts
                where person.Id == personId
                select person;

            if (deleteQuery.Count() == 0)
            {
                return NotFound("Person not found");
            }

            Contacts.Remove(deleteQuery.Single());

            return StatusCode(204, "Successful operation");
        }
    }
}
