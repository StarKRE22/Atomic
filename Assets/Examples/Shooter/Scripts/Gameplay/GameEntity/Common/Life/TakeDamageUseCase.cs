// using System;
// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public static class TakeDamageUseCase
//     {
//         public static bool TakeDamage(in Collider collider, in DamageArgs args, in IGameContext context = null)
//         {
//             return collider.TryGetComponent(out IEntity target) && TakeDamage(in target, in args, in context);
//         }
//         
//         public static bool TakeDamage(in IEntity target, in DamageArgs args, in IGameContext context = null)
//         {
//             if (!target.HasDamageableTag())
//                 return false;
//
//             Health health = target.GetHealth();
//             if (!health.Reduce(args.damage))
//                 return false;
//
//             target.GetTakeDamageEvent().Invoke(args);
//             
//             if (!health.IsEmpty())
//                 return true;
//
//             target.GetTakeDeathEvent().Invoke(args);
//
//             IEntity source = args.source;
//             context?.GetKillEvent().Invoke(new KillArgs
//             {
//                 instigator = source,
//                 victim = target
//             });
//
//             return true;
//         }
//     }
// }