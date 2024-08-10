using UnityEngine;

namespace SampleArcade.Camera
{
    public class CameraControllerModel
    {
        private Vector3 _followCameraOffset;
        private Vector3 _rotationOffset;

        public TransformModel TransformModel { get; private set; }

        public CameraControllerModel(Vector3 followCameraOffset, Vector3 rotationOffset)
        {
            _followCameraOffset = followCameraOffset;
            _rotationOffset = rotationOffset;
            TransformModel = new TransformModel(Vector3.zero, Quaternion.identity);
        }

        public void UpdateCameraPosition(Vector3 playerPosition)
        {
            Vector3 targetRotation = _rotationOffset - _followCameraOffset;

            TransformModel.Position = playerPosition + _followCameraOffset;
            TransformModel.Rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
        }
    }
}
