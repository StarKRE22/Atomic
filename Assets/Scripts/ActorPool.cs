// using Atomic.Entities;
//
// /**
//  * Created by Entity Domain Generator.
//  */
//
// namespace SampleGame.Gameplay
// {
//     /// <summary>
//     /// A Unity-integrated pool for <see cref="Actor"/> entities that exist within a scene.
//     /// </summary>
//     /// <remarks>
//     /// Implements <see cref="IEntityPool{IActor}"/> for renting and returning scene-based entities.
//     /// </remarks>
//     public sealed class ActorPool : SceneEntityPool<Actor>, IEntityPool<IActor>
//     {
//         IActor IEntityPool<IActor>.Rent() => this.Rent();
//
//         void IEntityPool<IActor>.Return(IActor entity) => this.Return((Actor)entity);
//     }
// }
