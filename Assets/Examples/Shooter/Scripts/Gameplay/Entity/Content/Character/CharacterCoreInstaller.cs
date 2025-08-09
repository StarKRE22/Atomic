// using Atomic.Elements;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterCoreInstaller : SceneEntityInstaller
//     {
//         [SerializeField]
//         private float _moveSpeed = 3;
//
//         [SerializeField]
//         private float _angularSpeed = 15;
//
//         [SerializeField]
//         private int _health = 3;
//
//         [SerializeField]
//         private GameObject _gameObject;
//
//         [SerializeField]
//         private Transform _transform;
//
//         [SerializeField]
//         private SceneEntity _initialWeapon;
//
//         [SerializeField]
//         private TeamType _team;
//
//         public override void Install(IEntity entity)
//         {
//             //Main
//             entity.AddGameObject(_gameObject);
//             entity.AddTransform(_transform);
//             
//             //Team
//             entity.AddTeam(new ReactiveVariable<TeamType>(_team));
//             entity.AddBehaviour<TeamLayerBehaviour>();
//
//             //Life
//             entity.AddDamageableTag();
//             entity.AddHealth(new Health(_health, _health));
//             entity.AddTakeDamageEvent(new BaseEvent<DamageArgs>());
//             entity.AddTakeDeathEvent(new BaseEvent<DamageArgs>());
//
//             //Combat
//             entity.AddFireCondition(new AndExpression(entity.GetHealth().Exists));
//             entity.AddFireAction(new CharacterFireAction(entity));
//             entity.AddFireEvent(new BaseEvent());
//             entity.AddWeapon(_initialWeapon);
//             entity.WhenInit(() => _initialWeapon.GetTeam().Value = _team);
//
//             //Movement:
//             entity.AddMoveDirection(new ReactiveVector3(Vector3.zero));
//             entity.AddMoveCondition(new AndExpression(entity.GetHealth().Exists));
//             entity.AddMoveSpeed(new ReactiveVariable<float>(_moveSpeed));
//             entity.AddBehaviour<CharacterMoveBehaviour>();
//             entity.AddAngularSpeed(new Const<float>(_angularSpeed));
//         }
//     }
// }