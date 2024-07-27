using UnityEngine;
using UnityEngine.UI;

namespace SampleArcade.UI
{
    public class HeathBarUI : MonoBehaviour
    {
        [SerializeField]
        private Slider _healthSlider;
        private Transform _cameraTransform;

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
            _cameraTransform = UnityEngine.Camera.main.transform;
        }

        private void LateUpdate()
        {
            // Always face the camera
            transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
        }
        public void SetHealth(float healthPercent)
        {
            _healthSlider.value = healthPercent;
        }
    }
}