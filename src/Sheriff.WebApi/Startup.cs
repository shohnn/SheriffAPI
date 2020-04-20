using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;
using Sheriff.Application.UseCases;
using Sheriff.Domain.Contracts;
using Notifications = Sheriff.Domain.Notifications;
using Sheriff.Infrastructure.Mail;
using Sheriff.Infrastructure.Repositories;
using Sheriff.WebApi.Middlewares;

namespace Sheriff.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureRepositories(services);
            ConfigureUseCases(services);
            ConfigureNotifications(services);
            ConfigureMail(services);

            services.AddControllers();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddDbContext<SheriffContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddScoped<IBandRepository, AsyncEFBandRepository>();
            services.AddScoped<IRoundRepository, AsyncEFRoundRepository>();
            services.AddScoped<IBanditRepository, AsyncEFBanditRepository>();
            services.AddScoped<IInvitationRepository, AsyncEFInvitationRepository>();
            services.AddScoped<INotificationRepository, AsyncEFNotificationRepository>();
        }

        private void ConfigureUseCases(IServiceCollection services)
        {
            services.AddScoped<BanditsList>();
            services.AddScoped<BanditDetails>();
            services.AddScoped<BanditBandsList>();
            services.AddScoped<BandsList>();
            services.AddScoped<BandDetails>();
            services.AddScoped<CreateBandit>();
            services.AddScoped<CreateBand>();
            services.AddScoped<CreateRound>();
            services.AddScoped<AppNotifications>();
            services.AddScoped<ReadNotification>();
            services.AddScoped<InvitesList>();
            services.AddScoped<HandleInvitation>();
            services.AddScoped<ScoreRound>();
            services.AddScoped<NotifyScoreRound>(s => 
                new NotifyScoreRound(s.GetByName<Notifications.Notifications>("Email"),
                                     s.GetService<IBandRepository>(),
                                     s.GetService<IRoundRepository>()));
            services.AddScoped<JoinApp>(s => 
                new JoinApp(s.GetByName<Notifications.Notifications>("Email"),
                            s.GetService<IBanditRepository>()));
            services.AddScoped<InviteJoinBand>(s => 
                new InviteJoinBand(s.GetService<IInvitationRepository>(),
                                   s.GetService<IBandRepository>(),
                                   s.GetService<IBanditRepository>(), 
                                   s.GetByName<Notifications.Notifications>("InApp")));
            services.AddScoped<RequestJoinBand>(s => 
                new RequestJoinBand(s.GetService<IInvitationRepository>(),
                                    s.GetService<IBandRepository>(),
                                    s.GetService<IBanditRepository>(), 
                                    s.GetByName<Notifications.Notifications>("InApp")));
        }

        private void ConfigureNotifications(IServiceCollection services)
        {
            services.AddScoped<Notifications.AppNotifications>();
            services.AddScoped<Notifications.EmailNotifications>();
            services.AddByName<Notifications.Notifications>()
                .Add<Notifications.AppNotifications>("InApp")
                .Add<Notifications.EmailNotifications>("Email")
                .Build();
        }

        private void ConfigureMail(IServiceCollection services)
        {
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
                
            services.AddSingleton<IEmailConfiguration>(emailConfig);

            services.AddScoped<IEmailService, MailKitService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
