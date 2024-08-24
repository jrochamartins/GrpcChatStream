namespace GrpcChat.Client
{
    internal class ForecolorPicker : IDisposable
    {
        private readonly ConsoleColor _color = Console.ForegroundColor;

        public ForecolorPicker(ConsoleColor _newColor) => Console.ForegroundColor = _newColor;

        public void Dispose() => Console.ForegroundColor = _color;
    }
}
