namespace Atomic.UI
{
    public interface IViewFixedUpdate : IViewController
    {
        void OnFixedUpdate(float deltaTime);
    }
}