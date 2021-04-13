using System;
using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;
using IInstantiator = Game.Managers.Instantiator.IInstantiator;

namespace Game.Components.UI.Inventory {
    public class UIInventoryController : AbstractBehaviour,  IOnPlayerAdded, IOnInventoryCellUpdated {
        [SerializeField]
        private UIInventoryView _inventoryView;

        private IPlayerManager _playerManager;
        private IInventoryStorage<InventoryItem> _inventory;
        private IInstantiator _instantiator;

        private List<CellPresenter> _presenters;

        [Inject]
        public void Init(IPlayerManager playerManager, IInstantiator instantiator) {
            _playerManager = playerManager;
            _instantiator = instantiator;
        }

        private void Awake() {
            _presenters = new List<CellPresenter>();
            for (int i = 0; i < _inventoryView.CellViews.Count; i++) {
                var cellView = _inventoryView.CellViews[i];
                var presenter = _instantiator.Instantiate<CellPresenter>();
                cellView.SetPresenter(presenter);
                _presenters.Add(presenter);
            }
        }

        protected override void OnEnable() {
            base.OnEnable();
            Observe(_playerManager.OnPlayerAdded);
        }

        void IOnPlayerAdded.Do(int playerIndex) {
            var player = _playerManager.GetPlayer(0);
            _inventory = player.Inventory.RawStorage as IInventoryStorage<InventoryItem>;
            Observe(_inventory.OnCellsUpdated);
            

            for (int i = 0; i < _inventory.StorageCellsCount; i++) {
                _presenters[i].SetItem(_inventory[i], _inventory.CountInCell(i));
            }
        }

        void IOnInventoryCellUpdated.Do(List<int> cellIndices) {
            for (int i = 0; i < cellIndices.Count; i++) {
                var index = cellIndices[i];
                _presenters[i].SetItem(_inventory[index], _inventory.CountInCell(index));
            }
        }
    }
}