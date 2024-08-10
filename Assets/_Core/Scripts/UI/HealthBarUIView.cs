using UnityEngine;
using UnityEngine.UI;

namespace SampleArcade.UI
{
    public class HealthBarUIView : MonoBehaviour
    {
        [SerializeField]
        private Slider _healthSlider;

        private Transform _cameraTransform;

        private HealthBarUIModel Model;

        private float _initializeHealth;

        private void Awake()
        {
            _healthSlider = GetComponent<Slider>();
            _cameraTransform = UnityEngine.Camera.main.transform;

            Model = new HealthBarUIModel(_initializeHealth);
        }

        private void LateUpdate()
        {
            if (_cameraTransform != null)
            {
                transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward,
                                 _cameraTransform.rotation * Vector3.up);
            }
        }

        public void UpdateHealth(float healthPercent)
        {
            Model.UpdateHealth(healthPercent);
            _initializeHealth = healthPercent;
            UpdateView();
        }

        private void UpdateView()
        {
            _healthSlider.value = Model.HealthPercent;
        }
    }
}