using Application.Interface;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterIoCServices(this IServiceCollection services)
        {
            // Application
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IReaderService, ReaderService>();

            // Domain.Interfaces > Infrastructure.Data.Repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IReaderRepository, ReaderRepository>();
        }
    }
}
