using GestaoProjeto.Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            // services.AddValidatorsFromAssemblyContaining<FuncionarioCadastroValidator>();

            services.AddValidatorsFromAssemblyContaining<FuncionarioCadastroValidator>();
            services.AddValidatorsFromAssemblyContaining<FuncionarioEdicaoValidator>();
            services.AddValidatorsFromAssemblyContaining<FuncionarioExclusaoValidator>();

            //services.AddControllers().AddFluentValidation(fv =>
            //{
            //    // Opções adicionais, se necessário
            //});

            return services;

        }
    }
}