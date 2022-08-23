using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactChallenge.Infrastructure.Model
{
    public class CatFactJsonModel
    {
        [JsonProperty("facts")]
        public List<CatFactModel> Facts { get; set; }

        public CatFactJsonModel()
        {
            Facts = new List<CatFactModel>();
        }
    }
}
