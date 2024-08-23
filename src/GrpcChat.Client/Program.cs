﻿using Grpc.Net.Client;
using GrpcChat.Shared;
using ProtoBuf.Grpc.Client;

namespace GrpcChat.Client
{
    public class Program
    {
        public static async Task Main()
        {
            var user = GetUser();

            using var channel = GrpcChannel.ForAddress("https://localhost:7212");
            var client = channel.CreateGrpcService<IChatService>();

            var listenTask = Task.Run(async () =>
            {
                await foreach (var message in client.UserJoin(user))
                    if (user.Username != message?.Username)
                        WriteReceivedMessage(message);
            });

            await client.UserInteract(GetClientMessages(user));
            await client.UserDisconect(user);
            await listenTask;

            WriteReceivedMessage(new ChatMessage { Username = "Server", Content = "Você saiu da conversa, tecle 'enter' para fechar..." });
            Console.Read();
        }

        private static void WriteReceivedMessage(ChatMessage? message)
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = message?.Username == "Server" ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.WriteLine(message?.Render());
            Console.ForegroundColor = defaultColor;
        }

        private static User GetUser()
        {
            string? userName;
            do
            {
                Console.Clear();
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("Bem vindo a sala de chat particular!");
                Console.Write("Digite seu nome de usuário: ");
                userName = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(userName));

            Console.Clear();
            Console.WriteLine(new string('*', 30));
            Console.Title = $"Usuário: {userName}";
            Console.WriteLine("Bem vindo(a) {0}! ", userName);
            Console.WriteLine("Começe a escrever e tecle 'Enter' para enviar.");
            Console.WriteLine("Envie 'sair' para fechar a qualquer momento...");
            Console.WriteLine(new string('*', 30));

            return new User()
            {
                Username = userName
            };
        }

        private static async IAsyncEnumerable<ChatMessage> GetClientMessages(User user)
        {
            do
            {
                await Task.Delay(25);

                var request = new ChatMessage
                {
                    Username = user.Username,
                    Content = Console.ReadLine()
                };

                PreviewSentMessage(request);

                if (request.Content?.Trim().ToLower() == "sair")
                    yield break;

                yield return request;
            } while (true);
        }

        private static void PreviewSentMessage(ChatMessage? message)
        {
            if (Console.CursorTop == 0) return;
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(message?.Render());
        }
    }
}