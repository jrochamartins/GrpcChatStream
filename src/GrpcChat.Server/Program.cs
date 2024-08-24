using GrpcChat.Server.Domain;
using GrpcChat.Server.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddSingleton<ChatRoom>();

var app = builder.Build();

app.MapGrpcService<ChatService>();

app.Run();
