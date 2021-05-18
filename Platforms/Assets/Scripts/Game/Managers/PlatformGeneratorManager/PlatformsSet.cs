using UnityEngine;

namespace Game.Managers.PlatformGeneratorManager {
    [CreateAssetMenu(fileName = "PlatformsSet", menuName = "PLATFORMS/ScriptableObjects/PlatformsSet")]
    public class PlatformsSet : ScriptableObject {
        public GameObject waterPlatform;
        public GameObject sandPlatform;
        public GameObject forestPlatform;
    }
}