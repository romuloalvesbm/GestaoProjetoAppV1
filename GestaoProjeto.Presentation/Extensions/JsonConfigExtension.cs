using System.Text.Json.Serialization;

namespace GestaoProjeto.Presentation.Extensions
{
    public static class JsonConfigExtension
    {
        public static IServiceCollection AddJsonDoc(this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            return services;

        }
    }
}
