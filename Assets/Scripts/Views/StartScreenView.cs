using System;
using Api;
using UnityEngine;
using User;

namespace Views
{
    public class StartScreenView : MonoBehaviour
    {
        public event Action<UserProfile> Authorized;
        public event Action<string> Unauthorized;

        [SerializeField]
        private GameObject _loginPopup;
        [SerializeField]
        private GameObject _lobbyScreen;

        private AuthenticationView _authenticationView;

        public void Initialize(AuthenticationView authenticationView)
        {
            _authenticationView = authenticationView;
            _authenticationView.Authorized += OnAuthorized;
            _authenticationView.Unauthorized += OnUnauthorized;

            WebApi.Instance.AuthenticationAPI.SendLoginRequestByDeviceId(OnSuccess, OnUnauthorized);
        }

        public void OnGameStarted()
        {
            _loginPopup.SetActive(true);
        }

        private void OnSuccess(UserProfile userProfile)
        {
            Authorized?.Invoke(userProfile);
            LoadLobbyScreen();
        }
        
        private void OnAuthorized(MonoBehaviour popup, UserProfile userProfile)
        {
            Authorized?.Invoke(userProfile);

            popup.gameObject.SetActive(false);
            LoadLobbyScreen();
        }
        
        private void OnUnauthorized(string errorMessage)
        {
            Unauthorized?.Invoke(errorMessage);
        }

        private void LoadLobbyScreen()
        {
            gameObject.SetActive(false);
            _lobbyScreen.SetActive(true);
        }

        private void OnDestroy()
        {
            _authenticationView.Authorized -= OnAuthorized;
            _authenticationView.Unauthorized -= OnUnauthorized;
        }
    }
}