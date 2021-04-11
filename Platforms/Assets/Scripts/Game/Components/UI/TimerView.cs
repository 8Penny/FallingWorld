using Foundation;
using Game.Managers;
using Game.Managers.PhaseManagers;
using TMPro;
using UnityEngine;
using Zenject;
using Game.Components.Core;

namespace Game.Components.UI {
    public class TimerView : FWView<TimerPresenter> {
        [SerializeField]
        private GameObject _mainGameObject;

        [SerializeField]
        private TMP_Text _text;

        protected override void OnAttached()
        {
            base.OnAttached();
            Bind(_presenter.OnTimerUpdated, OnTimerUpdated);
            Bind(_presenter.OnRetentionStarted, OnPhaseStarted);
            Bind(_presenter.OnRetentionEnded, OnPhaseEnded);
            
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