// using System;
// // ReSharper disable FieldCanBeMadeReadOnly.Global
//
// namespace Atomic.Entities
// {
//     public class EntityFactorySpy : IEntityFactory<IEntity>
//     {
//         public IEntity Created { get; private set; }
//
//         public Func<IEntity> CreateMethod = () => new Entity();
//         
//         public IEntity Create()
//         {
//             this.Created = this.CreateMethod?.Invoke();
//             return this.Created;
//         }
//     }
// }