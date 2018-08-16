using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiVerJSONAbbrDiffSample.Abbreviations
{
    public class MyMapper
    {
        private readonly IMapper _mapper;

        public MyMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Dictionary<string, string> GetAbbreviations()
        {
            return _mapper.GetType()
             .GetFields()
             .ToDictionary(prop => prop.Name, prop => prop.GetValue(_mapper).ToString());
        }


    }
}
