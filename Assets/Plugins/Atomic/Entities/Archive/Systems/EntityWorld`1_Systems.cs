// using System;
// using System.Collections.Generic;
// using System.Runtime.CompilerServices;
// using static Atomic.Entities.InternalUtils;
//
// namespace Atomic.Entities
// {
//     /**
//      * Experimental
//      */
//     public partial class EntityWorld<E>
//     {
//         /// <summary>
//         /// Equality comparer for ITickEntitySystem behaviours.
//         /// </summary>
//         private static readonly IEqualityComparer<ITickEntitySystem<E>> s_tickSystemComparer =
//             EqualityComparer<ITickEntitySystem<E>>.Default;
//
//         /// <summary>
//         /// Equality comparer for IFixedTickEntitySystem behaviours.
//         /// </summary>
//         private static readonly IEqualityComparer<IFixedTickEntitySystem<E>> s_fixedTickSystemComparer =
//             EqualityComparer<IFixedTickEntitySystem<E>>.Default;
//
//         /// <summary>
//         /// Equality comparer for ILateTickEntitySystem behaviours.
//         /// </summary>
//         private static readonly IEqualityComparer<ILateTickEntitySystem<E>> s_lateTickSystemComparer =
//             EqualityComparer<ILateTickEntitySystem<E>>.Default;
//         
//         public event Action<IEntityWorld<E>, IEntitySystem<E>> OnSystemAdded;
//         
//         public event Action<IEntityWorld<E>, IEntitySystem<E>> OnSystemDeleted;
//
//         public int SystemCount => _systemCount;
//
//         private IEntitySystem<E>[] _systems = Array.Empty<IEntitySystem<E>>();
//         
//         private ITickEntitySystem<E>[] _updates;
//         private IFixedTickEntitySystem<E>[] _fixedUpdates;
//         private ILateTickEntitySystem<E>[] _lateUpdates;
//
//         private int _updateCount;
//         private int _fixedUpdateCount;
//         private int _lateUpdateCount;
//
//         
//         private int _systemCount;
//
//         public void AddSystem(IEntitySystem<E> system)
//         {
//             if (system == null)
//                 throw new ArgumentNullException(nameof(system));
//             
//             //Check for capacity:
//             int capacity = _systems.Length;
//             if (_systemCount == capacity)
//                 Array.Resize(ref _systems, capacity == 0 ? 1 : capacity * 2);
//             
//             //Push as last
//             _systems[_systemCount] = system;
//             _systemCount++;
//             
//             if (_enabled)
//                 this.EnableSystem(system);
//             
//             this.OnSystemAdded?.Invoke(this, system);
//             this.NotifyAboutStateChanged();
//         }
//
//         public bool HasSystem(IEntitySystem<E> system)
//         {
//             if (system == null)
//                 return false;
//             
//             for (int i = 0; i < _systemCount; i++)
//                 if (_systems[i] == system)
//                     return true;
//             
//             return false;
//         }
//
//         public bool HasSystem<T>() where T : IEntitySystem<E>
//         {
//             for (int i = 0; i < _systemCount; i++)
//                 if (_systems[i] is T)
//                     return true;
//             
//             return false;
//         }
//
//         public bool DelSystem(IEntitySystem<E> system)
//         {
//             if (system == null)
//                 return false;
//             
//             for (int i = 0; i < _systemCount; i++)
//             {
//                 if (_systems[i] != system)
//                     continue;
//             
//                 _systemCount--;
//                 if (i < _systemCount)
//                     Array.Copy(_systems, i + 1, _systems, i, _systemCount - i);
//             
//                 _systems[_systemCount] = null;
//             
//                 if (_enabled)
//                     this.DisableSystem(system);
//             
//                 this.OnSystemDeleted?.Invoke(this, system);
//                 this.NotifyAboutStateChanged();
//                 return true;
//             }
//             
//             return false;
//         }
//
//         public bool DelSystem<T>() where T : IEntitySystem<E>
//         {
//             for (int i = 0; i < _systemCount; i++)
//                 if (_systems[i] is T)
//                     return this.DelSystemAt(i);
//             
//             return false;
//         }
//
//         public void DelSystems<T>() where T : IEntitySystem<E>
//         {
//             for (int i = 0; i < _systemCount; i++)
//                 if (_systems[i] is T)
//                     this.DelSystemAt(i);
//         }
//
//         public void ClearSystems()
//         {
//             if (_systemCount == 0)
//                 return;
//             
//             while (_systemCount > 0)
//             {
//                 _systemCount--;
//                 IEntitySystem<E> system = _systems[_systemCount];
//                 _systems[_systemCount] = null;
//             
//                 if (_enabled)
//                     this.DisableSystem(system);
//                 
//                 this.OnSystemDeleted?.Invoke(this, system);
//             }
//             
//             this.NotifyAboutStateChanged();
//         }
//         
//         /// <summary>
//         /// Removes the system at the specified index.
//         /// </summary>
//         public bool DelSystemAt(int index)
//         {
//             if (index < 0 || index >= _systemCount)
//                 return false;
//         
//             IEntitySystem<E> system = _systems[index];
//         
//             _systemCount--;
//             if (index < _systemCount)
//                 Array.Copy(_systems, index + 1, _systems, index, _systemCount - index);
//            
//             _systems[_systemCount] = null;
//         
//             if (_enabled)
//                 this.DisableSystem(system);
//         
//             this.OnSystemDeleted?.Invoke(this, system);
//             this.NotifyAboutStateChanged();
//             return true;
//         }
//         
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         private void EnableSystem(IEntitySystem<E> system)
//         {
//             if (system is IEnableEntitySystem<E> enable)
//                 enable.Enable(this);
//         
//             if (system is ITickEntitySystem<E> update)
//                 InternalUtils.Add(ref _updates, ref _updateCount, update);
//         
//             if (system is IFixedTickEntitySystem<E> fixedUpdate)
//                 InternalUtils.Add(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate);
//         
//             if (system is ILateTickEntitySystem<E> lateUpdate)
//                 InternalUtils.Add(ref _lateUpdates, ref _lateUpdateCount, lateUpdate);
//         }
//         
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         private void DisableSystem(IEntitySystem<E> system)
//         {
//             if (system is IDisableEntitySystem<E> disable)
//                 disable.Disable(this);
//         
//             if (system is IEntityTick update)
//                 Remove(ref _updates, ref _updateCount, update, s_tickSystemComparer);
//         
//             if (system is IEntityFixedTick fixedUpdate)
//                 Remove(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate, s_fixedTickSystemComparer);
//         
//             if (system is IEntityLateTick lateUpdate)
//                 Remove(ref _lateUpdates, ref _lateUpdateCount, lateUpdate, s_lateTickSystemComparer);
//         }
//     }
// }