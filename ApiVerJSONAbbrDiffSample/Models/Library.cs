using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVerJSONAbbrDiffSample.Models.v1
{
    public class Library
    {
        public string Name { get; set; }
        public DateTime EstablishedAt { get; set; }
        public string Librarian { get; set; }
        public List<string> Books { get; set; }
    }
}
