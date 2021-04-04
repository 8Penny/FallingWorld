using System.Collections.Generic;
using Foundation;
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
        private List<Transform> _platforms;

        public void GeneratePlatforms() {
            foreach (var platform in _platforms) {
                Destroy(platform.gameObject);
            }
            _platforms.Clear();
            
            float startX = ((_colums * _platformSize + (_colums - 1) * _space) / -2.0f ) + _platformSize/2.0f;
            float startZ = ((_rows * _platformSize + (_rows - 1) * _space) / -2.0f) + _platformSize/2.0f;

            for (int i = 0; i < _rows; i++) {
                float currentX = startX;
                for (int j = 0; j < _colums; j++) {
                    GameObject newPlatform = ChooseRandom();
                    newPlatform.transform.localPosition = new Vector3(startZ, 0, currentX);
                    currentX += _space + _platformSize;
                    _platforms.Add(newPlatform.transform);
                }
                startZ += _space + _platformSize;
            }
        }

        private GameObject ChooseRandom() {
            float chance = Random.Range(0, 1f);

            if (chance < 0.333f) {
                return  Instantiate(_platformsSet.forestPlatform, _parent);
            }
            if (chance < 0.6666f) {
                return  Instantiate(_platformsSet.sandPlatform, _parent);
            }
            return  Instantiate(_platformsSet.waterPlatform, _parent);

        }
    }
}