using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace RTS.EntityFramework;
public class SeedHistory
{
    public int Id { get; set; }
    public string? SeedName { get; set; }
    public DateTime SeedDate { get; set; }
}
public class DatabaseSeeder
{
    private readonly RecruitmentDbContext _context;
    private readonly ILogger<DatabaseSeeder> _logger;

    public DatabaseSeeder(RecruitmentDbContext context, ILogger<DatabaseSeeder> logger)
    {
        _context = context;
        _logger = logger;
    }

    public void Seed()
    {
        try
        {
            if (!IsDatabaseSeeded())
            {
                ExecuteSqlFile("recruitmentDBInsertFINAL.sql");
                MarkDatabaseAsSeeded();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }
    private bool IsDatabaseSeeded()
    {
        return _context.SeedHistory.Any(sh => sh.SeedName == "InitialSeed");
    }

    private void MarkDatabaseAsSeeded()
    {
      
        _context.SeedHistory.Add(new SeedHistory { SeedName = "InitialSeed", SeedDate = DateTime.UtcNow });
        _context.SaveChanges();
    }

    private void ExecuteSqlFile(string filePath)
    {
        using var transaction = _context.Database.BeginTransaction();
        var sql = File.ReadAllText(filePath);
        _context.Database.ExecuteSqlRaw(sql);
        transaction.Commit();
        _logger.LogInformation("SQL file executed successfully.");
    }
   
}