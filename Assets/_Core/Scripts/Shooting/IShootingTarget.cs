using UnityEngine;

namespace SampleArcade.Shooting
{
    public interface IShootingTarget
    {
        BaseCharacterModel GetTarget(Vector3 position, float radius);
    }
}