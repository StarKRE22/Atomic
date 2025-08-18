// using Atomic.Entities;
// using Modules.Gameplay;
//
// namespace RTSGame
// {
//     public static class TakeDamageUseCase
//     {
//         public static bool TakeDamage(in IEntity target, in int damage)
//         {
//             if (!target.HasDamageableTag())
//                 return false;
//
//             Health health = target.GetHealth();
//             if (health.IsEmpty())
//                 return false;
//
//             if (!health.Reduce(damage))
//                 return false;
//
//             return true;
//         }
//     }
// }