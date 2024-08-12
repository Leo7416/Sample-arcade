using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SampleArcade.Scripts.UI
{
    public class CloseGame : MonoBehaviour
    {

        private Button _closeGameButton;

        protected void Start()
        {
            _closeGameButton = GetComponent<Button>();
            _closeGameButton.onClick.RemoveAllListeners();
            _closeGameButton.onClick.AddListener(Close);
        }

        private void Close()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}