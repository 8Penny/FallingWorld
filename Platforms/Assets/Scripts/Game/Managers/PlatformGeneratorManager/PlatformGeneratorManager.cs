using System.Collections.Generic;
using Foundation;
using Game.Managers.PlatformManager;
using UnityEngine;

namespace Game.Managers.PlatformGeneratorManager {
    public class PlatformGeneratorManager : AbstractService<IPlatformGeneratorManager>, IPlatformGeneratorManager{
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private PlatformsSet _platformsSet;
        [SerializeField]
        private float _space;
        [SerializeField]
        private float _platformSize;
        [SerializeField]
        private int _rows;
        [SerializeField]
        private int _colums;
        [SerializeField]
        private List<Platform> _platforms;

        public List<Platform> GeneratePlatforms() {
            foreach (var platform in _platforms) {
                if (platform == null)
                {
                    continue;
                }
                Destroy(platform.gameObject);
            }
            _platforms.Clear();
            
            float startX = ((_colums * _platformSize + (_colums - 1) * _space) / -2.0f ) + _platformSize/2.0f;
            float startZ = ((_rows * _platformSize + (_rows - 1) * _space) / -2.0f) + _platformSize/2.0f;

            for (int i = 0; i < _rows; i++) {
                float currentX = startX;
                for (int j = 0; j < _colums; j++) {
                    Platform platformComponent = ChooseRandom();
                    platformComponent.transform.parent = _parent;
                    PlatformView platformView = platformComponent.GetComponent<PlatformView>();
                    
                    platformView.SetPresenter(platformComponent);
                    platformComponent.SetPosition(new Vector3(startZ, 0, currentX));
                    
                    _platforms.Add(platformComponent);
                    currentX += _space + _platformSize;
                }
                startZ += _space + _platformSize;
            }

            return _platforms;
        }

        private Platform ChooseRandom() {
            float chance = Random.Range(0, 1f);

            if (chance < 0.333f) {
                return Container.InstantiatePrefabForComponent<Platform>(_platformsSet.forestPlatform);
            }
            if (chance < 0.6666f)
            {
                return Container.InstantiatePrefabForComponent<Platform>(_platformsSet.sandPlatform);
            }

            return Container.InstantiatePrefabForComponent<Platform>(_platformsSet.waterPlatform);

        }
    }
}