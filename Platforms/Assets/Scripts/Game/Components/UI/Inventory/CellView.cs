using Game.Components.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.UI.Inventory {
    public class CellView : FWView<CellPresenter> {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private GameObject _gameObject;

        protected override void OnAttached() {
            base.OnAttached();
            Bind(_presenter.OnCellUpdated, OnUpdated);
        }

        private void OnUpdated() {
            if (!_presenter.HasItem) {
                _gameObject.SetActive(false);
                return;
            }

            _gameObject.SetActive(true);
            if (_presenter.Count == 1) {
                _text.text = "";
            }
            else {
                _text.text = _presenter.Count.ToString();
            }

            _image.sprite = _presenter.Image;
        }
    }
}