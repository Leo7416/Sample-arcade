using UnityEngine;

namespace SampleArcade.Shooting
{
    public class ShootingTargetGO: IShootingTarget
    {
        private readonly Collider[] _colliders = new Collider[2];
        private readonly GameObject _shooter;

        public ShootingTargetGO(GameObject shooter)
        {
            _shooter = shooter;
        }

        public BaseCharacterModel GetTarget(Vector3 position, float radius)
        {
            BaseCharacterModel target = null;
            var mask = LayerUtils.CharacterMask;

            var size = Physics.OverlapSphereNonAlloc(position, radius, _colliders, mask);
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != _shooter)
                    {
                        target = _colliders[i].gameObject.GetComponent<BaseCharacterView>().Model;
                        break;
                    }
                }
            }

            return target;
        }
    }
}
