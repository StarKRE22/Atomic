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
            public bool viewRequired;
            public InstallerMode installerMode;
            public AspectMode aspectMode;
            public FactoryMode factoryMode;
            public PoolMode poolMode;
            public BakerMode bakerMode;
        }

        public static void Generate(GenerateArgs args)
        {
            string entityImplementation = args.entityType;
            string entityInterface = $"I{entityImplementation}";
            EntityMode entityMode = args.entityMode;
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityInterfaceGenerator.GenerateFile(entityInterface, ns, imports, directory);
            EntityConcreteGenerator.GenerateFile(entityImplementation, ns, imports, directory, entityMode, entityInterface);
            EntityBehaviourGenerator.GenerateFile(entityInterface, ns, imports, directory);

            switch (args.aspectMode)
            {
                case AspectMode.None:
                    break;
                case AspectMode.ScriptableEntityAspect:
                    ScriptableEntityAspectGenerator.GenerateFile(entityImplementation, ns, directory, imports);
                    break;
                case AspectMode.SceneEntityAspect:
                    SceneEntityAspectGenerator.GenerateFile(entityImplementation, ns, directory, imports);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            switch (args.installerMode)
            {
                case InstallerMode.None:
                    break;
                case InstallerMode.ScriptableEntityInstaller:
                    ScriptableEntityInstallerGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory, imports);
                    break;
                case InstallerMode.SceneEntityInstaller:
                    SceneEntityInstallerGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory, imports);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // Unity
            if (entityMode is EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)
            {
                if (args.proxyRequired)
                    SceneEntityProxyGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);

                if (args.worldRequired) 
                    SceneEntityWorldGenerator.GenerateFile(entityImplementation, ns, imports, directory);
                
                switch (args.poolMode)
                {
                    case PoolMode.None:
                        break;
                    case PoolMode.SceneEntityPool:
                        SceneEntityPoolGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory, imports);
                        break;
                    case PoolMode.PrefabEntityPool:
                        PrefabEntityPoolGenerator.GenerateFile(entityImplementation, ns, imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            // CSharp mode
            else if (entityMode is EntityMode.Entity or EntityMode.EntitySingleton)
            {
                switch (args.factoryMode)
                {
                    case FactoryMode.None:
                        break;
                    case FactoryMode.ScriptableEntityFactory:
                        ScriptableEntityFactoryGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory,
                            imports);
                        break;
                    case FactoryMode.SceneEntityFactory:
                        SceneEntityFactoryGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory, imports);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (args.viewRequired)
                {
                    EntityViewGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                    EntityCollectionViewGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                    EntityViewCatalogGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                    EntityViewPoolGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                }

                switch (args.bakerMode)
                {
                    case BakerMode.None:
                        break;
                    case BakerMode.SceneEntityBaker:
                        SceneEntityBakerGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                        break;
                    case BakerMode.SceneEntityBakerOptimized:
                        SceneEntityBakerOptimizedGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}