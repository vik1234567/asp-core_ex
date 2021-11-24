using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class FileReadAsyncTest
    {
        private List<User3> _lstUsers;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, @"Data/Users1000.json"));
            _lstUsers = (await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User3>>(json))).ToList();
            _lstUsers = _lstUsers.Take(10).ToList();
        }

        [Benchmark]
        public List<User3> GetUser3_GroupByAsync()
        {
            return _lstUsers
                .GroupBy(n => n.gender)
                .Select(x => x.First())
                .ToList();
        }
        //[Benchmark]
        //public List<User3> GetUser3_Ml_GroupByAsync()
        //{
        //    return _lstUsers
        //        .DistinctBy(n => n.gender)
        //        .ToList();
        //}

        //[Benchmark]
        //public List<User3> GetUser3_OrderByAsync()
        //{
        //    return _lstUsers.OrderBy(n => n.gender).ToList();
        //}

        //[Benchmark]
        //public List<User3> GetUser3_ContainsAsync()
        //{
        //    return _lstUsers.OrderBy(n => n.name.Contains("an")).ToList();
        //}
    }
}