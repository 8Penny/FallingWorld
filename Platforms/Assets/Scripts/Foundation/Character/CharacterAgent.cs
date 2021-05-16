using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Foundation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class CharacterAgent : AbstractService<ICharacterAgent>, ICharacterAgent, IOnUpdate
    {
        public NavMeshAgent agent;

        [SerializeField]
        private Transform _rotationTransform;

        [InjectOptional] ICharacterHealth health = default;
        [Inject] ISceneState state = default;

        public Transform CharacterTransform;
        public bool UpdatePosition;
        public bool UpdateRotation;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public float Speed => 1;

        public void Move(Vector2 dir)
        {
            if (agent != null) {
                agent.Move(new Vector3(dir.x, 0.0f, dir.y));
                Look(dir);
            }
        }

        public void SetSpeed(float value) {
            throw new System.NotImplementedException();
        }

        public void NavigateTo(Vector2 target)
        {
            if (agent != null) {
                agent.destination = new Vector3(target.x, transform.position.y, target.y);
                agent.isStopped = false;
            }
        }

        public void Look(Vector2 dir)
        {
            _rotationTransform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0.0f, dir.y));
            transform.localRotation = Quaternion.identity;
        }

        public void Stop()
        {
            if (agent != null) {
                agent.isStopped = true;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Observe(state.OnUpdate);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (health != null && health.IsDead && agent != null) {
                Destroy(agent);
                agent = null;
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
