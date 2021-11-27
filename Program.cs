using System.IdentityModel.Tokens.Jwt;

// Disable claim mapping to get claims 1:1 from the tokens
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// Read config
var apiPath = builder.Configuration.GetValue<string>("Gateway:ApiPath", "/api/").ToLower();
var url = builder.Configuration.GetValue<string>("Gateway:Url", "");

// Configure Services
builder.Services.AddDistributedMemoryCache();
builder.AddGateway();

// Build App and add Middleware
var app = builder.Build();
app.UseGateway(apiPath);

// Start Gateway
if (string.IsNullOrEmpty(url)) {
    app.Run();
}
else {
    app.Run(url);
}

