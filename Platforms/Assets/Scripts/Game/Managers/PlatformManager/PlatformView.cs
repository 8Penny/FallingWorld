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
        [SerializeField]
        private MeshRenderer _meshRenderer;

        private Platform _presenter;

        public void SetPresenter(Platform presenter)
        {
            _presenter = presenter;
            OnPositionUpdated();
            OnStatusUpdated();
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

        public void AddMaterialAlpha(float value) {
            Material currentMaterial = _meshRenderer.material;
            float alphaValueClamp = Mathf.Clamp(currentMaterial.color.a + value,0, 1f);
            float diff = alphaValueClamp - currentMaterial.color.a;
            if (Mathf.Abs(diff) < Mathf.Epsilon) {
                return;
            }
            var newColor = currentMaterial.color + new Color(0, 0, 0, diff);
            currentMaterial.color = newColor;
            _meshRenderer.material = currentMaterial;
        }
    }
}