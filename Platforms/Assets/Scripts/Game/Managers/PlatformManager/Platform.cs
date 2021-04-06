using System;
using Foundation;
using UnityEngine;
using Zenject;

namespace Game.Managers.PlatformManager
{
    [RequireComponent(typeof(BoxCollider))]
    public class Platform : AbstractBehaviour, IOnUpdate
    {
        [SerializeField]
        private PlatformView _view;
        
        [SerializeField]
        private AnimationCurve _fallingSpeedCurve;
        
        private IPlatformManager _platformManager;
        private ISceneState _sceneState;
        private PlatformStatus _status;
        private Vector3 _position;
        private float _fallingSpeed = 1f;
        private float _timeFromFallingStart;
        private bool _isFalling;

        public Vector3 Position => _position;
        public PlatformStatus Status => _status;
        public float FallingSpeed => _fallingSpeed;

        [Inject]
        public void Init(IPlatformManager platformManager, ISceneState sceneState)
        {
            _platformManager = platformManager;
            Observe(sceneState.OnUpdate);
        }
        
        public void SetStatus(PlatformStatus status)
        {
            _status = status;
            _view.OnStatusUpdated();
            Debug.Log("STATUS UPDATED");
        }
        
        public void SetPosition(Vector3 position)
        {
            _position = position;
            _view.OnPositionUpdated();
        }
        
        public void SetFallingSpeed(float fallingSpeed)
        {
            _fallingSpeed = fallingSpeed;
        }

        public void StartFalling()
        {
            _isFalling = true;
        }

        public void Reset()
        {
            _isFalling = false;
            _status = PlatformStatus.Default;
            _view.OnStatusUpdated();
        }

        public void OnTriggerEnter(Collider other)
        { 
            _platformManager.BecomeAvailable(this);
        }

        public void OnTriggerExit(Collider other)
        {
            _platformManager.BecomeNotAvailable(this);
        }

        void IOnUpdate.Do(float timeDelta)
        {
            if (_isFalling)
            {
                _timeFromFallingStart += timeDelta;
                _position += Vector3.down * (_fallingSpeed * _fallingSpeedCurve.Evaluate(_timeFromFallingStart / 2f));
                _view.OnPositionUpdated();
            }
        }
    }
}