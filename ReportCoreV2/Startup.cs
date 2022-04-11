using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReportCoreV2.ATCEfModel;
using ReportCoreV2.BusinessDataHandler;
using ReportCoreV2.DataRepository;
using ReportCoreV2.ExternalEfModel;
using ReportCoreV2.Models;
using ReportCoreV2.Models.ModelInterfaces;
using ReportCoreV2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportCoreV2
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
            services.AddScoped<IDashboardViewModel, DashboardViewModel>();
            services.AddScoped<IDashboardModel, DashboardModel>();
            services.AddScoped<IDashboardData, DashboardData>();
            services.AddScoped<IDashboardDataHandler, DashboardDataHandler>();
            services.AddScoped<IExecutionLogModel, ExecutionLogModel>();
            services.AddScoped<IExecutionData, ExecutionData>();
            services.AddScoped<IExecutionLogViewModel, ExecutionLogViewModel>();
            services.AddScoped<IExecutionDataHandler, ExecutionDataHandler>();
            services.AddScoped<IApprovedScenarioModel, ApprovedScenarioModel>();
            services.AddScoped<IApprovedScenarioData, ApprovedScenarioData>();
            services.AddScoped<IApprovedScenarioViewModel, ApprovedScenarioViewModel>();
            services.AddScoped<IApprovedScenarioDataHandler, ApprovedScenarioDataHandler>();
            services.AddScoped<IExternalExecutionDataModel, ExternalExecutionDataModel>();
            services.AddScoped<IExternalExecutionData, ExternalExecutionData>();
            services.AddScoped<IExternalDataHandler, ExternalDataHandler>();
            services.AddScoped<IExternalApprovedScenarioModel, ExternalApprovedScenarioModel>(); 
            services.AddScoped<IExternalApprovedScenarioData, ExternalApprovedScenarioData>();
            services.AddScoped<IExternalProjectModel, ExternalProjectModel>();
            services.AddScoped<IExternalDataAddViewModel, ExternalDataAddViewModel>();
            services.AddScoped<IExternalProjectDataHandler, ExternalProjectDataHandler>();
            services.AddScoped<IExternalAddExecution, ExternalAddExecution>();
            services.AddScoped<IExternalAddCreatedScenarios, ExternalAddCreatedScenarios>(); 
            services.AddScoped<IExtenalData, ExtenalData>();
            services.AddScoped<IDurationInfoDataHandler, DurationInfoDataHandler>();
            services.AddScoped<IDurationInfoModel, DurationInfoModel>();
            services.AddScoped<IDurationInfoData, DurationInfoData>();
            services.AddScoped<IDurationListViewModel, DurationListViewModel>();
            services.AddScoped<IProjectDataModel, ProjectDataModel>();
            services.AddScoped<IProjectDashboardModel, ProjectDashboardModel>();
            services.AddScoped<IProjectDashboardViewModel, ProjectDashboardViewModel>();
            services.AddScoped<IProjectDashboardData, ProjectDashboardData>();
            services.AddScoped<IProjectDashboardDataHandler, ProjectDashboardDataHandler>(); 
            services.AddScoped<IExternalDashboardViewModel, ExternalDashboardViewModel>();

            services.AddDbContext<ATCContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("ATCDBConnection")));

            services.AddDbContext<ExternalReportsContext>(options =>
               options.UseSqlServer(
               Configuration.GetConnectionString("ExternalDBConnection")));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
