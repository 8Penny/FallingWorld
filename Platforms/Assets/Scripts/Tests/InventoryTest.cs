using Foundation;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    [TestFixture]
    public class InventoryTest
    {
        private InventoryStorage<AbstractInventoryItem> _storage;
        [SetUp]
        public void Install()
        {
            _storage = new InventoryStorage<AbstractInventoryItem>();
            
        }
        
        [Test]
        public void CheckIfStorage()
        {
            var item = ScriptableObject.CreateInstance<TestInventoryItem>();
            item.UpdateParameters("magic", 5);
            _storage.Add(item, 3);
            
            Assert.AreEqual(2, _storage.CountOf(item));
        }

    }
}