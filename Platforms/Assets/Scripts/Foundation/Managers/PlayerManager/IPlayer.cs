using Foundation.Activities;
using UnityEngine;

namespace Foundation
{
    public interface IPlayer
    {
        int Index { get; }
        ICharacterHealth Health { get; }
        ICharacterAgent Agent { get; }
        ICharacterActivityQueue ActivityQueue { get; }
        IInventory Inventory { get; } 
        Sprite Portrait { get; }
        Vector3 Position { get; }
    }
}
