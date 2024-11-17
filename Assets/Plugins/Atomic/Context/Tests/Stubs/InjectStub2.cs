namespace Atomic.Contexts
{
    public sealed class InjectStub2
    {
        public string name;

        [ContextInject]
        public void Construct([ContextInject(1)] string name)
        {
            this.name = name;
        }
    }
}