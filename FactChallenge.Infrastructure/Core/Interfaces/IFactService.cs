using FactChallenge.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactChallenge.Infrastructure.Core.Interfaces
{
    public interface IFactService
    {
        Task<List<CatFactModel>> GetFacts(int limit, int maxLength);
        Task<CatFactModel?> GetRandomFacts(int maxLength);
        Task<CatFactModel?> CreateFact(CatFactModel fact);
    }
}
