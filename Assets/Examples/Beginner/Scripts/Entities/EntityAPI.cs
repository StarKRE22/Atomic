/**
* Code generation. Don't modify! 
**/

using Atomic.Entities;
using static Atomic.Entities.EntityNames;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Atomic.Elements;
using System.Collections.Generic;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class EntityAPI
	{

		///Tags
		public static readonly int Character;
		public static readonly int Coin;
		public static readonly int GameContext;

		///Values
		public static readonly int Transform; // Transform
		public static readonly int MovementDirection; // IVariable<Vector3>
		public static readonly int MovementSpeed; // IValue<float>
		public static readonly int Money; // IReactiveVariable<int>
		public static readonly int CoinSpawnInfo; // SpawnInfo
		public static readonly int InputMap; // InputMap
		public static readonly int TriggerEvents; // TriggerEvents
		public static readonly int Players; // IDictionary<TeamType, IEntity>
		public static readonly int GameCountdown; // ICooldown

		static EntityAPI()
		{
			//Tags
			Character = NameToId(nameof(Character));
			Coin = NameToId(nameof(Coin));
			GameContext = NameToId(nameof(GameContext));

			//Values
			Transform = NameToId(nameof(Transform));
			MovementDirection = NameToId(nameof(MovementDirection));
			MovementSpeed = NameToId(nameof(MovementSpeed));
			Money = NameToId(nameof(Money));
			CoinSpawnInfo = NameToId(nameof(CoinSpawnInfo));
			InputMap = NameToId(nameof(InputMap));
			TriggerEvents = NameToId(nameof(TriggerEvents));
			Players = NameToId(nameof(Players));
			GameCountdown = NameToId(nameof(GameCountdown));
		}


		///Tag Extensions

		#region Character

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterTag(this IEntity entity) => entity.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this IEntity entity) => entity.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this IEntity entity) => entity.DelTag(Character);

		#endregion

		#region Coin

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinTag(this IEntity entity) => entity.HasTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinTag(this IEntity entity) => entity.AddTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinTag(this IEntity entity) => entity.DelTag(Coin);

		#endregion

		#region GameContext

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameContextTag(this IEntity entity) => entity.HasTag(GameContext);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameContextTag(this IEntity entity) => entity.AddTag(GameContext);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameContextTag(this IEntity entity) => entity.DelTag(GameContext);

		#endregion


		///Value Extensions

		#region Transform

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetTransform(this IEntity entity) => entity.GetValue<Transform>(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTransform(this IEntity entity, out Transform value) => entity.TryGetValue(Transform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTransform(this IEntity entity, Transform value) => entity.AddValue(Transform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTransform(this IEntity entity) => entity.HasValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTransform(this IEntity entity) => entity.DelValue(Transform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTransform(this IEntity entity, Transform value) => entity.SetValue(Transform, value);

		#endregion

		#region MovementDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetMovementDirection(this IEntity entity) => entity.GetValue<IVariable<Vector3>>(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementDirection(this IEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(MovementDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementDirection(this IEntity entity, IVariable<Vector3> value) => entity.AddValue(MovementDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementDirection(this IEntity entity) => entity.HasValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementDirection(this IEntity entity) => entity.DelValue(MovementDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementDirection(this IEntity entity, IVariable<Vector3> value) => entity.SetValue(MovementDirection, value);

		#endregion

		#region MovementSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMovementSpeed(this IEntity entity) => entity.GetValue<IValue<float>>(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMovementSpeed(this IEntity entity, out IValue<float> value) => entity.TryGetValue(MovementSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMovementSpeed(this IEntity entity, IValue<float> value) => entity.AddValue(MovementSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMovementSpeed(this IEntity entity) => entity.HasValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMovementSpeed(this IEntity entity) => entity.DelValue(MovementSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMovementSpeed(this IEntity entity, IValue<float> value) => entity.SetValue(MovementSpeed, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<int> GetMoney(this IEntity entity) => entity.GetValue<IReactiveVariable<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IEntity entity, out IReactiveVariable<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IEntity entity, IReactiveVariable<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IEntity entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IEntity entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IEntity entity, IReactiveVariable<int> value) => entity.SetValue(Money, value);

		#endregion

		#region CoinSpawnInfo

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SpawnInfo GetCoinSpawnInfo(this IEntity entity) => entity.GetValue<SpawnInfo>(CoinSpawnInfo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCoinSpawnInfo(this IEntity entity, out SpawnInfo value) => entity.TryGetValue(CoinSpawnInfo, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddCoinSpawnInfo(this IEntity entity, SpawnInfo value) => entity.AddValue(CoinSpawnInfo, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinSpawnInfo(this IEntity entity) => entity.HasValue(CoinSpawnInfo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinSpawnInfo(this IEntity entity) => entity.DelValue(CoinSpawnInfo);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCoinSpawnInfo(this IEntity entity, SpawnInfo value) => entity.SetValue(CoinSpawnInfo, value);

		#endregion

		#region InputMap

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IEntity entity) => entity.GetValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IEntity entity, out InputMap value) => entity.TryGetValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddInputMap(this IEntity entity, InputMap value) => entity.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IEntity entity) => entity.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IEntity entity) => entity.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IEntity entity, InputMap value) => entity.SetValue(InputMap, value);

		#endregion

		#region TriggerEvents

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTriggerEvents(this IEntity entity) => entity.GetValue<TriggerEvents>(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerEvents(this IEntity entity, out TriggerEvents value) => entity.TryGetValue(TriggerEvents, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerEvents(this IEntity entity, TriggerEvents value) => entity.AddValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerEvents(this IEntity entity) => entity.HasValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerEvents(this IEntity entity) => entity.DelValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerEvents(this IEntity entity, TriggerEvents value) => entity.SetValue(TriggerEvents, value);

		#endregion

		#region Players

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<TeamType, IEntity> GetPlayers(this IEntity entity) => entity.GetValue<IDictionary<TeamType, IEntity>>(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayers(this IEntity entity, out IDictionary<TeamType, IEntity> value) => entity.TryGetValue(Players, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayers(this IEntity entity, IDictionary<TeamType, IEntity> value) => entity.AddValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayers(this IEntity entity) => entity.HasValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayers(this IEntity entity) => entity.DelValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayers(this IEntity entity, IDictionary<TeamType, IEntity> value) => entity.SetValue(Players, value);

		#endregion

		#region GameCountdown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICooldown GetGameCountdown(this IEntity entity) => entity.GetValue<ICooldown>(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdown(this IEntity entity, out ICooldown value) => entity.TryGetValue(GameCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameCountdown(this IEntity entity, ICooldown value) => entity.AddValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdown(this IEntity entity) => entity.HasValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdown(this IEntity entity) => entity.DelValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdown(this IEntity entity, ICooldown value) => entity.SetValue(GameCountdown, value);

		#endregion
    }
}
