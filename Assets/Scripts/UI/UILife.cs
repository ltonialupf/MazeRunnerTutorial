using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILife : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void SetImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}