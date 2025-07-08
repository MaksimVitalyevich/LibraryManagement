using LibraryManagement.LibraryModule;
using LibraryManagement.LibraryModule.BookModels;
using LibraryManagement.LibraryModule.BookServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddViewLocalization().AddDataAnnotationsLocalization();

builder.Services.AddSingleton<IUnifiedBookService, BookMediator>();

builder.Services.AddSingleton<IBookService<HistoricalBook>, HistoricalBookService>();
builder.Services.AddSingleton<IBookService<GuideBook>, GuideBookService>();
builder.Services.AddSingleton<IBookService<ModernBook>, ModernBookService>();

builder.Services.AddSingleton<IBookService<IBook>>(provider =>
    new BookServiceAdapter<HistoricalBook>(provider.GetRequiredService<IBookService<HistoricalBook>>()));
builder.Services.AddSingleton<IBookService<IBook>>(provider =>
    new BookServiceAdapter<GuideBook>(provider.GetRequiredService<IBookService<GuideBook>>()));
builder.Services.AddSingleton<IBookService<IBook>>(provider =>
    new BookServiceAdapter<ModernBook>(provider.GetRequiredService<IBookService<ModernBook>>()));

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    app.UseExceptionHandler("/Error/Exception");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages().WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}").WithStaticAssets();

app.MapControllerRoute(
    name: "library-list",
    pattern: "library-list/{action=Index}",
    defaults: new { controller = "LibraryList" }).WithStaticAssets();

app.MapControllerRoute(
    name: "Historical",
    pattern: "Historical/{action=FormEdit}/{year?}/{title?}",
    defaults: new { controller = "HistoricalBook"}).WithStaticAssets();

app.MapControllerRoute(
    name: "Guide",
    pattern: "Guide/{action=FormEdit}/{year?}/{title?}",
    defaults: new { controller = "GuideBook"}).WithStaticAssets();

app.MapControllerRoute(
    name: "Modern",
    pattern: "Modern/{action=FormEdit}/{year?}/{title?}",
    defaults: new { controller = "ModernBook" }).WithStaticAssets();

app.Run();
