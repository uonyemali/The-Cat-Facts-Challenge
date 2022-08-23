using FactChallenge.Infrastructure.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactChallenge.Infrastructure.Core.DataSource
{
    public class DataPath : IDataPath
    {

        public string? GetFilePath()
        {
            string? env = Path.GetFullPath("Data/factchallenge.json");

            if (!Directory.Exists(Path.GetFullPath("Data")))
                Directory.CreateDirectory(Path.GetFullPath("Data"));

            if (!File.Exists(env))
                File.CreateText(env);

            return env;
        }
    }
}
