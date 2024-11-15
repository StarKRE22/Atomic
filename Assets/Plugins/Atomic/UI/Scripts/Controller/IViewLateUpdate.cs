namespace Atomic.UI
{
    public interface IViewLateUpdate : IViewController
    {
        void OnLateUpdate(float deltaTime);
    }
}