using GrpcChat.Server.Domain;
using GrpcChat.Server.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddSingleton<ChatRoom>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ChatService>();

app.Run();
