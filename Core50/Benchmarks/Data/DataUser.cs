using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Benchmarks
{
    public static class DataUser
    {
        public static IEnumerable<User3> GetUsers1000()
        {
            return JsonConvert.DeserializeObject<IEnumerable<User3>>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"Data/Users1000.json"))).ToList();
        }

        public static string GetStrUsers1000()
        {
            return File.ReadAllText(Path.Combine(AppContext.BaseDirectory, @"Data/Users1000.json")) ?? "";
        }
    }
}