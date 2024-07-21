using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }
        public GameObject ClosestCharacter { get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacter _player;
        private readonly BaseCharacter _baseCharacter;

        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(Transform agentTransform, PlayerCharacter player, float viewRadius, BaseCharacter baseCharacter)
        {
            _agentTransform = agentTransform;
            _viewRadius = viewRadius;
            _player = player;
            _baseCharacter = baseCharacter;
        }

        public void FindClosest()
        {
            var closestWeapon = FindClosestObject(LayerUtils.PickUpMask);

            if (closestWeapon != null && !_baseCharacter.HasPickedUpWeapon)
            {
                Closest = closestWeapon;
                ClosestCharacter = null;
            }
            else
            {
                ClosestCharacter = FindClosestObject(LayerUtils.CharacterMask);
                Closest = ClosestCharacter;

                if (_player != null && DistanceFromAgentTo(_player.gameObject) < DistanceClosestFromCharacter())
                {
                    Closest = _player.gameObject;
                }
            }
        }

        private GameObject FindClosestObject(int layerMask)
        {
            float minDistance = float.MaxValue;
            GameObject closestObject = null;

            int count = FindAllTargets(layerMask);
            for (int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;

                float distance = DistanceFromAgentTo(go);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestObject = go;
                }
            }

            return closestObject;
        }

        public float DistanceClosestFromAgent()
        {
            if (Closest != null)
            {
                return DistanceFromAgentTo(Closest);
            }
            return float.MaxValue;
        }

        public float DistanceClosestFromCharacter()
        {
            if (ClosestCharacter != null)
            {
                return DistanceFromAgentTo(ClosestCharacter);
            }
            return float.MaxValue;
        }

        private int FindAllTargets(int layerMask)
        {
            return Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _colliders,
                layerMask);
        }

        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}
