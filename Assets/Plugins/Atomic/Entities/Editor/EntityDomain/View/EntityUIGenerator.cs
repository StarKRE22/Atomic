namespace Atomic.Entities
{
    public static class EntityUIGenerator
    {
        public static void Generate(
            EntityViewMode viewMode,
            string concreteType,
            string interfaceType,
            string ns,
            string[] imports,
            string directory
        )
        {
            if (viewMode.HasFlag(EntityViewMode.EntityView))
                EntityViewGenerator.Generate(concreteType, interfaceType, ns, imports, directory);

            if (viewMode.HasFlag(EntityViewMode.EntityViewCatalog))
                EntityViewCatalogGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);

            if (viewMode.HasFlag(EntityViewMode.EntityViewPool))
                EntityViewPoolGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);

            if (viewMode.HasFlag(EntityViewMode.EntityCollectionView))
                EntityCollectionViewGenerator.GenerateFile(concreteType, interfaceType, ns, imports, directory);
        }
    }
}