using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApiVerJSONAbbrDiffSample.Models.v1;

namespace ApiVerJSONAbbrDiffSample.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Library> Get()
        {
            return new List<Library> {
                new Library() {
                    Books = new List<string>() { "Suç ve Ceza", "Hobbit" },
                    EstablishedAt = DateTime.Now,
                    Librarian = "Necati",
                    Name = "Atatürk Kütüphanesi" }
            };
        }



    }
}
