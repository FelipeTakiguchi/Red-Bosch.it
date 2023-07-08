using Security.Jwt;
using backend.Repositories;
using backend.Model;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
    policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

builder.Services.AddTransient<IPasswordHasher, BasicPasswordHasher>();
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddTransient<IRepository<ImageDatum>, ImageRepository>();
builder.Services.AddScoped<RedBoschContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IImageRepository, ImageRepository>(); 
builder.Services.AddTransient<IForumRepository, ForumRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();
builder.Services.AddTransient<IUsuarioCargoRepository, UsuarioCargoRepository>();
builder.Services.AddTransient<IUsuarioForumRepository, UsuarioForumRepository>();
builder.Services.AddTransient<IVoteRepository, VoteRepository>();

builder.Services.AddTransient<IPasswordProvider>(p =>{
    return new PasswordProvider("minhasenhaultrasecreta");
});



var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
