using LibraryV4.Application.Services;
using LibraryV4.Application.Services.Library.Services;
using LibraryV4.Domain;
using LibraryV4.Domain.Interfaces;
using LibraryV4.Infrastructure;
using LibraryV4.Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi koneksi ke PostgreSQL
var connectionStringPostgre = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<libraryContext>(options => options.UseNpgsql(connectionStringPostgre));

// Konfigurasi BookLoanSettings
builder.Services.Configure<BookLoanSettings>(builder.Configuration.GetSection("BookLoanSettings"));
builder.Services.ConfigurePersistence(builder.Configuration);

// Registrasi repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPeminjamanRepository, PeminjamanRepository>();


// Registrasi layanan
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPeminjamanService, PinjamService>();

var app = builder.Build();

// Konfigurasi pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();