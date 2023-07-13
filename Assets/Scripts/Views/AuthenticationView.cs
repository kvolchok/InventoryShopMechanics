using System;
using Api;
using TMPro;
using UnityEngine;
using User;

namespace Views
{
    public class AuthenticationView : MonoBehaviour
    {
        public event Action<MonoBehaviour, UserProfile> Authorized;
        public event Action<string> Unauthorized;
        
        private const string INVALID_INPUT_MESSAGE = "Login or Password is empty.";

        [SerializeField] 
        private TMP_InputField _usernameInput;
        [SerializeField] 
        private TMP_InputField _passwordInput;
        [SerializeField]
        private GameObject _preloader;

        public void Register()
        {
            if (!ValidateInput())
            {
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendRegistrationRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        public void LogIn()
        {
            if (!ValidateInput())
            {
                return;
            }

            ShowPreloader();
            WebApi.Instance.AuthenticationAPI.SendLoginRequest(_usernameInput.text, _passwordInput.text,
                OnSuccess, OnError);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(_usernameInput.text) || string.IsNullOrEmpty(_passwordInput.text))
            {
                Unauthorized?.Invoke(INVALID_INPUT_MESSAGE);
                return false;
            }

            return true;
        }

        private void OnSuccess(UserProfile userProfile)
        {
            HidePreloader();
            Authorized?.Invoke(this, userProfile);
        }

        private void OnError(string errorMessage)
        {
            HidePreloader();
            Unauthorized?.Invoke(errorMessage);
        }

        private void ShowPreloader()
        {
            _preloader.SetActive(true);
        }
        
        private void HidePreloader()
        {
            _preloader.SetActive(false);
        }
    }
}