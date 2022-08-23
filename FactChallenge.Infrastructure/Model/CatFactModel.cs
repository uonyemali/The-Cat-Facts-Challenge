using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactChallenge.Infrastructure.Model
{
    public class CatFactModel
    {
        [Required, JsonProperty("fact")]
        public string? Fact { get; set; }
        [Required, JsonProperty("length")]
        public int Length { get; set; }
    }
}
