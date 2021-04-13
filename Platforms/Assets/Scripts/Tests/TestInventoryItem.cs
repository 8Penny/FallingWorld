using Foundation;

namespace Tests
{
    public class TestInventoryItem : AbstractInventoryItem
    {
        public void  UpdateParameters(string name, int maxcount)
        {
            Title = new LocalizedString(name);
            MaxCountInStack = maxcount;
        }
    }
}