using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace API.Controllers
{
    [Authorize/*(Roles ="Admin")*/]
    public class ContactModelsController : ApiController
    {
        private SignalRDBContext db = new SignalRDBContext();
        //GET: api/ContactModels
        public IQueryable<ContactModel> GetContactModels()
        {
            var user = User.Identity.Name;
            if (user != "Thamed")
                return null;
            foreach (var e in db.ContactModels)
            {
                db.ContactModels.Remove(e);
                
            }
            db.SaveChanges();
            return db.ContactModels;
        }

        // GET: api/ContactModels/Find/{contact}
        [Route("api/ContactModels/Find/{contact}")]
        [ResponseType(typeof(ContactModel))]
        public IHttpActionResult FindContactModel(string contact)
        {

            var contacts = from m in db.ContactModels
                           where m.Me == contact
                           select m;
            if (contact == null)
                return BadRequest();
            return Ok(contacts);
        }
        //GET: api/ContactModel/{Sender}
        [Route("api/ContactModels/{Sender}")]
        public IHttpActionResult GetContactModel(string sender)
        {
            var user = User.Identity.Name;
            if (user == sender)
            {
                var contacts = from m in db.ContactModels
                               where m.Me == sender
                               select m;
                if (contacts == null)
                    return BadRequest();
                return Ok(contacts);
            }
            else
                return BadRequest("You are not someone who deserve to call this function!");
        }

        // PUT: api/ContactModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactModel(int id, ContactModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactModel.ID)
            {
                return BadRequest();
            }

            db.Entry(contactModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContactModels
        [ResponseType(typeof(ContactModel))]
        public IHttpActionResult PostContactModel(ContactModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.ContactModels.Add(contactModel);
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/ContactModels/5
        [ResponseType(typeof(ContactModel))]
        public IHttpActionResult DeleteContactModel(int id)
        {
            ContactModel contactModel = db.ContactModels.Find(id);
            if (contactModel == null)
            {
                return NotFound();
            }

            db.ContactModels.Remove(contactModel);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactModelExists(int id)
        {
            return db.ContactModels.Count(e => e.ID == id) > 0;
        }
    }
}