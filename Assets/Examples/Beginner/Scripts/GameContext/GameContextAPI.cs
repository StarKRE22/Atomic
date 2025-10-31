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
using System.Collections.Generic;
using BeginnerGame;
using Atomic.Elements;

namespace BeginnerGame
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public static class GameContextAPI
	{
		///Values
		public static readonly int Players; // IDictionary<TeamType, PlayerContext>
		public static readonly int GameCountdown; // ICooldown
		public static readonly int GameOverEvent; // IEvent
		public static readonly int WinnerTeam; // IReactiveVariable<TeamType>
		public static readonly int TeamCatalog; // TeamCatalog

		static GameContextAPI()
		{
			//Values
			Players = NameToId(nameof(Players));
			GameCountdown = NameToId(nameof(GameCountdown));
			GameOverEvent = NameToId(nameof(GameOverEvent));
			WinnerTeam = NameToId(nameof(WinnerTeam));
			TeamCatalog = NameToId(nameof(TeamCatalog));
		}


		///Value Extensions

		#region Players

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IDictionary<TeamType, PlayerContext> GetPlayers(this GameContext entity) => entity.GetValue<IDictionary<TeamType, PlayerContext>>(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayers(this GameContext entity, out IDictionary<TeamType, PlayerContext> value) => entity.TryGetValue(Players, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddPlayers(this GameContext entity, IDictionary<TeamType, PlayerContext> value) => entity.AddValue(Players, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayers(this GameContext entity) => entity.HasValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayers(this GameContext entity) => entity.DelValue(Players);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayers(this GameContext entity, IDictionary<TeamType, PlayerContext> value) => entity.SetValue(Players, value);

		#endregion

		#region GameCountdown

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICooldown GetGameCountdown(this GameContext entity) => entity.GetValue<ICooldown>(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCountdown(this GameContext entity, out ICooldown value) => entity.TryGetValue(GameCountdown, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameCountdown(this GameContext entity, ICooldown value) => entity.AddValue(GameCountdown, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCountdown(this GameContext entity) => entity.HasValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCountdown(this GameContext entity) => entity.DelValue(GameCountdown);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCountdown(this GameContext entity, ICooldown value) => entity.SetValue(GameCountdown, value);

		#endregion

		#region GameOverEvent

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEvent GetGameOverEvent(this GameContext entity) => entity.GetValue<IEvent>(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameOverEvent(this GameContext entity, out IEvent value) => entity.TryGetValue(GameOverEvent, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddGameOverEvent(this GameContext entity, IEvent value) => entity.AddValue(GameOverEvent, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameOverEvent(this GameContext entity) => entity.HasValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameOverEvent(this GameContext entity) => entity.DelValue(GameOverEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameOverEvent(this GameContext entity, IEvent value) => entity.SetValue(GameOverEvent, value);

		#endregion

		#region WinnerTeam

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IReactiveVariable<TeamType> GetWinnerTeam(this GameContext entity) => entity.GetValue<IReactiveVariable<TeamType>>(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWinnerTeam(this GameContext entity, out IReactiveVariable<TeamType> value) => entity.TryGetValue(WinnerTeam, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddWinnerTeam(this GameContext entity, IReactiveVariable<TeamType> value) => entity.AddValue(WinnerTeam, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWinnerTeam(this GameContext entity) => entity.HasValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWinnerTeam(this GameContext entity) => entity.DelValue(WinnerTeam);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWinnerTeam(this GameContext entity, IReactiveVariable<TeamType> value) => entity.SetValue(WinnerTeam, value);

		#endregion

		#region TeamCatalog

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TeamCatalog GetTeamCatalog(this GameContext entity) => entity.GetValue<TeamCatalog>(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamCatalog(this GameContext entity, out TeamCatalog value) => entity.TryGetValue(TeamCatalog, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void AddTeamCatalog(this GameContext entity, TeamCatalog value) => entity.AddValue(TeamCatalog, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamCatalog(this GameContext entity) => entity.HasValue(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamCatalog(this GameContext entity) => entity.DelValue(TeamCatalog);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamCatalog(this GameContext entity, TeamCatalog value) => entity.SetValue(TeamCatalog, value);

		#endregion
    }
}
