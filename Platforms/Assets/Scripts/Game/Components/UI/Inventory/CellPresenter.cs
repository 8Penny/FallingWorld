using Foundation;
using Game.Components.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Components.UI.Inventory {
    public class CellPresenter : FWPresenter {
        private InventoryItem _item;
        private int _itemCount;
        
        public FWEvent OnCellUpdated;
        
        public bool HasItem => _item != null || _itemCount > 0;
        public Sprite Image => _item.Icon;
        public int Count => _itemCount;
        
        protected  override void OnInit() {
            OnCellUpdated = CreateEvent();
        }
        
        public void SetItem(InventoryItem item, int count) {
            _item = item;
            _itemCount = count;
            OnCellUpdated.Invoke();
        }

        public void SetItemCount(int count) {
            _itemCount = count;
            OnCellUpdated.Invoke();
        }
    }
}