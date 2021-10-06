using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicTacToe_Data;
using TicTacToeWebApi.Hubs;
using TicTcToe_Service.Interfaces;
using TicTcToe_Service.Services;

namespace TicTacToeWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DB_Context>(opts =>
              opts.UseSqlServer(Configuration.GetConnectionString("Default"))
          );
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IGameService, GameService>();
            services.AddSignalR(e => {
                e.MaximumReceiveMessageSize = 102400000;
                e.EnableDetailedErrors = true;
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200").AllowCredentials();
            }));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chatsocket");
                endpoints.MapControllers();
            });
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DB_Context>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
