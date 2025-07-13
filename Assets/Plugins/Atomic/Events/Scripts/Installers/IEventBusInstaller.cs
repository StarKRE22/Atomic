namespace Atomic.Events
{
    public interface IEventBusInstaller
    {
        void Install(IEventBus bus);
    }
}