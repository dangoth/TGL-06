using Dapper;
using SqlExample;
using StackExchange.Redis;
using System.Data.SqlClient;

class Startup
{
    private static SqlConnection _connection;
    static async Task Main(string[] args)
    {
        var connectionString = "Server=127.0.0.1,1433;Database=model;User ID=sa;Password=Password123!;Integrated Security=false;Connection Timeout=120;Application Name=Quark;TrustServerCertificate=True;";
        //_connection = new SqlConnection(connectionString);
        //await TestDapper();
        //await TestEntityFramework();
        await TestRedis();
        //_connection.Dispose();
    }
    #region redis
    private static async Task TestRedis()
    {
        var connectionString = "localhost:6379,password=eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81";
        var redis = ConnectionMultiplexer.Connect(connectionString);
        var db = redis.GetDatabase();
        db.StringSet("TestKey", "TestValue");
        Console.WriteLine(db.StringGet("TestKey"));
        db.KeyDelete("TestKey");
    }
    #endregion

    #region EF
    private static async Task TestEntityFramework()
    {
        using var ctx = new DoggosContext("Server=127.0.0.1,1433;Database=model;User ID=sa;Password=Password123!;Integrated Security=false;Connection Timeout=120;");
        var doggo = new Doggo()
        {
            Id = 1,
            DateOfBirth = new DateTime(2020, 05, 31),
            Height = 50,
            Weight = 10,
            Name = "Buddy"
        };
        var doggoInfo = new DoggoInfo()
        {
            Id = 101,
            DoggoId = doggo.Id,
            OwnerName = "Joe Anonymous",
            Age = 3
        };
        ctx.Doggos.Add(doggo);
        ctx.DoggoInfos.Add(doggoInfo);
        await ctx.SaveChangesAsync();
    }
    #endregion

    #region Dapper
    private static async Task TestDapper()
    {
        if (_connection.State != System.Data.ConnectionState.Open)
        {
            _connection.Open();
        }
        if (!await IsTableCreatedAsync(DatabaseConstants.PuppiesTableName))
        {
            await CreatePuppiesTableAsync();
        }
        await PopulatePuppiesTableWithPuppies();
        Console.WriteLine($"Current number of rows in the db is {await GetPuppiesCountAsync()}");
        await DropPuppiesTable();
    }

    private async static Task DropPuppiesTable()
    {
        var result = await _connection.ExecuteAsync(Queries.DeletePuppiesTableQuery);
        if (result != -1)
        {
            throw new ApplicationException("Failed to drop puppies table");
        }
    }

    private async static Task<int> GetPuppiesCountAsync()
    {
        var result = await _connection.QuerySingleAsync<int>(Queries.CountPuppiesTableQuery);
        return result;
    }

    private async static Task PopulatePuppiesTableWithPuppies()
    {
        var puppies = new List<Puppy>
        {
            new Puppy()
            {
                Name = "Luna",
                BirthDate = new DateTime(2020, 01, 05),
                Id = 1,
                IsCute = true,
                IsGoodPuppy = true,
                PuppyLengthInCm = 15
            },
            new Puppy()
            {
                Name = "Max",
                BirthDate = new DateTime(2015, 02, 13),
                Id = 2,
                IsCute = true,
                IsGoodPuppy = true,
                PuppyLengthInCm = 23
            },
            new Puppy()
            {
                Name = "Charlie",
                BirthDate = new DateTime(2015, 10, 10),
                Id = 3,
                IsCute = true,
                IsGoodPuppy = true,
                PuppyLengthInCm = 40
            },
            new Puppy()
            {
                Name = "Koda",
                BirthDate = new DateTime(2021, 06, 24),
                Id = 4,
                IsCute = true,
                IsGoodPuppy = true,
                PuppyLengthInCm = 60
            },
            new Puppy()
            {
                Name = "Bear",
                BirthDate = new DateTime(2022, 08, 24),
                Id = 5,
                IsCute = true,
                IsGoodPuppy = true,
                PuppyLengthInCm = 5
            },
        };
        await _connection.ExecuteAsync(Queries.InsertPuppiesDataQuery, puppies);
    }

    private async static Task CreatePuppiesTableAsync()
    {
        var result = await _connection.ExecuteAsync(Queries.CreatePuppiesTableQuery);
        if (result != -1)
        {
            throw new ApplicationException("Failed to create puppies table");
        }
    }

    private async static Task<bool> IsTableCreatedAsync(string puppiesTableName)
    {
        var result = await _connection.QuerySingleAsync<bool>(Queries.CheckIfTableExistsQuery, new { TableName = puppiesTableName });
        return result;
    }
    

    public class Puppy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public double PuppyLengthInCm { get; set; }
        public bool IsCute { get; set; }
        public bool IsGoodPuppy { get; set; }
    }
    #endregion
}