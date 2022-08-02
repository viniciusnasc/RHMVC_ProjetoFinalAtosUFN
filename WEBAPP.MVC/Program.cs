using KissLog.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using WEBAPP.MVC.Configs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

IdentityConfig.AddIdentityConfig(builder);
DIConfig.ResolveDependencias(builder);
builder.Services.RegisterKissLogListeners();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc(o =>
{
    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido � invalido para este campo!");
    o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => "Este campo deve ser preenchido");
    o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo deve ser preenchido");
    o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "� necessario que o body na requqisi��o n�o esteja vazio");
    o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido � invalido para este campo");
    o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido � invalido para este campo");
    o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser num�rico");
    o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor preenchido � invalido para este campo");
    o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor preenchido � invalido para este campo");
    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => "O campo deve ser num�rico");
    o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => "Este campo deve ser preenchido");
});

// Configura��o de Areas
// Observa��o: Essa configura��o serve para caso o nome da pasta de Areas seja alterado
// Caso o nome seja Areas mesmo, n�o � necess�rio fazer essa configura��o
// Nesse exemplo, utilizei o nome "Modulos"
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

app.RegisterKissLogListeners(builder.Configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Essa configura��o n�o altera o javascript, para alterar o js, verificar _ValidationScriptsPartial na pasta Shared
var defaultCulture = new CultureInfo("pt-Br");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};
app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
//app.MapAreaControllerRoute("AreaRecursosHumanos", "RecursosHumanos", "RecursosHumanos/{controller=Funcao}/{action=Index}/{id?}");

app.Run();