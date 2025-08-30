using Atomic.Elements;
using Atomic.Entities;
using TMPro;

namespace ShooterGame.Gameplay
{
    public sealed class KillsPresenter : IEntityInit, IEntityDispose
    {
        private readonly TMP_Text _text;
        private readonly TeamType _teamType;

        private IReactiveDictionary<TeamType, int> _leaderboard;

        public KillsPresenter(TMP_Text text, TeamType teamType)
        {
            _text = text;
            _teamType = teamType;
        }

        private void OnLeaderboardChanged(TeamType teamType, int value)
        {
            if (_teamType == teamType) 
                this.UpdateText(value);
        }

        private void UpdateText(int value)
        {
            _text.text = $"Kills: {value}";
        }

        public void Init(IEntity entity)
        {
            _leaderboard = GameContext.Instance.GetLeaderboard();
            _leaderboard.OnItemChanged += this.OnLeaderboardChanged;
            this.UpdateText(_leaderboard[_teamType]);
        }

        public void Dispose(IEntity entity)
        {
            _leaderboard.OnItemChanged -= this.OnLeaderboardChanged;
        }
    }
}