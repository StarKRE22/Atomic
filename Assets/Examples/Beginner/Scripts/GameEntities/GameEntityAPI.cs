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
		public static bool HasCharacterTag(this GameEntity entity) => entity.HasTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacterTag(this GameEntity entity) => entity.AddTag(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacterTag(this GameEntity entity) => entity.DelTag(Character);

		#endregion

		#region Coin

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCoinTag(this GameEntity entity) => entity.HasTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCoinTag(this GameEntity entity) => entity.AddTag(Coin);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCoinTag(this GameEntity entity) => entity.DelTag(Coin);

		#endregion


		///Value Extensions

		#region Position

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetPosition(this GameEntity entity) => entity.GetValue<IVariable<Vector3>>(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPosition(this GameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(Position, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPosition(this GameEntity entity, IVariable<Vector3> value) => entity.AddValue(Position, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPosition(this GameEntity entity) => entity.HasValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPosition(this GameEntity entity) => entity.DelValue(Position);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPosition(this GameEntity entity, IVariable<Vector3> value) => entity.SetValue(Position, value);

		#endregion

		#region Rotation

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Quaternion> GetRotation(this GameEntity entity) => entity.GetValue<IVariable<Quaternion>>(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotation(this GameEntity entity, out IVariable<Quaternion> value) => entity.TryGetValue(Rotation, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotation(this GameEntity entity, IVariable<Quaternion> value) => entity.AddValue(Rotation, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotation(this GameEntity entity) => entity.HasValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotation(this GameEntity entity) => entity.DelValue(Rotation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotation(this GameEntity entity, IVariable<Quaternion> value) => entity.SetValue(Rotation, value);

		#endregion

		#region MoveSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetMoveSpeed(this GameEntity entity) => entity.GetValue<IValue<float>>(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveSpeed(this GameEntity entity, out IValue<float> value) => entity.TryGetValue(MoveSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveSpeed(this GameEntity entity, IValue<float> value) => entity.AddValue(MoveSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveSpeed(this GameEntity entity) => entity.HasValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveSpeed(this GameEntity entity) => entity.DelValue(MoveSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveSpeed(this GameEntity entity, IValue<float> value) => entity.SetValue(MoveSpeed, value);

		#endregion

		#region MoveDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetMoveDirection(this GameEntity entity) => entity.GetValue<IVariable<Vector3>>(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveDirection(this GameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(MoveDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoveDirection(this GameEntity entity, IVariable<Vector3> value) => entity.AddValue(MoveDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveDirection(this GameEntity entity) => entity.HasValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveDirection(this GameEntity entity) => entity.DelValue(MoveDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveDirection(this GameEntity entity, IVariable<Vector3> value) => entity.SetValue(MoveDirection, value);

		#endregion

		#region RotationSpeed

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<float> GetRotationSpeed(this GameEntity entity) => entity.GetValue<IValue<float>>(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationSpeed(this GameEntity entity, out IValue<float> value) => entity.TryGetValue(RotationSpeed, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationSpeed(this GameEntity entity, IValue<float> value) => entity.AddValue(RotationSpeed, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationSpeed(this GameEntity entity) => entity.HasValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationSpeed(this GameEntity entity) => entity.DelValue(RotationSpeed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationSpeed(this GameEntity entity, IValue<float> value) => entity.SetValue(RotationSpeed, value);

		#endregion

		#region RotationDirection

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IVariable<Vector3> GetRotationDirection(this GameEntity entity) => entity.GetValue<IVariable<Vector3>>(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRotationDirection(this GameEntity entity, out IVariable<Vector3> value) => entity.TryGetValue(RotationDirection, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRotationDirection(this GameEntity entity, IVariable<Vector3> value) => entity.AddValue(RotationDirection, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRotationDirection(this GameEntity entity) => entity.HasValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRotationDirection(this GameEntity entity) => entity.DelValue(RotationDirection);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRotationDirection(this GameEntity entity, IVariable<Vector3> value) => entity.SetValue(RotationDirection, value);

		#endregion

		#region TeamType

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetTeamType(this GameEntity entity) => entity.GetValue<IReactiveVariable<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this GameEntity entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamType(this GameEntity entity, IReactiveVariable<TeamType> value) => entity.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this GameEntity entity) => entity.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this GameEntity entity) => entity.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this GameEntity entity, IReactiveVariable<TeamType> value) => entity.SetValue(TeamType, value);

		#endregion

		#region TriggerEvents

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TriggerEvents GetTriggerEvents(this GameEntity entity) => entity.GetValue<TriggerEvents>(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTriggerEvents(this GameEntity entity, out TriggerEvents value) => entity.TryGetValue(TriggerEvents, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTriggerEvents(this GameEntity entity, TriggerEvents value) => entity.AddValue(TriggerEvents, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTriggerEvents(this GameEntity entity) => entity.HasValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTriggerEvents(this GameEntity entity) => entity.DelValue(TriggerEvents);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTriggerEvents(this GameEntity entity, TriggerEvents value) => entity.SetValue(TriggerEvents, value);

		#endregion

		#region Money

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IValue<int> GetMoney(this GameEntity entity) => entity.GetValue<IValue<int>>(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoney(this GameEntity entity, out IValue<int> value) => entity.TryGetValue(Money, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoney(this GameEntity entity, IValue<int> value) => entity.AddValue(Money, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoney(this GameEntity entity) => entity.HasValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoney(this GameEntity entity) => entity.DelValue(Money);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoney(this GameEntity entity, IValue<int> value) => entity.SetValue(Money, value);

		#endregion

		#region Renderer

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Renderer GetRenderer(this GameEntity entity) => entity.GetValue<Renderer>(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetRenderer(this GameEntity entity, out Renderer value) => entity.TryGetValue(Renderer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddRenderer(this GameEntity entity, Renderer value) => entity.AddValue(Renderer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasRenderer(this GameEntity entity) => entity.HasValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelRenderer(this GameEntity entity) => entity.DelValue(Renderer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetRenderer(this GameEntity entity, Renderer value) => entity.SetValue(Renderer, value);

		#endregion

		#region MoneyView

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoneyView GetMoneyView(this GameEntity entity) => entity.GetValue<MoneyView>(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoneyView(this GameEntity entity, out MoneyView value) => entity.TryGetValue(MoneyView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddMoneyView(this GameEntity entity, MoneyView value) => entity.AddValue(MoneyView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoneyView(this GameEntity entity) => entity.HasValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoneyView(this GameEntity entity) => entity.DelValue(MoneyView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoneyView(this GameEntity entity, MoneyView value) => entity.SetValue(MoneyView, value);

		#endregion
    }
}
