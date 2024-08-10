using UnityEngine;
using SampleArcade.GameManagers;

namespace SampleArcade.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        [SerializeField]
        private AudioSource _winSound;
        
        protected void Start()
        {
            _gameManager.Win += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
            _winSound.Play();
        }
    }
}