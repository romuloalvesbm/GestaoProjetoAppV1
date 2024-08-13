using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using GestaoProjeto.Infra.Database.Extensions;
using GestaoProjeto.Application.Extensions;
using GestaoProjeto.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc(builder.Environment, provider);
builder.Services.AddDataContext(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddJsonDoc();
builder.Services.AddFluentValidation();
builder.Services.AddCorsConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseSwaggerDoc(provider);
app.UseCorsConfig();
app.MapControllers();

app.Run();
