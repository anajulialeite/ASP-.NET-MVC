using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar a conex�o com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar suporte a controllers e views
builder.Services.AddControllersWithViews();

// Registrar reposit�rios
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();

// Registrar CarrinhoCompra
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

// Registrar IHttpContextAccessor (obrigat�rio para CarrinhoCompra)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configurar a sess�o
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".LanchesMac.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura��o do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // importante: sess�o antes de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
