using System.Collections.Generic;

namespace Foundation
{
    public sealed class InventoryStorage<T> : IInventoryStorage<T>
        where T : AbstractInventoryItem
    {
        private const int MAX_COUNT = 15;

        private int _storageSize;

        private readonly T[] _items = new T[MAX_COUNT];
        private readonly int[] _counts = new int[MAX_COUNT];
        private readonly Dictionary<T, List<int>> _itemCellIndices = new Dictionary<T, List<int>>();
        private readonly List<int> _updatedCells = new List<int>();

        public T this[int index] => _items[index];

        public int StorageSize => _storageSize;
        
        public ObserverList<IOnInventoryChanged> OnChanged { get; } =
            new ObserverList<IOnInventoryChanged>();
        public ObserverList<IOnInventoryCellUpdated> OnCellsUpdated { get; } =
            new ObserverList<IOnInventoryCellUpdated>();

        public InventoryStorage(int storageSize = 15)
        {
            _storageSize = storageSize;
        }

        public IEnumerable<(T item, int count)> Items
        {
            get
            {
                for (int i = 0; i < _storageSize; i++)
                {
                    if (_items[i] == null || _counts[i] < 1)
                    {
                        continue;
                    }

                    yield return (_items[i], _counts[i]);
                }
            }
        }

        public IEnumerable<(AbstractInventoryItem item, int count)> RawItems
        {
            get
            {
                for (int i = 0; i < _storageSize; i++)
                {
                    if (_items[i] == null || _counts[i] < 1)
                    {
                        continue;
                    }

                    yield return (_items[i], _counts[i]);
                }
            }
        }
        
        public int CountInCell(int index)
        {
            return index >= _storageSize ? 0 : _counts[index];
        }
        
        public int CountOf(T item)
        {
            if (!_itemCellIndices.TryGetValue(item, out var indices))
            {
                return 0;
            }
            var itemCount = 0;
            for (int i = 0; i < indices.Count; i++)
            {
                int index = indices[i];
                if (index < _storageSize)
                {
                    itemCount += _counts[indices[i]];  
                }
            }
            return itemCount;
        }

        int IInventoryStorage.CountOf(AbstractInventoryItem item)
        {
            if (item is T castItem)
            {
                return CountOf(castItem);
            }

            return 0;
        }

        public void Add(T item, int amount, out int amountRemainder)
        {
            amountRemainder = 0;
            if (amount <= 0)
            {
                DebugOnly.Error($"Attempted to add {amount} of '{item.Title}' into the inventory.");
                return;
            }

            _updatedCells.Clear();

            amountRemainder = amount;
            _itemCellIndices.TryGetValue(item, out var indices);
            if (indices != null)
            {
                for (int i = 0; i < indices.Count; i++)
                {
                    var index = indices[i];
                    if (index >= _storageSize)
                    {
                        continue;
                    }

                    _updatedCells.Add(index);
                    amountRemainder = AddExtraItemsToCell(index, amountRemainder);
                    if (amountRemainder <= 0)
                    {
                        CallCellsUpdated(_updatedCells);
                        return;
                    }
                }
            }
            

            for (int i = 0; i < _storageSize; i++)
            {
                if (_items[i] != null)
                {
                    continue;
                }

                _updatedCells.Add(i);
                _items[i] = item;
                AddIndexToItemCellIndices(item, i);
                amountRemainder = AddExtraItemsToCell(i, amountRemainder);
                if (amountRemainder <= 0)
                {
                    break;
                }
            }

            CallCellsUpdated(_updatedCells);
        }

        private void CallCellsUpdated(List<int> cells)
        {
            foreach (var it in OnCellsUpdated.Enumerate())
            {
                it.Do(cells);
            }

            if (cells == null)
            {
                return;
            }

            foreach (var it in OnChanged.Enumerate())
            {
                it.Do();
            }
        }

        private int AddExtraItemsToCell(int cellIndex, int count)
        {
            var item = _items[cellIndex];
            var freeSpace = item.MaxCountInStack - _counts[cellIndex];
            if (freeSpace <= 0)
            {
                return 0;
            }

            count -= freeSpace;
            if (count <= 0)
            {
                _counts[cellIndex] = item.MaxCountInStack + count;
                return 0;
            }

            _counts[cellIndex] = item.MaxCountInStack;
            return count;
        }

        private void AddIndexToItemCellIndices(T item, int index)
        {
            if (_itemCellIndices.TryGetValue(item, out var indices))
            {
                indices.Add(index);
                return;
            }

            _itemCellIndices[item] = new List<int>() {index};
        }

        public void Add(T item, int amount)
        {
            Add(item, amount, out var remainder);
        }

        public bool Remove(T item, int amount)
        {
            if (amount <= 0)
            {
                DebugOnly.Error($"Attempted to remove {amount} of '{item.Title}' from the inventory.");
                return false;
            }

            if (CountOf(item) < amount)
            {
                return false;
            }

            _updatedCells.Clear();

            var indices = _itemCellIndices[item];
            for (int i = 0; i < indices.Count; i++)
            {
                var currentIndex = indices[i];
                _updatedCells.Add(currentIndex);
                amount -= _counts[currentIndex];
                if (amount > 0)
                {
                    _counts[currentIndex] = 0;
                    _items[currentIndex] = null;
                    RemoveFromItemCellIndices(item, currentIndex);
                }
                else
                {
                    _counts[currentIndex] = -amount;
                    break;
                }
            }

            CallCellsUpdated(_updatedCells);
            return true;
        }

        private void RemoveFromItemCellIndices(T item, int index)
        {
            _itemCellIndices[item].Remove(index);
        }

        void IInventoryStorage.Add(AbstractInventoryItem item, int amount, out int remainder)
        {
            Add((T) item, amount, out remainder);
        }

        void IInventoryStorage.Add(AbstractInventoryItem item, int amount)
        {
            Add((T) item, amount, out var remainder);
        }

        bool IInventoryStorage.Remove(AbstractInventoryItem item, int amount)
        {
            if (item is T castItem)
            {
                return Remove(castItem, amount);
            }

            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < MAX_COUNT; i++)
            {
                _items[i] = null;
                _counts[i] = 0;
            }

            _itemCellIndices.Clear();

            CallCellsUpdated(default);
        }
    }
}