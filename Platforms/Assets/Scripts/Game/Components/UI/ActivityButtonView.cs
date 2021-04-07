using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class ActivityButtonView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _main;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Image _image;
    }
}