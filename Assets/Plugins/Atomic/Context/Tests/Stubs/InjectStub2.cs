namespace Atomic.Contexts
{
    public sealed class InjectStub2
    {
        public string name;

        [Construct]
        public void Construct([Inject(1)] string name)
        {
            this.name = name;
        }
    }
}