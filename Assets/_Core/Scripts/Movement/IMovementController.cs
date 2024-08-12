using UnityEngine;

namespace SampleArcade.Movement
{
    public interface IMovementController
    {
        Vector3 Translate(Vector3 movementDirection);

        Quaternion Rotate(Quaternion currentRotation, Vector3 lookDirection);

        float SetSprint(bool isSprinting);

        float MultiplySpeedBoost(float boostSpeed);
    }
}