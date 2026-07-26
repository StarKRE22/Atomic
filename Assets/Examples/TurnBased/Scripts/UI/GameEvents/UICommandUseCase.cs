using Atomic.Entities;

namespace Game.UI
{
    public static class UICommandUseCase
    {
        public static void EnqueueCommand(this IUIContext ui, IUICommand command)
        {
            UICommandQueue commandQueue = ui.GetValue(UIContextAPI.CommandQueue);
            commandQueue.Enqueue(command);
        }
    }
}