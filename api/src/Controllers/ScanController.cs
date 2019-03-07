using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers {
    [Route("scans")]
    [ApiController]
    public class ScanController : ControllerBase {
        private Context context;
        
        public ScanController(Context context) {
            this.context = context;
        }

        public IQueryable<Scan> scans {
            get {
                return context.Scans
                    .Include(s => s.author)
                    .Include(s => s.set)
                    .Include(s => s.instrument)
                    .Include(s => s.instrument.type)
                    .Include(s => s.lands);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Scan>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return scans
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<Scan> Get(long id) {
            var query = from scan in scans
                where scan.id == id
                select scan;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpPost]
        public ActionResult<Scan> Post(Scan scan) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            context.Scans.Add(scan); 
            context.SaveChanges(); 
            return scan;
        }

        [HttpPut("{id:long}")]
        public ActionResult<Scan> Put(long id, Scan updated) {
            var query = from s in scans
                where s.id == id
                select s;

            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            var scan = query.First();
            foreach(PropertyInfo prop in scan.editableProperties) {
                var oldValue = prop.GetValue(scan);
                var newValue = prop.GetValue(updated);

                if(newValue != null && newValue != oldValue) {
                    prop.SetValue(scan, newValue);
                }
            }

            context.Scans.Update(scan);
            context.SaveChanges();
            return scan;
        }

        [HttpDelete("{id:long}")]
        public ActionResult<Scan> Delete(long id) {
            var query = from s in context.Scans
                where s.id == id
                select s;
            
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var scan = query.First();
            context.Scans.Remove(scan);
            context.SaveChanges();
            return scan;
        }
    }
}