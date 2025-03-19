using BlazingBlog.Application;
using BlazingBlog.Infrastructure;
using BlazingBlog.WebUI.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents() // static server side stuff...
    .AddInteractiveServerComponents(); // server side stuf...

// All depenencies in the ApplicationLayer are loaded here:
builder.Services.AddApplication();
builder.Services.AddInfrastucture(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Configure to allow server render mode... 

app.Run();
