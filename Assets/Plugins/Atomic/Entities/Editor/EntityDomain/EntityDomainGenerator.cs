namespace Atomic.Entities
{
    internal static class EntityDomainGenerator
    {
        public struct GenerateArgs
        {
            public string entityType;
            public EntityMode entityMode;
            public string ns;
            public string directory;
            public string[] imports;

            public bool proxyRequired;
            public bool worldRequired;
            public EntityInstallerMode installerMode;
            public EntityAspectMode aspectMode;
            public EntityFactoryMode factoryMode;
            public EntityPoolMode poolMode;
            public EntityBakerMode bakerMode;
            public EntityViewMode viewMode;
        }

        public static void Generate(GenerateArgs args)
        {
            string concreteType = args.entityType;
            string interfaceType = $"I{concreteType}";
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityMode entityMode = args.entityMode;

            EntityInterfaceGenerator.Generate(interfaceType, ns, imports, directory);
            EntityConcreteGenerator.Generate(entityMode, concreteType, interfaceType, ns, imports, directory);
            EntityBehaviourGenerator.Generate(interfaceType, ns, imports, directory);
            EntityAspectGenerator.Generate(args.aspectMode, concreteType, interfaceType, ns, directory, imports);
            EntityInstallerGenerator.Generate(args.installerMode, concreteType, interfaceType, ns, directory, imports);

            // Unity
            if (entityMode is EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)
            {
                if (args.proxyRequired)
                    SceneEntityProxyGenerator.Generate(concreteType, interfaceType, ns, imports, directory);

                if (args.worldRequired)
                    SceneEntityWorldGenerator.Generate(concreteType, ns, imports, directory);

                EntityPoolGenerator.Generate(args.poolMode, concreteType, interfaceType, ns, directory, imports);
            }

            // CSharp
            else if (entityMode is EntityMode.Entity or EntityMode.EntitySingleton)
            {
                EntityFactoryGenerator.Generate(args.factoryMode, concreteType, interfaceType, ns, directory, imports);
                EntityBakerGenerator.Generate(args.bakerMode, concreteType, interfaceType, ns, imports, directory);
                EntityUIGenerator.Generate(args.viewMode, concreteType, interfaceType, ns, imports, directory);
            }
        }
    }
}