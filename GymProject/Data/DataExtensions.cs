using Microsoft.EntityFrameworkCore;

namespace GymProject.Data;

public static class DataExtensions
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<GymContext>();
        DbContext.Database.Migrate();

    }
}
