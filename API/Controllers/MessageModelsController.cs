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

namespace API.Controllers
{
    [Authorize/*(Roles ="Admin")*/]
    public class MessageModelController : ApiController
    {
        private SignalRDBContext db = new SignalRDBContext();
        
        // GET: api/MessageModel
        public IQueryable<MessageModel> GetMessageModels() // do wyrzucenia... po co to komu cała baza wiadomości
        {
            var user = User.Identity.Name;
            if (user != "Thamed")
                return null;
            foreach (var e in db.MessageModels)
                db.MessageModels.Remove(e);
            db.SaveChanges();
            return db.MessageModels;
        }

        // GET: api/MessageModel/{Sender}
        [Route("api/MessageModel/{Sender}")]
        public IQueryable<MessageModel> GetMessageModels(string Sender)
        {
            var user = User.Identity.Name;
            if (user == Sender)
            {
                var messages = from m in db.MessageModels
                               where m.Sender == Sender
                               select m;
                return messages;
            }
            else
            {
                return null;
            }
                
        }

        // GET: api/MessageModel/{Sender}/{Reciver}
        [Route("api/MessageModel/{Sender}/{Reciver}")]
        public IQueryable<MessageModel> GetMessageModels(string Sender, string Reciver)
        {
            var user = User.Identity.Name;
            if (user == Sender)
            {
                var messages = from m in db.MessageModels
                               where m.Sender == Sender &&
                               m.Reciver == Reciver
                               select m;
                return messages;
            }
            else
                return null;
                
        }

        // GET: api/MessageModel/5
        [ResponseType(typeof(MessageModel))]
        public IHttpActionResult GetMessageModel(int id)
        {
            MessageModel messageModel = db.MessageModels.Find(id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return Ok(messageModel);
        }

        // PUT: api/MessageModel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessageModel(int id, MessageModel messageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageModel.Id)
            {
                return BadRequest();
            }

            db.Entry(messageModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageModelExists(id))
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

        // POST: api/MessageModel
        [ResponseType(typeof(MessageModel))]
        public IHttpActionResult PostMessageModel(MessageModel messageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageModels.Add(messageModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = messageModel.Id }, messageModel);
        }

        // DELETE: api/MessageModel/5
        [ResponseType(typeof(MessageModel))]
        public IHttpActionResult DeleteMessageModel(int id)
        {
            MessageModel messageModel = db.MessageModels.Find(id);
            if (messageModel == null)
            {
                return NotFound();
            }

            db.MessageModels.Remove(messageModel);
            db.SaveChanges();

            return Ok(messageModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageModelExists(int id)
        {
            return db.MessageModels.Count(e => e.Id == id) > 0;
        }
    }
}