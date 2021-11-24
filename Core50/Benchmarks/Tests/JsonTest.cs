using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class JsonTest
    {
        private List<User3> _lstUsers;
        private string _json;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            _json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, @"Data/Users1000.json"));
            //_json = DataUser.GetStrUsers1000();
            _lstUsers = (await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User3>>(_json))).ToList();
            //_lstUsers = _lstUsers.Take(10).ToList();
        }

        [Benchmark]
        public int SerializeTypeNew()
        {
            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                //WriteIndented = true
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize<List<User3>>(_lstUsers, options);
            return jsonString.Length;
        }

        [Benchmark]
        public int SerializeType()
        {
            string jsonString = JsonConvert.SerializeObject(_lstUsers);
            return jsonString.Length;
        }

        //[Benchmark]
        //public List<User3> DeserializeType()
        //{
        //    var users = JsonConvert.DeserializeObject<List<User3>>(_json);
        //    return users;
        //}

        //[Benchmark]
        //public List<User3> DeserializeDynamic()
        //{
        //    var users = JsonConvert.DeserializeObject<dynamic>(_json);
        //    return (users as List<User3>);
        //}   
        
        
        //[Benchmark]
        //public List<User3> DeserializeTypeNew()
        //{
        //    var users = System.Text.Json.JsonSerializer.Deserialize<List<User3>>(_json);
        //    return users;
        //}

        //[Benchmark]
        //public List<User3> DeserializeDynamicNew()
        //{
        //    var users = System.Text.Json.JsonSerializer.Deserialize<dynamic>(_json);
        //    return (users as List<User3>);
        //}

        //[Benchmark]
        //public List<User3> DeserializeDynamicInt()
        //{
        //    var users = JsonConvert.DeserializeObject<dynamic>(_json);
        //    return (List<User3>)users;
        //}
    }
}