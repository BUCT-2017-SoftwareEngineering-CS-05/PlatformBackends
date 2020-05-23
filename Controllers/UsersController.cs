using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPBackends.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPBackends.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.userid)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.userid }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.userid == id);
        }




        //the following is about museum guide subsystem
        //POST: MuseumTourBackEnd/User/Create
        [HttpPost]
        [Route("Create")]
        public JsonResult Create([FromBody] User newUser)
        {
            var searchUser = _context.User.FirstOrDefault(
                m => m.userid == newUser.userid);
            int flag = 1;
            if (searchUser == null)
            {
                newUser.coright = 1;
                _context.User.Add(newUser);
                _context.SaveChanges();
            }
            else
            {
                flag = 0;
            }
            var returnMesg = new { status = flag };
            return Json(returnMesg);
        }

        //POST: MuseumTourBackEnd/User/Loogin
        [HttpPost]
        [Route("Login")]
        public JsonResult Login([FromBody] JObject jsonObj)
        {
            //序列化JObject对象为Json字符串
            var jsonStr = JsonConvert.SerializeObject(jsonObj);
            //反序列化Json字符串为动态Object
            var objParams = JsonConvert.DeserializeObject<dynamic>(jsonStr);

            string userid = objParams.userid;
            string userpwd = objParams.userpwd;
            var searchUser = _context.User.FirstOrDefault(
                m => m.userid == userid && m.userpwd == userpwd);

            if (searchUser != null)
            {
                return Json(new { status = 1 });
            }
            else
            {
                return Json(new { status = 0 });
            }

        }

        //GET: MuseumTourBackEnd/User/Details
        [HttpGet]
        [Route("Details")]
        public JsonResult Details([FromQuery] string userid)
        {
            var searchUser = _context.User.FirstOrDefault(
                m => m.userid == userid);
            if (searchUser != null)
            {
                return Json(new
                {
                    status = 1,
                    id = searchUser.userid,
                    pwd = searchUser.userpwd,
                    coright = searchUser.coright
                });
            }
            else
            {
                return Json(new { status = 0 });
            }
        }

        //GET: MuseumTourBackEnd/User/AllDetails
        [HttpGet]
        [Route("AllDetails")]
        public JsonResult AllDetails()
        {
            return Json(_context.User);
        }

        //POST: MuseumTourBackEnd/User/Modify
        [HttpPost]
        [Route("Modify")]
        public JsonResult Modify([FromBody] User changeUser)
        {
            var searchUser = _context.User.Find(changeUser.userid);
            int flag = 1;
            if (searchUser == null)
            {
                flag = 0;
            }
            else
            {
                searchUser.nickname = changeUser.nickname;
                searchUser.userpwd = changeUser.userpwd;
                _context.SaveChanges();
            }
            return Json(new { status = flag });
        }

        //GET: MuseumTourBackEnd/User/Delete
        [HttpGet]
        [Route("Delete")]
        public JsonResult Delete([FromQuery] string userid)
        {
            var searchUser = _context.User.Find(userid);
            int flag = 1;
            if (searchUser != null)
            {
                _context.User.Remove(searchUser);
                _context.SaveChanges();
            }
            else
            {
                flag = 0;
            }
            return Json(new { status = flag });
        }

        //GET: MuseumTourBackEnd/User/Index
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "dajfdjfsdbfj";
        }
    }
}
