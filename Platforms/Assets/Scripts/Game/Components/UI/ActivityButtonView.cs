using Foundation;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class ActivityButtonView : ButtonView<ActivityButtonPresenter> {
        [SerializeField]
        private GameObject _main;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Sprite _fixedSprite;
        [SerializeField]
        private Sprite _interactionSprite;

        protected override void OnAttached() {
            base.OnAttached();
            _presenter.Bind(_presenter.OnUpdated, OnChanged);
            OnChanged();
        }

        private void OnChanged() {
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