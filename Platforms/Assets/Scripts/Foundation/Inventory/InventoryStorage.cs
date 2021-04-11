using UnityEngine;
using System;
using System.Collections.Generic;

namespace Foundation {
    public sealed class InventoryStorage<T> : IInventoryStorage<T>
        where T : AbstractInventoryItem {
        sealed class Comparer : IComparer<T> {
            public static readonly Comparer Instance = new Comparer();

            public int Compare(T a, T b) {
                if (a.LessThan(b)) {
                    return -1;
                }

                if (b.LessThan(a)) {
                    return 1;
                }

                int iidA = a.GetInstanceID();
                int iidB = b.GetInstanceID();
                if (iidA < iidB) {
                    return -1;
                }

                if (iidA > iidB) {
                    return 1;
                }

                return 0;
            }
        }

        private const int MAX_COUNT = 15;
        private readonly T[] _items = new T[MAX_COUNT];
        private readonly int[] _counts = new int[MAX_COUNT];

        public ObserverList<IOnInventoryChanged> OnChanged { get; } = new ObserverList<IOnInventoryChanged>();
        public ObserverList<IOnInventoryClear> OnCleared { get; } = new ObserverList<IOnInventoryClear>();

        public IEnumerable<(T item, int count)> Items {
            get {
                for (int i = 0; i < MAX_COUNT; i++) {
                    if (_items[i] == null || _counts[i] < 1) {
                        continue;
                    }

                    yield return (_items[i], _counts[i]);
                }
            }
        }

        public IEnumerable<(AbstractInventoryItem item, int count)> RawItems {
            get {
                for (int i = 0; i < MAX_COUNT; i++) {
                    if (_items[i] == null || _counts[i] < 1) {
                        continue;
                    }

                    yield return (_items[i], _counts[i]);
                }
            }
        }

        public int CountOf(T item) {
            var 
            for (int i = 0; i < MAX_COUNT; i++){
                if (_items[i])
            }
            items.TryGetValue(item, out int count);
            return count;
        }

        int IInventoryStorage.CountOf(AbstractInventoryItem item) {
            if (item is T castItem) {
                return CountOf(castItem);
            }

            return 0;
        }

        public void Add(T item, int amount) {
            if (amount <= 0) {
                DebugOnly.Error($"Attempted to add {amount} of '{item.Title}' into the inventory.");
                return;
            }

            items.TryGetValue(item, out int count);
            items[item] = count + amount;


            foreach (var it in OnChanged.Enumerate()) {
                it.Do(item);
            }
        }

        void IInventoryStorage.Add(AbstractInventoryItem item, int amount) {
            Add((T) item, amount);
        }

        public bool Remove(T item, int amount) {
            if (amount <= 0) {
                DebugOnly.Error($"Attempted to remove {amount} of '{item.Title}' from the inventory.");
                return false;
            }

            if (!items.TryGetValue(item, out int count) || count < amount) {
                return false;
            }

            count -= amount;
            if (count > 0) {
                items[item] = count;
            }
            else {
                if (!items.Remove(item)) {
                    return false;
                }
            }

            foreach (var it in OnChanged.Enumerate()) {
                it.Do(item);
            }

            return true;
        }

        bool IInventoryStorage.Remove(AbstractInventoryItem item, int amount) {
            if (item is T castItem) {
                return Remove(castItem, amount);
            }

            return false;
        }

        public void Clear() {
            items.Clear();

            foreach (var it in OnCleared.Enumerate()) {
                it.Do();
            }
        }
    }
}