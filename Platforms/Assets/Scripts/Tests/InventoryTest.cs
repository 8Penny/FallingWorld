using Foundation;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class InventoryTest
    {
        private InventoryStorage<AbstractInventoryItem> _storage;
        private TestInventoryItem _item1;
        private TestInventoryItem _item2;
        
        [SetUp]
        public void Install()
        {
            _storage = new InventoryStorage<AbstractInventoryItem>();
            _item1 = ScriptableObject.CreateInstance<TestInventoryItem>();
            _item1.UpdateParameters("item1", 5);
            
            _item2 = ScriptableObject.CreateInstance<TestInventoryItem>();
            _item2.UpdateParameters("item2", 3);
            
        }
        
        [Test]
        public void AddItem1()
        {
            _storage.Add(_item1, 3);
            
            Assert.AreEqual(3, _storage.CountOf(_item1));

            _storage.Remove(_item1, 2);
            Assert.AreEqual(1, _storage.CountOf(_item1));
            
            _storage.Add(_item1, 3);
            Assert.AreEqual(4, _storage.CountOf(_item1));
            
            _storage.Add(_item1, 3);
            Assert.AreEqual(7, _storage.CountOf(_item1));

            foreach (var i in _storage.Items) {
                Debug.Log($"item {i.count} {i.item.Title.LocalizationID}");
            }
            
            _storage.Remove(_item1, 4);
            Assert.AreEqual(3, _storage.CountOf(_item1));
            
            foreach (var i in _storage.Items) {
                Debug.Log($"item {i.count} {i.item.Title.LocalizationID}");
            }
        }
        
        [Test]
        public void AddMultipleItems()
        {
            _storage.Add(_item1, 3);
            
            Assert.AreEqual(3, _storage.CountOf(_item1));

            _storage.Add(_item2, 2);
            Assert.AreEqual(2, _storage.CountOf(_item2));

            foreach (var i in _storage.Items) {
                Debug.Log($"item {i.count} {i.item.Title.LocalizationID}");
            }
        }

    }
}