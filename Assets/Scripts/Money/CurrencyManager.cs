using System;
using Api;
using Api.Responses;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class CurrencyManager : MonoBehaviour
    {
        public UnityEvent<int> MoneyValueChanged;
        public UnityEvent<int> GemsValueChanged;

        public event Action<int> MoneyChanged;
        public event Action<string> ErrorEvent;

        [SerializeField]
        private int _replenishmentAmount;
        
        private int _money;
        private int _gems;

        public void Initialize(int money, int gems)
        {
            SetMoney(money);
            SetGems(gems);
        }

        [UsedImplicitly]
        public void TryAddMoney()
        {
            WebApi.Instance.UserMoneyApi.SendAddMoneyRequest(_replenishmentAmount, OnSuccess, OnError);
        }

        private void SetMoney(int value)
        {
            _money = value;
            MoneyValueChanged.Invoke(_money);
        }

        private void SetGems(int value)
        {
            _gems = value;
            GemsValueChanged.Invoke(_gems);
        }

        private void OnSuccess(UserMoneyResponse response)
        {
            var delta = response.Money - _money;
            MoneyChanged?.Invoke(delta);
        }
        
        private void OnError(string message)
        {
            ErrorEvent?.Invoke(message);
        }
    }
}