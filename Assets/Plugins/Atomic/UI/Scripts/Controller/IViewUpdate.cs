namespace Atomic.UI
{
    public interface IViewUpdate : IViewController
    {
        void OnUpdate(float deltaTime);
    }
}