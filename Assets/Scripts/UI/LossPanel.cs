using SampleArcade.GameManagers;
using UnityEngine;

namespace SampleArcade.UI
{
    public class LossPanel : MonoBehaviour
    {
        [SerializeField]
        private GameManager _gameManager;

        protected void Start()
        {
            _gameManager.Loss += ShowPanel;
            gameObject.SetActive(false);
        }

        private void ShowPanel()
        {
            gameObject.SetActive(true);
        }
    }
}