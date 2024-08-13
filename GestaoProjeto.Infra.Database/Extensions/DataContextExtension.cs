using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Database.Repositories;
using GestaoProjeto.Infra.Domain.Departamentos;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Infra.Domain.Interfaces.Repositories;
using GestaoProjeto.Infra.Domain.Participacoes;
using GestaoProjeto.Infra.Domain.Projetos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Extensions
{
    public static class DataContextExtension
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            //injeção de dependência do DataContext
            services.AddDbContext<DataContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DB_GESTAOPROJETOV1")));

            //injeção de dependência do UnitOfWork
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDepartamentoRepository, DepartamentoRepository>();
            services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();
            services.AddTransient<IParticipacaoRepository, ParticipacaoRepository>();
            services.AddTransient<IProjetoRepository, ProjetoRepository>();

            return services;
        }
    }
}
