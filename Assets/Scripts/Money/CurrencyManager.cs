using Api;
using Api.Responses;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Money
{
    public class CurrencyManager : MonoBehaviour
    {
        public UnityEvent<int> MoneyValueChanged;
        public UnityEvent<int> GemsValueChanged;

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

        public void ItemBought(int price)
        {
            var newMoney = _money - price;
            SetMoney(newMoney);
        }

        private void SetMoney(int value)
        {
            _money = value;
            MoneyValueChanged?.Invoke(_money);
        }

        private void SetGems(int value)
        {
            _gems = value;
            GemsValueChanged?.Invoke(_gems);
        }

        private void OnSuccess(UserMoneyResponse response)
        {
            var newMoney = _money + response.Money;
            SetMoney(newMoney);
        }
        
        private void OnError(string message)
        {
            NotificationsManager.Instance.ShowNotification(message);
        }
    }
}