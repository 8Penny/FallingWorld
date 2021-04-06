using System.Collections.Generic;

namespace Game.Managers.PlatformManager
{
    public interface IPlatformManager
    {
        void ResetAvailablePlatforms();
        void GeneratePlatforms();
        List<Platform> GetFallingPlatforms();
        void BecomeAvailable(Platform platform);
        void BecomeNotAvailable(Platform platform);
    }
}