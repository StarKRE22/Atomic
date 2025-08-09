// using Atomic.Elements;
// using Atomic.Entities;
// using Atomic.Presenters;
// using ShooterGame.Gameplay;
// using UnityEngine;
//
// namespace ShooterGame.UI
// {
//     public sealed class HitPointsPresenter : Presenter
//     {
//         [SerializeField]
//         private HitPointsView _view;
//
//         [SerializeField]
//         private SceneEntity _character;
//         
//         private Health _health;
//         private TeamConfig _teamConfig;
//         private IValue<TeamType> _teamType;
//
//         protected override void OnInit()
//         {
//             _health = _character.GetHealth();
//             _teamType = _character.GetTeam();
//             _teamConfig = GameContext.Instance.GetTeamConfig();
//         }
//
//         protected override void OnShow()
//         {
//             _health.OnStateChanged += this.OnHealthChanged;
//             _view.Hide();
//         }
//
//         protected override void OnHide()
//         {
//             _health.OnStateChanged -= this.OnHealthChanged;
//         }
//
//         private void OnHealthChanged()
//         {
//             _view.SetColor(_teamConfig.GetTeam(_teamType.Value).Material.color);
//             _view.SetProgress(_health.GetPercent());
//             _view.SetText($"{_health.GetCurrent()}/{_health.GetMax()}");
//             _view.Show();
//         }
//     }
// }