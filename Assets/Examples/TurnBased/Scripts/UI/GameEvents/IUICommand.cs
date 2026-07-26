using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public interface IUICommand
    {
        UniTask Execute();
    }
}