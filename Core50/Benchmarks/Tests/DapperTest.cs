using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class DapperTest
    {
        private List<User3> _lstUsers;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
        }

        [Benchmark]
        public void GetUser_Select()
        {

        }

        /*
        [Benchmark]
        public int DapperInsert()
        {
            using (var conn = new SqlConnection("Data Source=.;Integrated Security=True;Initial Catalog=freesqlTest;Pooling=true;Min Pool Size=21;Max Pool Size=31"))
            {
                foreach (var song in songs)
                {
                    Dapper.SqlMapper.Execute(conn, @$"insert into dapper_song(Create_time,Is_deleted,Title,Url) 
values('{song.Create_time.Value.ToString("yyyy-MM-dd HH:mm:ss")}',{(song.Is_deleted == true ? 1 : 0)},'{song.Title}','{song.Url}')");
                }
            }
            return songs.Count();
        }
       
        [Benchmark]
        public int DapperUpdate()
        {
            using (var conn = new SqlConnection("Data Source=.;Integrated Security=True;Initial Catalog=freesqlTest;Pooling=true;Min Pool Size=21;Max Pool Size=31"))
            {
                foreach (var song in songs)
                {
                    Dapper.SqlMapper.Execute(conn, @$"update dapper_song set
Create_time = '{song.Create_time.Value.ToString("yyyy-MM-dd HH:mm:ss")}',
Is_deleted = {(song.Is_deleted == true ? 1 : 0)},
Title = '{song.Title}',
Url = '{song.Url}'
where id = {song.Id}");
                }
            }
            return songs.Count();
        }

        [Benchmark]
        public List<Song> DapperSelete()
        {
            using (var conn = new SqlConnection("Data Source=.;Integrated Security=True;Initial Catalog=freesqlTest;Pooling=true;Min Pool Size=21;Max Pool Size=31"))
            {
                return Dapper.SqlMapper.Query<Song>(conn, $"select top {size} Id,Create_time,Is_deleted,Title,Url from dapper_song").ToList();
            }
        }
         */

    }
}