using System.Collections.Generic;
using Game.Managers.PlatformManager;

namespace Game.Managers.PlatformGeneratorManager {
    public interface IPlatformGeneratorManager {
        List<Platform> GeneratePlatforms();
    }
}