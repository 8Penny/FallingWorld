using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class ActivityButtonView : MonoBehaviour {
        [SerializeField]
        private ActivityButtonPresenter _presenter;
        
        [SerializeField]
        private GameObject _main;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Image _image;
        
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private Sprite _fixedSprite;
        [SerializeField]
        private Sprite _interactionSprite;

        public void UpdateParameters() {
            _main.SetActive(_presenter.IsVisible);
            if (!_presenter.IsVisible) {
                return;
            }

            if (_presenter.ButtonType == ActivityButtonType.Fixation) {
                _image.sprite = _fixedSprite;
            } else if (_presenter.ButtonType == ActivityButtonType.Interaction) {
                _image.sprite = _interactionSprite;
            }

            _button.interactable = _presenter.IsInteractable;
        }
    }
}