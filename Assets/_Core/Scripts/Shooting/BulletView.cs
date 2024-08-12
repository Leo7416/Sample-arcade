using SampleArcade.Timer;
using UnityEngine;

namespace SampleArcade.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _barrierParticle;

        private BulletModel _Model;
        private ITimer _timer;
        private Rigidbody _rigidbody;

        public float Damage { get { return _Model?.Damage ?? 0f; } }

        public void Awake()
        {
            _timer = new UnityTimer();
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;
        }

        public void Initialize(float damage, Vector3 direction, float flySpeed, float maxFlyDistance)
        {
            _Model = new BulletModel(damage, direction, flySpeed, maxFlyDistance, _timer);
            _rigidbody.velocity = direction * flySpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var hitObject = collision.gameObject;

            var character = hitObject.GetComponent<BaseCharacterView>();
            if (character != null)
            {
                character.OnBulletHit(this);
            }
            else
            {
                _barrierParticle.Play();
                _barrierParticle.transform.parent = null;
            }

            Destroy(gameObject);
        }

        protected void Update()
        {
            if (_Model == null) return;

            if (_Model.BulletFlight())
            {
                Destroy(gameObject);
            }
        }
    }
}
