using System;
using UnityEngine;

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
            string concreteType = args.entityType;
            string interfaceType = $"I{concreteType}";
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityMode entityMode = args.entityMode;

            EntityInterfaceGenerator.GenerateFile(interfaceType, ns, imports, directory);
            EntityConcreteGenerator.GenerateFile(concreteType, interfaceType, entityMode, ns, imports, directory);
            EntityBehaviourGenerator.GenerateFile(interfaceType, ns, imports, directory);

            GenerateAspects(args, concreteType, ns, directory, imports);

            switch (args.installerMode)
            {
                case InstallerMode.None:
                    break;
                case InstallerMode.ScriptableEntityInstaller:
                    ScriptableEntityInstallerGenerator.GenerateFile(concreteType, interfaceType, ns,
                        directory, imports);
                    break;
                case InstallerMode.SceneEntityInstaller:
                    SceneEntityInstallerGenerator.GenerateFile(concreteType, interfaceType, ns, directory,
                        imports);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Unity
            if (entityMode is EntityMode.SceneEntity or EntityMode.SceneEntitySingleton)
            {
                if (args.proxyRequired)
                    SceneEntityProxyGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                        directory);

                if (args.worldRequired)
                    SceneEntityWorldGenerator.GenerateFile(concreteType, ns, imports, directory);

                switch (args.poolMode)
                {
                    case PoolMode.None:
                        break;
                    case PoolMode.SceneEntityPool:
                        SceneEntityPoolGenerator.GenerateFile(concreteType, interfaceType, ns, directory,
                            imports);
                        break;
                    case PoolMode.PrefabEntityPool:
                        PrefabEntityPoolGenerator.GenerateFile(concreteType, ns, imports, directory);
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
                        ScriptableEntityFactoryGenerator.GenerateFile(concreteType, interfaceType, ns,
                            directory,
                            imports);
                        break;
                    case FactoryMode.SceneEntityFactory:
                        SceneEntityFactoryGenerator.GenerateFile(concreteType, interfaceType, ns, directory,
                            imports);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (args.viewRequired)
                {
                    EntityViewGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);
                    EntityCollectionViewGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                        directory);
                    EntityViewCatalogGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                        directory);
                    EntityViewPoolGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);
                }

                switch (args.bakerMode)
                {
                    case BakerMode.None:
                        break;
                    case BakerMode.SceneEntityBaker:
                        SceneEntityBakerGenerator.GenerateFile(concreteType, interfaceType, ns, imports,
                            directory);
                        break;
                    case BakerMode.SceneEntityBakerOptimized:
                        SceneEntityBakerOptimizedGenerator.GenerateFile(concreteType, interfaceType, ns,
                            imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void GenerateAspects(
            GenerateArgs args,
            string concreteType,
            string ns,
            string directory,
            string[] imports
        )
        {
            AspectMode mode = args.aspectMode;
            bool scriptableAspectRequired = mode.HasFlag(AspectMode.ScriptableEntityAspect);
            bool sceneAspectRequired = mode.HasFlag(AspectMode.SceneEntityAspect);
            bool bothRequired = scriptableAspectRequired && sceneAspectRequired;

            if (scriptableAspectRequired)
                ScriptableEntityAspectGenerator.GenerateFile(concreteType, ns, directory, imports, bothRequired);

            if (sceneAspectRequired)
                SceneEntityAspectGenerator.GenerateFile(concreteType, ns, directory, imports, bothRequired);
        }
    }
}