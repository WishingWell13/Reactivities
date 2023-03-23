using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    //Extension classes should be static
    public static class ApplicationServiceExtensions
    {

        //this refers to the object who is calling
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<DataContext>(opt => {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });

            //Services: tells us which external libraries we use
            //Param is saying where handlers are located
            services.AddMediatR(typeof(List.Handler));
            //Assembly locates all Mapping Profiles in project
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}