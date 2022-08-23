using FactChallenge.Infrastructure.Core.Interfaces;
using FactChallenge.Infrastructure.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactChallenge.Infrastructure.Core.Services
{
    public class FactService : IFactService
    {
        private readonly IDataPath dataFilePath;
        public FactService(IDataPath dataFilePath)
        {
            this.dataFilePath = dataFilePath ?? throw new ArgumentNullException(nameof(dataFilePath));
        }

        /// <summary>
        /// Get list of facts
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public async Task<List<CatFactModel>> GetFacts(int maxLength, int limit)
        {
            string filePath = GetJsonFilePath();

            string jsonBody = File.ReadAllText(filePath);

            CatFactJsonModel factModel = JsonConvert.DeserializeObject<CatFactJsonModel>(File.ReadAllText(filePath)) ?? new CatFactJsonModel();

            var getFacts = factModel.Facts.Skip((limit - 1) * maxLength).Take(maxLength).ToList();

            return await Task.FromResult(getFacts);
        }
        /// <summary>
        /// Get random facts
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public async Task<CatFactModel?> GetRandomFacts(int maxLength)
        {
            string filePath = GetJsonFilePath();
            string jsonBody = File.ReadAllText(filePath);

            CatFactJsonModel factModel = JsonConvert.DeserializeObject<CatFactJsonModel>(File.ReadAllText(filePath)) ?? new CatFactJsonModel();

            Random random = new();

            var catFactData = factModel.Facts.Where(max => max.Length <= maxLength);

            if (catFactData.Count() <=0)
                return null;


            int randomValue = random.Next(1, catFactData.Count());

            var getRandomFact = catFactData.OrderBy(x => Guid.NewGuid()).Take(randomValue).FirstOrDefault();

            return await Task.FromResult(getRandomFact);
        }

        /// <summary>
        ///  create fact
        /// </summary>
        /// <param name="fact"></param>
        /// <returns></returns>
        public async Task<CatFactModel?> CreateFact(CatFactModel fact)
        {
            try
            {
                string filePath = GetJsonFilePath();
                string jsonBody = File.ReadAllText(filePath);

                CatFactJsonModel factModel = JsonConvert.DeserializeObject<CatFactJsonModel>(jsonBody) ?? new CatFactJsonModel();


                if (fact != null)
                {
                    factModel.Facts.Add(fact);
                    string json = JsonConvert.SerializeObject(factModel, Formatting.None);
                    File.WriteAllText(filePath, json);
                    return await Task.FromResult(fact);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }




        }

        private string GetJsonFilePath()
        {
            string? path = dataFilePath.GetFilePath();
            return path ?? "";

            //return string.Concat(path, "/Data/factchallenge.json");
        }
    }
}
