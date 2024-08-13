namespace GestaoProjeto.Presentation.Extensions
{
    /// <summary>
    /// Classe de extensão para configuração da pólítica de CORS
    /// </summary>
    public static class CorsConfigExtension
    {
        private static string _policyName = "DefaultPolicy";

        public static IServiceCollection AddCorsConfig(this IServiceCollection services)
        {
            services.AddCors(s => s.AddPolicy(_policyName, builder =>
            {
                //qualquer domínio pode acessar a API
                builder.AllowAnyOrigin()
                //qualquer método (POST, PUT, DELETE, GET etc) pode ser acessado
                .AllowAnyMethod()
                //qualquer parâmetro de cabeçalho de requisição pode ser enviado
                .AllowAnyHeader();
            }));

            return services;
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            app.UseCors(_policyName);
            return app;
        }
    }
}

