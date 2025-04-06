using BlazingBlog.Application;
using BlazingBlog.Infrastructure;
using BlazingBlog.WebUI.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents() // static server side stuff...
    .AddInteractiveServerComponents() // server side stuf...
    .AddInteractiveWebAssemblyComponents(); // WebAssembly added for article management


// All depenencies in the ApplicationLayer are loaded here:
builder.Services.AddApplication();
builder.Services.AddInfrastucture(builder.Configuration);

// Added after we introduced the WebUI.Client so we have endpoints. Ordinarilly not needed at all... 
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Added after enabling Controllers above... 
app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()  // Configure to allow server render mode... 
    .AddInteractiveWebAssemblyRenderMode()   // WebAssembly added later
                                             // Any specific assemblies in the WebAssembly Client UI project need to be added:
                                             // .AddAdditionalAssemblies(typeof(BlazingBlog.WebUI.Client.Features.Articles.Components.ArticleOverview).Assembly); // see comment in lectures/57072550

    .AddAdditionalAssemblies(typeof(BlazingBlog.WebUI.Client._Imports).Assembly); // Only using _Imports is easier. See comment in lectures/57072550

app.Run();
