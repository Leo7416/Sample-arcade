using UnityEngine;

namespace SampleArcade.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }
        public GameObject ClosestCharacter { get; private set; }

        private readonly float _viewRadius;
        private readonly EnemyCharacterView _agent;

        public PlayerCharacterView Player { get; set; }

        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(EnemyCharacterView agent, PlayerCharacterView player, float viewRadius)
        {
            _agent = agent;
            _viewRadius = viewRadius;
            Player = player;
        }

        public void FindClosest()
        {
            var minDistance = float.MaxValue;
            GameObject closestPickUp = null;
            float minPickUpDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpMask | LayerUtils.CharacterMask);

            for (var i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;

                if (go == _agent.gameObject) continue;

                var distance = DistanceFromAgentTo(go);

                if (LayerUtils.IsWeaponPickUp(go))
                {
                    if (!_agent.HasBaseWeapon() && distance < minPickUpDistance)
                    {
                        minPickUpDistance = distance;
                        closestPickUp = go;
                    }
                    else if (_agent.HasBaseWeapon() && distance < minDistance)
                    {
                        minPickUpDistance = distance;
                        closestPickUp = go;
                    }
                }
                else
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
            }

            if (_agent.HasBaseWeapon() && closestPickUp != null)
            {
                Closest = closestPickUp;
            }

            if (Player != null && DistanceFromAgentTo(Player.gameObject) < minDistance)
                Closest = Player.gameObject;
        }

        public float DistanceClosestFromAgent()
        {
            if (Closest != null)
            {
                return DistanceFromAgentTo(Closest);
            }
            return 0;
        }

        public bool IsTargetCharacter()
        {
            return LayerUtils.IsCharacter(Closest);
        }

        private int FindAllTargets(int layerMask)
        {
            return Physics.OverlapSphereNonAlloc(
                _agent.transform.position,
                _viewRadius,
                _colliders,
                layerMask);
        }

        private float DistanceFromAgentTo(GameObject go) => (_agent.transform.position - go.transform.position).magnitude;
    }
}
