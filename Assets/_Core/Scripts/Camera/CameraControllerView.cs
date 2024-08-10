using SampleArcade.Player;
using UnityEngine;

namespace SampleArcade.Camera
{
    public class CameraControllerView : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;

        private CameraControllerModel _model;

        private PlayerCharacterView _player;

        protected void Awake()
        {
            _model = new CameraControllerModel(_followCameraOffset, _rotationOffset);
        }

        protected void LateUpdate()
        {
            if (_player != null)
            {
                _model.UpdateCameraPosition(_player.transform.position);

                transform.position = _model.TransformModel.Position;
                transform.rotation = _model.TransformModel.Rotation;
            }
        }

        public void SetPlayer(PlayerCharacterView player)
        {
            _player = player;
        }
    }
}
