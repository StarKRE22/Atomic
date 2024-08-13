using System;
using System.Collections.Generic;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.UI;
using TMPro;
using UnityEngine;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class MoneyPresenter : IViewInit, IViewEnable, IViewDisable
    {
		[SerializeField]
        private TMP_Text moneyText;

        [SerializeField]
        private TeamType teamType;

        private IReactiveValue<int> _money;
        
        public void Init()
        {
            Dictionary<TeamType,IContext> playerMap = GameContext.Instance.GetPlayerMap();
            IContext playerContext = playerMap[this.teamType];
            _money = playerContext.GetMoney();
        }

        public void Enable()
        {
            _money.Observe(this.OnMoneyChanged);
        }

        public void Disable()
        {
            _money.Unsubscribe(this.OnMoneyChanged);
        }

        private void OnMoneyChanged(int money)
        {
            this.moneyText.text = $"Money: {money}";
        }
    }
}