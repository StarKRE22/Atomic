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
using BeginnerGame;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameEntityAPI
	{

		///Tags
		public static readonly int Character;
		public static readonly int Coin;

		///Values
		public static readonly int Position; // IVariable<Vector3>
		public static readonly int Rotation; // IVariable<Quaternion>
		public static readonly int MoveSpeed; // IValue<float>
		public static readonly int MoveDirection; // IVariable<Vector3>
		public static readonly int RotationSpeed; // IValue<float>
		public static readonly int RotationDirection; // IVariable<Vector3>
		public static readonly int TeamType; // IReactiveVariable<TeamType>
		public static readonly int TriggerEvents; // TriggerEvents
		public static readonly int Money; // IValue<int>
		public static readonly int Renderer; // Renderer
		public static readonly int MoneyView; // MoneyView

		static GameEntityAPI()
		{
			//Tags
			Character = NameToId(nameof(Character));
			Coin = NameToId(nameof(Coin));

			//Values
			Position = NameToId(nameof(Position));
			Rotation = NameToId(nameof(Rotation));
			MoveSpeed = NameToId(nameof(MoveSpeed));
			MoveDirection = NameToId(nameof(MoveDirection));
			RotationSpeed = NameToId(nameof(RotationSpeed));
			RotationDirection = NameToId(nameof(RotationDirection));
			TeamType = NameToId(nameof(TeamType));
			TriggerEvents = NameToId(nameof(TriggerEvents));
			Money = NameToId(nameof(Money));
			Renderer = NameToId(nameof(Renderer));
			MoneyView = NameToId(nameof(MoneyView));
		}


		///Tag Extensions

		#region Character

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacterTag(this IGameEntity entity) => entity.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this IGameEntity entity) => entity.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this IGameEntity entity) => entity.DelTag(Character);

		#endregion

		#region Coin

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinTag(this IGameEntity entity) => entity.HasTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinTag(this IGameEntity entity) => entity.AddTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinTag(this IGameEntity entity) => entity.DelTag(Coin);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetPosition(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this IGameEntity entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this IGameEntity entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Quaternion> GetRotation(this IGameEntity entity) => entity.GetValue<IVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this IGameEntity entity, out IVariable<Quaternion> value) => entity.TryGetValue(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this IGameEntity entity, IVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this IGameEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this IGameEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this IGameEntity entity, IVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this IGameEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this IGameEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetMoveDirection(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveDirection(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(MoveDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveDirection(this IGameEntity entity) => entity.HasValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveDirection(this IGameEntity entity) => entity.DelValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(MoveDirection, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this IGameEntity entity) => entity.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this IGameEntity entity, out IValue<float> value) => entity.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this IGameEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this IGameEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this IGameEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region RotationDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetRotationDirection(this IGameEntity entity) => entity.GetValue<IVariable<Vector3>>(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationDirection(this IGameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(RotationDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.AddValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationDirection(this IGameEntity entity) => entity.HasValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationDirection(this IGameEntity entity) => entity.DelValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationDirection(this IGameEntity entity, IVariable<Vector3> value) => entity.SetValue(RotationDirection, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeamType(this IGameEntity entity) => entity.GetValue<IReactiveVariable<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IGameEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IGameEntity entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IGameEntity entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IGameEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region TriggerEvents

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTriggerEvents(this IGameEntity entity) => entity.GetValue<TriggerEvents>(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerEvents(this IGameEntity entity, out TriggerEvents value) => entity.TryGetValue(TriggerEvents, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerEvents(this IGameEntity entity, TriggerEvents value) => entity.AddValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerEvents(this IGameEntity entity) => entity.HasValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerEvents(this IGameEntity entity) => entity.DelValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerEvents(this IGameEntity entity, TriggerEvents value) => entity.SetValue(TriggerEvents, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetMoney(this IGameEntity entity) => entity.GetValue<IValue<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this IGameEntity entity, out IValue<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this IGameEntity entity, IValue<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this IGameEntity entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this IGameEntity entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this IGameEntity entity, IValue<int> value) => entity.SetValue(Money, value);

		#endregion

		#region Renderer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetRenderer(this IGameEntity entity) => entity.GetValue<Renderer>(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRenderer(this IGameEntity entity, out Renderer value) => entity.TryGetValue(Renderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRenderer(this IGameEntity entity, Renderer value) => entity.AddValue(Renderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRenderer(this IGameEntity entity) => entity.HasValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRenderer(this IGameEntity entity) => entity.DelValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRenderer(this IGameEntity entity, Renderer value) => entity.SetValue(Renderer, value);

		#endregion

		#region MoneyView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoneyView GetMoneyView(this IGameEntity entity) => entity.GetValue<MoneyView>(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoneyView(this IGameEntity entity, out MoneyView value) => entity.TryGetValue(MoneyView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoneyView(this IGameEntity entity, MoneyView value) => entity.AddValue(MoneyView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoneyView(this IGameEntity entity) => entity.HasValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoneyView(this IGameEntity entity) => entity.DelValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoneyView(this IGameEntity entity, MoneyView value) => entity.SetValue(MoneyView, value);

		#endregion
    }
}
