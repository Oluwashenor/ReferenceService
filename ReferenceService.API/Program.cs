#if (UseCosmosDb)
using Microsoft.Azure.Cosmos;
using ReferenceService.API.Data;
#endif

#if (UseSqlServer)
using Microsoft.EntityFrameworkCore;
using ReferenceService.API.Data.Sql;
using ReferenceService.API.Data;
#endif
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


#if (UseSqlServer)
#region Sql Server Configuration

builder.Services.AddDbContext<ReferenceServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


builder.Services.AddScoped<ITodoRepository, ReferenceService.Api.Data.Sql.TodoRepository>();

#endregion
#endif

#if (UseCosmosDb)
#region CosmosDb Configuration

var cosmosClient = new CosmosClient(builder.Configuration.GetConnectionString("CosmosDbConnectionString"), new CosmosClientOptions
{
    SerializerOptions = new CosmosSerializationOptions
    {
        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
    },
    ConnectionMode = ConnectionMode.Direct,
    EnableContentResponseOnWrite = false
});

builder.Services.AddSingleton(cosmosClient);
builder.Services.AddScoped<ITodoRepository, ReferenceService.Api.Data.NoSql.TodoRepository>();

#endregion
#endif

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
#if(UseSwagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endif
var app = builder.Build();

// Configure the HTTP request pipeline.

#if(UseSwagger)
    app.UseSwagger();
    app.UseSwaggerUI();
#endif

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
