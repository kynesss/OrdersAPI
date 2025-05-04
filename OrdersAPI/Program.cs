using OrdersAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDatabaseConnection(builder.Configuration);

builder.Services.AddIdentity();
builder.Services.AddSeeders();
builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddServices();
builder.Services.AddMiddlewares();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();
await app.UseSeeders();

app.UseMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();