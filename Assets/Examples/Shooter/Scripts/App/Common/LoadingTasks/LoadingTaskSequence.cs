using Cysharp.Threading.Tasks;

namespace ShooterGame.App
{
    public sealed class LoadingTaskSequence : ILoadingTask
    {
        private readonly ILoadingTask[] _tasks;

        public LoadingTaskSequence(params ILoadingTask[] tasks) => 
            _tasks = tasks;

        public async UniTask Invoke(IAppContext context, LoadingBundle bundle)
        {
            for (int i = 0, count = _tasks.Length; i < count; i++)
                await _tasks[i].Invoke(context, bundle);
        }
    }
}