using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura diferentes arquivos de configuração de acordo com o ambiente
if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Local.json")))
{
    // Se estiver executando localmente e o arquivo appsettings.Local.json existir, use-o
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
    Console.WriteLine("Usando configuração local (appsettings.Local.json)");
}

// Configurar a conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
        sqlServerOptionsAction: sqlOptions => 
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// Adicionar suporte a controllers e views
builder.Services.AddControllersWithViews();

// Registrar repositórios
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();

// Registrar CarrinhoCompra
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

// Registrar IHttpContextAccessor (obrigatório para CarrinhoCompra)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configurar a sessão
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".LanchesMac.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Aplicar as migrações de acordo com o ambiente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    var env = app.Environment;
    var configuration = services.GetRequiredService<IConfiguration>();
    var runningInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    
    // Verifica se o banco deve ser inicializado pelo script SQL (em Docker)
    var initDatabaseWithScript = runningInDocker && 
                                 configuration.GetValue<bool>("INIT_DATABASE", false);
    
    try
    {
        if (initDatabaseWithScript)
        {
            // Em ambiente Docker, o script init-db.sh já criou as tabelas e registrou as migrações
            // Verificamos se há migrações pendentes (novas) para aplicar
            var pendingMigrations = dbContext.Database.GetPendingMigrations().ToList();
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"Detectadas {pendingMigrations.Count} migrações novas. Aplicando...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migrações aplicadas com sucesso.");
            }
            else
            {
                Console.WriteLine("Banco de dados já inicializado pelo script SQL. Nenhuma migração pendente.");
            }
        }
        else
        {
            // Em ambiente de desenvolvimento local, usamos o EF Core Migrations normalmente
            Console.WriteLine("Aplicando migrações do Entity Framework Core...");
            dbContext.Database.Migrate();
            Console.WriteLine("Migrações aplicadas com sucesso.");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao aplicar as migrations.");
    }
}

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // importante: sessão antes de Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
