using Microsoft.EntityFrameworkCore;

namespace GymProject.Data;

public static class DataExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<GymContext>();
        await DbContext.Database.MigrateAsync();

    }
}
