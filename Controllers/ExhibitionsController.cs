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
    public class ExhibitionsController : Controller
    {
        private readonly ExhibitionContext _context;

        public ExhibitionsController(ExhibitionContext context)
        {
            _context = context;
        }

        // GET: api/Exhibitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exhibition>>> GetExhibition()
        {
            return await _context.Exhibition.ToListAsync();
        }

        // GET: api/Exhibitions/midex/5
        [HttpGet("midex/{id}")]
        public async Task<ActionResult<IEnumerable<Exhibition>>> GetByUserid(int id)
        {
            var exhibition = await _context.Exhibition
                                .Where(b => b.midex == id)
                                .ToListAsync();

            return exhibition;
        }

        // GET: api/Exhibitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exhibition>> GetExhibition(int id)
        {
            var exhibition = await _context.Exhibition.FindAsync(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            return exhibition;
        }

        // PUT: api/Exhibitions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExhibition(int id, Exhibition exhibition)
        {
            if (id != exhibition.midex)
            {
                return BadRequest();
            }

            _context.Entry(exhibition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitionExists(id))
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

        // POST: api/Exhibitions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Exhibition>> PostExhibition(Exhibition exhibition)
        {
            _context.Exhibition.Add(exhibition);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExhibitionExists(exhibition.midex))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExhibition", new { id = exhibition.midex }, exhibition);
        }

        // DELETE: api/Exhibitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exhibition>> DeleteExhibition(int id)
        {
            var exhibition = await _context.Exhibition.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }

            _context.Exhibition.Remove(exhibition);
            await _context.SaveChangesAsync();

            return exhibition;
        }

        private bool ExhibitionExists(int id)
        {
            return _context.Exhibition.Any(e => e.midex == id);
        }




        //the following is about museum guide subsystem
        //Get: MuseumTourBackEnd/Exhibition/AllExhibitions 返回所有的展览
        [HttpGet]
        [Route("AllExhibitions")]
        public JsonResult AllExhibitions()
        {
            return Json(new { _context.Exhibition });
        }

        //Post: MuseumTourBackEnd/Exhibition/ExhibitionsOfMidex 根据博物馆id查询该馆展览
        [HttpPost]
        [Route("ExhibitionsOfMidex")]
        public JsonResult ExhibitionsOfMidex([FromBody] Exhibition E)
        {
            var search = _context.Exhibition.Where(e => e.midex == E.midex);
            return Json(new { search });
        }

        //Post: MuseumTourBackEnd/Exhibition/SearchByeid 根据展览id查询该展览其他信息
        [HttpPost]
        [Route("SearchByeid")]
        public JsonResult SearchByeid([FromBody] Exhibition E)
        {
            int Midex = 0;
            var Ename = "Not found";
            var Eintro = "Not found";
            var search = _context.Exhibition.FirstOrDefault(m => m.eid == E.eid);
            if (search != null)
            {
                Midex = search.midex;
                Ename = search.ename;
                Eintro = search.eintro;
            }
            return Json(new { midex = Midex, ename = Ename, eintro = Eintro });
        }

        //Get: MuseumTourBackEnd/Exhibition/SearchByName 根据关键字模糊查询所有名称符合的展览
        [HttpGet]
        [Route("SearchByName")]
        public JsonResult SearchByName([FromBody] Exhibition E)
        {
            var search = _context.Exhibition.Where(e => e.ename.Contains(E.ename));
            return Json(new { search });
        }
    }
}
