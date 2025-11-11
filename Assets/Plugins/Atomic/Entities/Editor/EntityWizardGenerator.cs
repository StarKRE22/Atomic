namespace Atomic.Entities
{
    public static class EntityWizardGenerator
    {
        public struct GenerateArgs
        {
            public string entityType;
            public EntityBaseType entityBaseType;
            public string ns;
            public string directory;
            public string[] imports;
        }
        
        public static void Generate(GenerateArgs args)
        {
            string entityImpl = args.entityType;
            string entityInterface = $"I{entityImpl}";
            EntityBaseType baseType = args.entityBaseType;
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityInterfaceGenerator.GenerateFile(entityInterface, ns, imports, directory);
            EntityClassGenerator.GenerateFile(entityImpl, ns, imports, directory, baseType, entityInterface);
            EntityBehavioursGenerator.GenerateFile(entityInterface, ns, imports, directory);
        }
    }
}