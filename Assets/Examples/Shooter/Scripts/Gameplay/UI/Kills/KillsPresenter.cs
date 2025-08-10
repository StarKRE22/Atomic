// using Atomic.Elements;
// using Atomic.Presenters;
// using ShooterGame.Gameplay;
// using TMPro;
// using UnityEngine;
//
// namespace ShooterGame.UI
// {
//     public sealed class KillsPresenter : Presenter
//     {
//         [SerializeField]
//         private TMP_Text _text;
//         
//         [SerializeField]
//         private TeamType _teamType;
//
//         private IReactiveDictionary<TeamType, int> _leaderboard;
//
//         protected override void OnShow()
//         {
//             _leaderboard = GameContext.Instance.GetLeaderboard();
//             _leaderboard.OnItemChanged += this.OnLeaderboardChanged;
//             this.UpdateText(_leaderboard[_teamType]);
//         }
//
//         protected override void OnHide()
//         {
//             _leaderboard.OnItemChanged -= this.OnLeaderboardChanged;
//         }
//
//         private void OnLeaderboardChanged(TeamType teamType, int value)
//         {
//             if (_teamType == teamType) 
//                 this.UpdateText(value);
//         }
//
//         private void UpdateText(int value)
//         {
//             _text.text = $"Kills: {value}";
//         }
//     }
// }