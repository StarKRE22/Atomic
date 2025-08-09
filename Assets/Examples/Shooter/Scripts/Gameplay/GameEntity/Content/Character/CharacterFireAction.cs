using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterFireAction : IAction
    {
        private readonly IGameEntity _character;

        public CharacterFireAction(IGameEntity character)
        {
            _character = character;
        }

        public void Invoke()
        {
            if (_character.GetFireCondition().Value)
            {
                _character.GetWeapon().GetFireAction().Invoke();
                _character.GetFireEvent().Invoke();
            }
        }
    }
}