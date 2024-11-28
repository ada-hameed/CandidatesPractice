
using Candidates_Project.Implementation;
using Candidates_Project.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICandidateRepo, CandidateRepo>();
builder.Services.AddTransient<IAdmin_UserRepo, Admin_UserRepo>();

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", cp =>
    {
        cp.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
var app = builder.Build();


#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.UseCors("AllowAll");

app.Run();

