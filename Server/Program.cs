using Server.Hubs; // Assuming ChatHub is in Server.Hubs namespace

var builder = WebApplication.CreateBuilder(args);

// Register SignalR
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7114")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure middleware pipeline
app.UseDefaultFiles(); // This serves index.html by default
app.UseStaticFiles();  // This enables serving static files (HTML, JS, etc.)

// Map SignalR endpoint
app.MapHub<ChatHub>("/chathub");



app.UseCors();

app.Run();
app.Run();