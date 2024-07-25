using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SampleArcade.UI
{
    public class OpenLevel : MonoBehaviour
    {
        [SerializeField]
        private List<int> _levelsList = new List<int>();

        private Button _openLevelButton;

        protected void Start()
        {
            _openLevelButton = GetComponent<Button>();
            _openLevelButton.onClick.RemoveAllListeners();
            _openLevelButton.onClick.AddListener(OpenRandomLevel);
        }

        private void OpenRandomLevel()
        {
            int level = _levelsList[Random.Range(0, _levelsList.Count)];
            SceneManager.LoadScene(level);
        }
    }
}