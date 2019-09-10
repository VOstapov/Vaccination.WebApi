using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vaccination.DataAccess.EF;
using Vaccination.DataAccess.Repository;
using Vaccination.Domain.Contracts;
using Vaccination.Domain.Mapping;
using Vaccination.Domain.Models.Domain;
using Vaccination.Domain.Models.DTO;
using Vaccination.Domain.Services;

namespace Vaccination.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<VaccinationContext>(options =>
                options.UseSqlServer(Configuration["Data:ContactContext:ConnectionString"]));
            services.AddScoped<DbContext, VaccinationContext>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddExpressionMapping();
                //cfg.AddProfile<PatientMapping>();
            },
            new Type[]
            {
                typeof(PatientMapping),
                typeof(VaccineMapping)
            },
            ServiceLifetime.Singleton);


            services.AddScoped<IService<PatientDto>, GenericService<Patient, PatientDto>>();
            services.AddScoped<IService<VaccineDto>, GenericService<Vaccine, VaccineDto>>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
