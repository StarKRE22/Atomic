using System.Collections.Generic;

#if UNITY_EDITOR
namespace Atomic.Events
{
    public readonly struct EventDefinition
    {
        public readonly string name;
        public readonly List<Arg> args;
        public int id => EventBusUtils.NameToId(name);

        public EventDefinition(string name, List<Arg> args)
        {
            this.name = name;
            this.args = args;
        }

        public struct Arg
        {
            public readonly string type;
            public readonly string name;

            public Arg(string type, string name)
            {
                this.type = type;
                this.name = name;
            }
        }
    }
}
#endif