using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace RTS.EntityFramework;

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
            ExecuteSqlFile("recruitmentDBInsertFINAL.sql");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }
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