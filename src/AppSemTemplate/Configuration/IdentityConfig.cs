using AppSemTemplate.Data;
using Microsoft.AspNetCore.Identity;

namespace AppSemTemplate.Configuration
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            #region Configurando o Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>();
            #endregion

            builder.Services.AddAuthorization(options =>
            {
                // Autorizacao por papel (role)
                options.AddPolicy("PodeExcluir", policy => policy.RequireRole("Admin"));

                // Autorizacao por claim (ação)
                options.AddPolicy("VerProdutos", policy => policy.RequireClaim("Produtos", "VI"));
            });

            return builder;
        }
    }
}
