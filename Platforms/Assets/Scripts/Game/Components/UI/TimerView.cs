using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Components.UI {
    public class TimerView : UIView<TimerPresenter> {
        [SerializeField]
        private GameObject _mainGameObject;

        [SerializeField]
        private TMP_Text _text;

        protected override void OnAttached()
        {
            base.OnAttached();
            _presenter.Bind(_presenter.OnTimerUpdated, OnTimerUpdated);
            _presenter.Bind(_presenter.OnRetentionStarted, OnPhaseStarted);
            _presenter.Bind(_presenter.OnRetentionEnded, OnPhaseEnded);
            
            _mainGameObject.SetActive(_presenter.IsVisible);
            DebugOnly.Message("TIMER VIEW ATTACHED");
        }

        void OnTimerUpdated(){
            _text.text = _presenter.TimeLeft.ToString();
        }

        void OnPhaseEnded() {
            _mainGameObject.SetActive(false);
        }

        void OnPhaseStarted() {
            _mainGameObject.SetActive(true);
        }
    }
}