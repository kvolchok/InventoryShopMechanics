using UnityEngine;

namespace Views
{
    public class StartScreenView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loginPopup;
        [SerializeField]
        private GameObject _lobbyScreen;

        public void OnGameStarted()
        {
            _loginPopup.SetActive(true);
        }
        
        public void OnAuthorized(MonoBehaviour popup)
        {
            popup.gameObject.SetActive(false);
            LoadLobbyScreen();
        }

        private void LoadLobbyScreen()
        {
            gameObject.SetActive(false);
            _lobbyScreen.SetActive(true);
        }
    }
}