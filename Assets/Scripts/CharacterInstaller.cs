// using Atomic.Elements;
// using Atomic.Entities;
//
// namespace SampleGame
// {
//     public sealed class CharacterInstaller : SceneEntityInstaller
//     {
//         public override void Install(IEntity entity)
//         {
//             entity.AddHealth(new Const<int>(5));
//             entity.AddSpeed(3.0f);
//             entity.AddTransform(this.transform);
//
//             entity.AddValue("GameObject", this.gameObject);
//             entity.AddTag("Character");
//         }
//     }
// }