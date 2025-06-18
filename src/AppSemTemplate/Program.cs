using AppSemTemplate.Data;
using AppSemTemplate.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

//builder.Services.AddRouting(options=>   
//    options.ConstraintMap["slugfy"] = typeof(RouteSlugfyParameterTransformer));

// Adiciona acessor do contexto http
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Configura contexto de banco de dados
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Acesso a arquivos estaticos
app.UseStaticFiles();

// Roteamento
app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller:slugfy=Home}/{action:slugfy=Index}/{id?}");

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
