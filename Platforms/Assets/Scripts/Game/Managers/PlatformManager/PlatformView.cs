using Game.Managers.PhaseManagers;
using UnityEngine;

namespace Game.Managers.PlatformManager
{
    public class PlatformView : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;
        [SerializeField]
        private GameObject _fixedParts;
        [SerializeField]
        private GameObject _availableParts;

        private Platform _presenter;

        public void SetPresenter(Platform presenter)
        {
            _presenter = presenter;
        }
        
        public void OnPositionUpdated()
        {
            _transform.position = _presenter.Position;
        }
        
        public void OnStatusUpdated()
        {
            switch (_presenter.Status)
            {
                case PlatformStatus.Fixed:
                    _fixedParts.SetActive(true);
                    _availableParts.SetActive(false);
                    break;
                case PlatformStatus.Selectable:
                    _availableParts.SetActive(true);
                    _fixedParts.SetActive(false);
                    break;
                default:
                    _availableParts.SetActive(false);
                    _fixedParts.SetActive(false);
                    break;
            }
        }
    }
}