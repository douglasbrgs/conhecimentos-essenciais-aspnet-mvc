using AppSemTemplate.Data;
using AppSemTemplate.Extensions;
using AppSemTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// MVC
//builder.Services.AddControllersWithViews();

//Proteção contra CSRF
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

// Adicionando suporte a mudança de convenção da rota das areas.
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});

//builder.Services.AddRouting(options=>   
//    options.ConstraintMap["slugfy"] = typeof(RouteSlugfyParameterTransformer));

// Adiciona acessor do contexto http
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddScoped<IOperacao, Operacao>();

builder.Services.AddTransient<IOperacaoTransient, Operacao>();
builder.Services.AddScoped<IOperacaoScoped, Operacao>();
builder.Services.AddSingleton<IOperacaoSingleton, Operacao>();
builder.Services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));

builder.Services.AddTransient<OperacaoService>();

// Configura contexto de banco de dados
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region HSTS
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(60);
    options.ExcludedHosts.Add("example.com");
    options.ExcludedHosts.Add("www.example.com");
});
#endregion

var app = builder.Build();

#region HSTS e HTTPS
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
#endregion

// Acesso a arquivos estaticos
app.UseStaticFiles();

// Roteamento
app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller:slugfy=Home}/{action:slugfy=Index}/{id?}");

// Rota generica para Areas
//app.MapControllerRoute(
//  name: "areas",
//  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//);

// Rota de areas especializadas
app.MapAreaControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
app.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Gestao}/{action=Index}/{id?}");

// Rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Acessando O container DI
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var singService = services.GetRequiredService<IOperacaoSingleton>();

    Console.WriteLine("Direto do Program.cs: " + singService.OperacaoId);
}

app.Run();
