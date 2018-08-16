using ApiVerJSONAbbrDiffSample.Abbreviations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVerJSONAbbrDiffSample.Abbreviations
{
    public class V1Mapping : IMapper
    {
        public string Name = "name";
        public string EstablishedAt = "establishedAt";
        public string Librarian = "librarian";
        public string Books = "books";
    }
}
