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
    builder.Services.AddDbContext<TeamDb>(options => options.UseNpgsql(connectionString));
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
    app.MapPost("/juego/equipo/", async (Equipo e, TeamDb db) =>
    {
        db.Equipos.Add(e);
        await db.SaveChangesAsync();
       
        return Results.Created($"/player/equipo/{e.Id}", e);

    });
    app.MapPut("/juego/equipo/{id:int}", async (int id, Equipo e, TeamDb db) =>
    {
        if (e.Id != id)
        {
            return Results.NotFound();
        }
        var jugar = await db.Equipos.FindAsync(id);

        if (jugar is null) return Results.NotFound();
        jugar.idjugador1 = e.idjugador1;
        jugar.email1 = e.email1;
        jugar.idjugador2 = e.idjugador2;
        jugar.email2 = e.email2;
        jugar.punteoequipo1 = e.punteoequipo1;
        jugar.punteoequipo2 = e.punteoequipo2;
        jugar.idjugador3 = e.idjugador3;
        jugar.email3 = e.email3;
        jugar.idjugador3 = e.idjugador3;
        jugar.email3 = e.email3;
        jugar.idjugador4= e.idjugador4;
        jugar.email4 = e.email4;
        if (e.idjugador1 != 0)
        {
            if (e.idjugador2 != 0)
            {
                if (e.idjugador3 != 0)
                {
                    if (e.idjugador4 != 0)
                    { 
                        if(e.idjugador1==e.idjugador2||e.idjugador1==e.idjugador3||e.idjugador1==e.idjugador4)
                        {
                            return Results.BadRequest();
                        }
                        if (e.idjugador2 == e.idjugador3 || e.idjugador2 == e.idjugador4 )
                        {
                            return Results.BadRequest();
                        }
                        if (e.idjugador3== e.idjugador4)
                        {
                            return Results.BadRequest();
                        }

                    }
                }
            }
        }

        await db.SaveChangesAsync();
        return Results.Ok(jugar);

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
