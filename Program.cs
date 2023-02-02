using Autofac;
using Autofac.Extensions.DependencyInjection;
using chessAPI;
using chessAPI.business.interfaces;
using chessAPI.models;
using chessAPI.models.player;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

//Serilog logger (https://github.com/serilog/serilog-aspnetcore)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("chessAPI starting");
    var builder = WebApplication.CreateBuilder(args);

    var connectionStrings = new connectionStrings();
    builder.Services.AddOptions();
    builder.Services.Configure<connectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
    //builder.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    builder.Services.AddDbContext<PlayerDb>(options =>options.UseNpgsql(connectionString));
    builder.Services.AddDbContext<GameDB>(options => options.UseNpgsql(connectionString));
    // Two-stage initialization (https://github.com/serilog/serilog-aspnetcore)
    builder.Host.UseSerilog((context, services, configuration) => configuration.ReadFrom
             .Configuration(context.Configuration)
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning).ReadFrom
             .Services(services).Enrich
             .FromLogContext().WriteTo
             .Console());

    // Autofac como inyección de dependencias
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new chessAPI.dependencyInjection<int, int>()));
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    //app.UseMiddleware(typeof(chessAPI.customMiddleware<int>));
    app.MapGet("/", () =>
    {
        return "hola mundo";
    });

    app.MapPost("/player/", async (Jugador e,PlayerDb db)  =>
    {
        db.players.Add(e);
        await db.SaveChangesAsync();
        return Results.Created($"/player/{e.id}", e);
    
    });

    app.MapPost("/juego/", async (Juego e, GameDB db) =>
    {
        db.partido.Add(e);
        await db.SaveChangesAsync();
        return Results.Created($"/player/{e.Id}", e);

    });
    app.MapGet("/player/{id:int}", async(int id, PlayerDb db)=>
    { 
        return await db.players.FindAsync(id)
            is Jugador e
            ? Results.Ok(e)
            :Results.NotFound();
    });
    app.MapGet("/juego/{id:int}", async (int id, GameDB db) =>
    {
        return await db.partido.FindAsync(id)
            is Juego e
            ? Results.Ok(e)
            : Results.NotFound();
    });
    app.MapGet("/player", async (PlayerDb db) => await db.players.ToListAsync());
    app.MapGet("/juego", async (GameDB db) => await db.partido.ToListAsync());

    app.MapPut("/player/{id:int}", async (int id, Jugador e, PlayerDb db) =>
    {
        if (e.id != id)
        { 
        return Results.BadRequest();
        }
        var jugar=await db.players.FindAsync(id);

        if(jugar is null) return Results.NotFound();
        jugar.email= e.email;

        await db.SaveChangesAsync();
        return Results.Ok(jugar);

    });
    app.MapPut("/juego/{id:int}", async (int id, Juego e, GameDB db) =>
    {
        if (e.Id != id)
        {
            return Results.BadRequest();
        }
        var jugar = await db.partido.FindAsync(id);

        if (jugar is null) return Results.NotFound();
        jugar.idhome = e.idhome;
        jugar.emailhome = e.emailhome;
        jugar.punteohome = e.punteohome;
        jugar.punteoaway = e.punteoaway;
        jugar.idaway = e.idaway;
        jugar.emailaway = e.emailaway;


        await db.SaveChangesAsync();
        return Results.Ok(jugar);

    });
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "chessAPI terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
