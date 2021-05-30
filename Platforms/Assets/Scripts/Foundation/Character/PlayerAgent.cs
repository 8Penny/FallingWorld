using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Foundation
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerAgent : AbstractService<ICharacterAgent>, ICharacterAgent, IOnUpdate {
        private const float FALLING_SPEED = 3f; //TODO: to config
        [SerializeField]
        private Transform _rotationTransform;

        [InjectOptional] ICharacterHealth health = default;
        [Inject] ISceneState state = default;

        public Transform CharacterTransform;
        public bool UpdatePosition;
        public bool UpdateRotation;
        public float Speed => _speed;
        public Vector3 Position => CharacterTransform.position;

        private Rigidbody _rigidbody;
        private float _speed;

        void Awake()
        {
            _rigidbody =  GetComponent<Rigidbody>();
        }
        
        public void Move(Vector2 dir)
        {
            if (_rigidbody != null) {
                _rigidbody.MovePosition(transform.position + new Vector3(dir.x, 0.0f, dir.y));
                Look(dir);
            }
        }

        public void SetPosition(Vector3 position) {
            _rigidbody.MovePosition(position);
        }

        public void SetSpeed(float value) {
            _speed = value;
        }

        public void NavigateTo(Vector2 target)
        {
        }

        public void Look(Vector2 dir)
        {
            _rotationTransform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.y));
            transform.localRotation = Quaternion.identity;
        }

        public void Stop() {
            _speed = 0;
        }

        public void SetConstantVelocity() {
            _rigidbody.velocity = FALLING_SPEED * (_rigidbody.velocity.normalized);
        }

        public void FreezeXZPositions() {
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX |
                                     RigidbodyConstraints.FreezePositionZ |
                                     RigidbodyConstraints.FreezeRotation;
        }
        
        public void UnfreezePositions() {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(state.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (health != null && health.IsDead && _rigidbody != null) {
                Destroy(_rigidbody);
                _rigidbody = null;
                return;
            }

            if (UpdatePosition) {
                CharacterTransform.position = transform.position;
            }

            if (UpdateRotation) {
                CharacterTransform.rotation = _rotationTransform.rotation;
            }
        }
    }
}
