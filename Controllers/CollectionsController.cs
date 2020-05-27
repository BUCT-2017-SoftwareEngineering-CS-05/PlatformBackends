using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using MPBackends.Models;

namespace MPBackends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : Controller
    {
        private readonly CollectionContext _context;

        public CollectionsController(CollectionContext context)
        {
            _context = context;
        }

        // GET: api/Collections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollection()
        {
            return await _context.Collection.ToListAsync();
        }

        // GET: api/Collections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
            var collection = await _context.Collection.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // GET: api/Collections/midex/5
        [HttpGet("midex/{id}")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetByUserid(int id)
        {
            var collection = await _context.Collection
                                .Where(b => b.midex == id)
                                .ToListAsync();

            return collection;
        }

        // PUT: api/Collections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.midex)
            {
                return BadRequest();
            }

            _context.Entry(collection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Collections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
            _context.Collection.Add(collection);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CollectionExists(collection.midex))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCollection", new { id = collection.midex }, collection);
        }

        // DELETE: api/Collections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Collection>> DeleteCollection(int id)
        {
            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            _context.Collection.Remove(collection);
            await _context.SaveChangesAsync();

            return collection;
        }

        private bool CollectionExists(int id)
        {
            return _context.Collection.Any(e => e.midex == id);
        }





        //the following is about museum guide subsysytem
        //GET:  MuseumTourBackEnd/Collection/CollectionList
        [HttpGet]
        [Route("CollectionList")]
        public JsonResult CollectionList([FromQuery] int midex)
        {
            return Json(_context.Collection.Where(m => m.midex == midex)
                .Select(m => new { oid = m.oid, oname = m.oname }).ToList());
        }

        //GET: MuseumTourBackEnd/Collection/SingleColleciotnInstruction
        [HttpGet]
        [Route("SingleColleciotnInstruction")]
        public JsonResult SingleColleciotnInstruction([FromQuery] int oid)
        {
            return Json(_context.Collection.FirstOrDefault(m => m.oid == oid));
        }

        //GET: MuseumTourBackEnd/Collection/CollectionAllDetails
        [HttpGet]
        [Route("CollectionAllDetails")]
        public JsonResult CollectionAllDetails([FromQuery] int midex)
        {
            return Json(new {
                content = _context.Collection.Where(m => m.midex == midex).ToList()
            });
        }
    }
}
