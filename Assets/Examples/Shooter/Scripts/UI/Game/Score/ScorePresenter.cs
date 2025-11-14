using Atomic.Elements;
using ShooterGame.Gameplay;
using TMPro;

namespace ShooterGame.UI
{
    public sealed class ScorePresenter : IGameUIInit, IGameUIDispose
    {
        private readonly TMP_Text _text;
        private readonly TeamType _teamType;
        private readonly IGameContext _gameContext;
        
        private IReactiveDictionary<TeamType, int> _leaderboard;
        
        public ScorePresenter(TMP_Text text, TeamType teamType, IGameContext gameContext)
        {
            _text = text;
            _teamType = teamType;
            _gameContext = gameContext;
        }

        public void Init(IGameUI entity)
        {
            _leaderboard = _gameContext.GetLeaderboard();
            _leaderboard.OnItemChanged += this.OnLeaderboardChanged;
            this.UpdateText(_leaderboard[_teamType]);
        }

        public void Dispose(IGameUI entity)
        {
            _leaderboard.OnItemChanged -= this.OnLeaderboardChanged;
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
    }
}