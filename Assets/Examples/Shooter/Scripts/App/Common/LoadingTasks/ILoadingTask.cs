using Atomic.Elements;
using Cysharp.Threading.Tasks;

namespace ShooterGame.App
{
    public interface ILoadingTask : IFunction<IAppContext, LoadingBundle, UniTask>
    {
    }
}