using Microsoft.EntityFrameworkCore;

namespace RTS.EntityFramework;

public class RecruitmentDbContextFactory
{
    private readonly Action<DbContextOptionsBuilder> _configureDbContext;
    
    public RecruitmentDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
    {
        _configureDbContext = configureDbContext;
    }
    public RecruitmentDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<RecruitmentDbContext> optionsBuilder = new DbContextOptionsBuilder<RecruitmentDbContext>();
        _configureDbContext(optionsBuilder);
        return new RecruitmentDbContext(optionsBuilder.Options);
    }
}