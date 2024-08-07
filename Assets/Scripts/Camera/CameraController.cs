﻿using UnityEngine;

namespace SampleArcade.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _followCameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;

        private PlayerCharacterView _player;

        protected void LateUpdate()
        {
            if (_player != null)
            {
                Vector3 targetRotation = _rotationOffset - _followCameraOffset;

                transform.position = _player.transform.position + _followCameraOffset;
                transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
            }
        }

        public void SetPlayer(PlayerCharacterView player)
        {
            _player = player;
        }
    }
}
