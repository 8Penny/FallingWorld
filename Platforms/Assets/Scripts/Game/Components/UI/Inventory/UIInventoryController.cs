using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace Game.Components.UI.Inventory {
    public class UIInventoryController : AbstractBehaviour, IOnInventoryChanged, IOnPlayerAdded {
        [SerializeField]
        private UIInventoryView _inventoryView;
        [SerializeField]
        private InventoryItem _defaultItem;

        private IPlayerManager _playerManager;
        private IInventoryStorage<InventoryItem> _inventory;

        private List<CellPresenter> _presenters;
        private int _firstFreeIndex;
        private Dictionary<AbstractInventoryItem, List<CellPresenter>> _cells = new Dictionary<AbstractInventoryItem, List<CellPresenter>>();

        [Inject]
        public void Init(IPlayerManager playerManager, IInventory inventory) {
            _playerManager = playerManager;

            _presenters = new List<CellPresenter>();
            for (int i = 0; i < _inventoryView.CellViews.Count; i++) {
                var cellView = _inventoryView.CellViews[i];
                var presenter = new CellPresenter();
                cellView.SetPresenter(presenter);
                _presenters.Add(presenter);
            }
        }
        
        protected override void OnEnable() {
            base.OnEnable();
            Observe(_playerManager.OnPlayerAdded);
            
        }

        void IOnInventoryChanged.Do() {
            
        }

        void IOnPlayerAdded.Do(int playerIndex) {
            var player = _playerManager.GetPlayer(0);
            _inventory = player.Inventory as IInventoryStorage<InventoryItem>;
            Observe(_inventory.OnChanged);

            foreach (var item in _inventory.Items) {
                if (_firstFreeIndex < _presenters.Count) {
                    _presenters[_firstFreeIndex++].SetItem(item.item, item.count);
                }
            }
        }
    }
}