using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bear.Web.Entities;

namespace Bear.Web.Controllers {

    [Route("api/[controller]")]
    public class FoosController : Controller {

        private readonly BearContext _context;

        public FoosController(BearContext context) {
            _context = context;
        }

        // GET api/foos
        [HttpGet]
        public IEnumerable<Foo> Get() {
            return _context.Foos.ToList();
        }

        // GET api/foos/5
        [HttpGet("{id}")]
        public Foo Get(int id) {
            return _context.Foos.First(f => f.Id == id);
        }

        // POST api/foos
        [HttpPost]
        public Foo Post([FromBody]Foo foo) {
            _context.Add(foo);
            _context.SaveChanges();
            return foo;
        }

        // PUT api/foos/5
        [HttpPut("{id}")]
        public Foo Put(int id, [FromBody]Foo foo) {
            var oldFoo = _context.Foos.First(f => f.Id == id);
            oldFoo.Bar = foo.Bar;
            _context.SaveChanges();
            return oldFoo;
        }

        // DELETE api/foos/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            var oldFoo = _context.Foos.First(f => f.Id == id);
            _context.Foos.Remove(oldFoo);
            _context.SaveChanges();
        }
    }
}
