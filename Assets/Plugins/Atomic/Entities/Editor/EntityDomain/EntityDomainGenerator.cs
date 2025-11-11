using System;

namespace Atomic.Entities
{
    internal static class EntityDomainGenerator
    {
        public struct GenerateArgs
        {
            public string entityType;
            public BaseMode BaseMode;
            public string ns;
            public string directory;
            public string[] imports;

            public bool proxyRequired;
            public bool worldRequired;
            public bool viewRequired;
            public InstallerMode installerMode;
            public AspectMode aspectMode;
            public PoolMode poolMode;
            public FactoryType factoryType;
            public BakerType bakerType;
        }

        public static void Generate(GenerateArgs args)
        {
            string entityImplementation = args.entityType;
            string entityInterface = $"I{entityImplementation}";
            BaseMode baseMode = args.BaseMode;
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityInterfaceGenerator.GenerateFile(entityInterface, ns, imports, directory);
            EntityConcreteGenerator.GenerateFile(entityImplementation, ns, imports, directory, baseMode, entityInterface);
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
            if (baseMode is BaseMode.SceneEntity or BaseMode.SceneEntitySingleton)
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
            else if (baseMode is BaseMode.Entity or BaseMode.EntitySingleton)
            {
                switch (args.factoryType)
                {
                    case FactoryType.None:
                        break;
                    case FactoryType.ScriptableEntityFactory:
                        ScriptableEntityFactoryGenerator.GenerateFile(entityImplementation, entityInterface, ns, directory,
                            imports);
                        break;
                    case FactoryType.SceneEntityFactory:
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

                switch (args.bakerType)
                {
                    case BakerType.None:
                        break;
                    case BakerType.SceneEntityBaker:
                        SceneEntityBakerGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                        break;
                    case BakerType.SceneEntityBakerOptimized:
                        SceneEntityBakerOptimizedGenerator.GenerateFile(entityImplementation, entityInterface, ns, imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}