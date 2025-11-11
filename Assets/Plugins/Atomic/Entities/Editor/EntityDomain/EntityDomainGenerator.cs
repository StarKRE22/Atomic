using System;

namespace Atomic.Entities
{
    internal static class EntityDomainGenerator
    {
        public struct GenerateArgs
        {
            public string entityType;
            public EntityBaseType baseType;
            public string ns;
            public string directory;
            public string[] imports;

            public bool proxyRequired;
            public bool worldRequired;
            public EntityInstallerType installerType;
            public EntityAspectType aspectType;
            public EntityPoolType poolType;
            public EntityFactoryType factoryType;
        }

        public static void Generate(GenerateArgs args)
        {
            string entityImpl = args.entityType;
            string entityInterface = $"I{entityImpl}";
            EntityBaseType baseType = args.baseType;
            string ns = args.ns;
            string[] imports = args.imports;
            string directory = args.directory;

            EntityInterfaceGenerator.GenerateFile(entityInterface, ns, imports, directory);
            EntityConcreteGenerator.GenerateFile(entityImpl, ns, imports, directory, baseType, entityInterface);
            EntityBehaviourGenerator.GenerateFile(entityInterface, ns, imports, directory);

            switch (args.aspectType)
            {
                case EntityAspectType.None:
                    break;
                case EntityAspectType.ScriptableEntityAspect:
                    ScriptableEntityAspectGenerator.GenerateFile(entityImpl, ns, directory, imports);
                    break;
                case EntityAspectType.SceneEntityAspect:
                    SceneEntityAspectGenerator.GenerateFile(entityImpl, ns, directory, imports);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            switch (args.installerType)
            {
                case EntityInstallerType.None:
                    break;
                case EntityInstallerType.ScriptableEntityInstaller:
                    ScriptableEntityInstallerGenerator.GenerateFile(entityImpl, entityInterface, ns, directory, imports);
                    break;
                case EntityInstallerType.SceneEntityInstaller:
                    SceneEntityInstallerGenerator.GenerateFile(entityImpl, entityInterface, ns, directory, imports);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // Unity
            if (baseType is EntityBaseType.SceneEntity or EntityBaseType.SceneEntitySingleton)
            {
                if (args.proxyRequired)
                    SceneEntityProxyGenerator.GenerateFile(entityImpl, entityInterface, ns, imports, directory);

                if (args.worldRequired) 
                    SceneEntityWorldGenerator.GenerateFile(entityImpl, ns, imports, directory);
                
                switch (args.poolType)
                {
                    case EntityPoolType.None:
                        break;
                    case EntityPoolType.SceneEntityPool:
                        SceneEntityPoolGenerator.GenerateFile(entityImpl, entityInterface, ns, directory, imports);
                        break;
                    case EntityPoolType.PrefabEntityPool:
                        PrefabEntityPoolGenerator.GenerateFile(entityImpl, ns, imports, directory);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            // CSharp mode
            else if (baseType is EntityBaseType.Entity or EntityBaseType.EntitySingleton)
            {
                switch (args.factoryType)
                {
                    case EntityFactoryType.None:
                        break;
                    case EntityFactoryType.ScriptableEntityFactory:
                        ScriptableEntityFactoryGenerator.GenerateFile(entityImpl, entityInterface, ns, directory,
                            imports);
                        break;
                    case EntityFactoryType.SceneEntityFactory:
                        SceneEntityFactoryGenerator.GenerateFile(entityImpl, entityInterface, ns, directory, imports);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}