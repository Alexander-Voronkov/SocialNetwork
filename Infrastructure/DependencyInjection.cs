using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connstr)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(connstr);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}