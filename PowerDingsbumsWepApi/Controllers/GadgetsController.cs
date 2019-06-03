using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerDingsbumsModel;

namespace PowerDingsbumsWepApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GadgetsController : ControllerBase {
        private static Dictionary<Guid, Gadget> _Items;

        private static Dictionary<Guid, Gadget> getItems() {
            if (_Items == null) {
                _Items = new Dictionary<Guid, Gadget>();
                {
                    var gadget = new Gadget() {
                        Id = new Guid("11111111-1111-1111-1111-111111111111"),
                        Name = "eins"
                    };
                    _Items.Add(gadget.Id, gadget);
                }
                {
                    var gadget = new Gadget() {
                        Id = new Guid("22222222-2222-2222-2222-222222222222"),
                        Name = "zwei"
                    };
                    _Items.Add(gadget.Id, gadget);
                }
            }
            return _Items;
        }
        // GET api/Gadgets
        [HttpGet]
        public ActionResult<IEnumerable<Gadget>> Get() {
            return getItems().Values;
        }

        // GET api/Gadgets/5
        [HttpGet("{id}")]
        public ActionResult<Gadget> Get(Guid id) {
            if (getItems().TryGetValue(id, out var result)) {
                return result;
            } else {
                return this.NotFound();
            }
        }

        // POST api/Gadgets
        [HttpPost]
        public void Post([FromBody] Gadget value) {
            if (value.Id == Guid.Empty) {
                value.Id = Guid.NewGuid();
            }
            var items = getItems();
            items[value.Id] = value;
        }

        // PUT api/Gadgets/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Gadget value) {
            if (id == Guid.Empty) {
                //
            } else {
                var items = getItems();
                value.Id = id;
                items[id] = value;
            }
        }

        // DELETE api/Gadgets/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) {
            var items = getItems();
            items.Remove(id);
        }
    }
}
