using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.UI.Inventory {
    public class UIInventoryView : MonoBehaviour{
        [SerializeField]
        private List<CellView> _cellViews;

        public List<CellView> CellViews => _cellViews;
    }
}