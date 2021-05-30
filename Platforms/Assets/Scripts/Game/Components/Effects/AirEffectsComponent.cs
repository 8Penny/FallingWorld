using System;
using Foundation;
using Game.Managers.PhaseManagers;
using UnityEngine;
using Zenject;

namespace Game.Components.Effects {
    public class AirEffectsComponent : AbstractBehaviour, IOnUpdate, IOnPhaseStarted, IOnPhaseCompleted {
        [SerializeField]
        private ParticleSystem _particles;
        [SerializeField]
        private Transform _transform;
        [Inject]
        public IFallingPhaseManager FallingPhaseManager { get; set; }
        [Inject]
        public IPlayerManager PlayerManager { get; set; }
        [Inject]
        public ISceneState SceneState { get; set; }

        private Vector3 _shift = new Vector3(0, 10f, 0);
        private IPlayer _player;

        protected override void OnEnable() {
            Observe(FallingPhaseManager.OnPhaseStarted);
            Observe(FallingPhaseManager.OnPhaseCompleted);
            Observe(SceneState.OnUpdate);
        }
        void IOnPhaseStarted.Do() {
            _player = PlayerManager.GetPlayer(PlayerStatic.Id);
            _particles.Play();
        }
        
        void IOnUpdate.Do(float timeDelta) {
            if (!FallingPhaseManager.IsActive) {
                return;
            }
            _transform.position = _player.Position - _shift;
        }

        void IOnPhaseCompleted.Do() {
            _particles.Stop();
        }
    }
}