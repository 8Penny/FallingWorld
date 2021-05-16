using UnityEngine;

namespace Foundation.Activities {
    public class AnimatorController : MonoBehaviour {
        [SerializeField]
        private Animator _animator;

        public void SetTrigger(int id) {
            _animator.SetTrigger(id);
        }
        public void SetFloat(int id, float value) {
            _animator.SetFloat(id, value);
        }
    }
}