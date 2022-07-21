WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options=>
options.MimeTypes = ResponseCompressionDefaults.MimeTypes
                                               .Concat(new[] {"application/octet-stream"}));
builder.Services.AddControllers();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseResponseCompression();
if (app.Environment.IsDevelopment())
    app.UseWebAssemblyDebugging();

else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
