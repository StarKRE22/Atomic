using System;

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

            // CSharp mode
            else if (entityMode is EntityMode.Entity or EntityMode.EntitySingleton)
            {
                switch (args.factoryMode)
                {
                    case EntityFactoryMode.None:
                        break;
                    case EntityFactoryMode.ScriptableEntityFactory:
                        ScriptableEntityFactoryGenerator.GenerateFile(concreteType, interfaceType, ns,
                            directory,
                            imports);
                        break;
                    case EntityFactoryMode.SceneEntityFactory:
                        SceneEntityFactoryGenerator.GenerateFile(concreteType, interfaceType, ns, directory,
                            imports);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                // if (args.viewRequired)
                // {
                //     EntityViewGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);
                //     EntityCollectionViewGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                //         directory);
                //     EntityViewCatalogGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                //         directory);
                //     EntityViewPoolGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);
                // }

                switch (args.bakerMode)
                {
                    case EntityBakerMode.None:
                        break;
                    case EntityBakerMode.SceneEntityBaker:
                        SceneEntityBakerGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                            directory);
                        break;
                    case EntityBakerMode.SceneEntityBakerOptimized:
                        SceneEntityBakerOptimizedGenerator.GenerateFile(concreteType, interfaceType, ns,
                            imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}