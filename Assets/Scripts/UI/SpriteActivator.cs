using UnityEngine;
using UnityEngine.UI;

public class SpriteActivator : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Activate()
    {
        _image.enabled = !_image.enabled;
    }
}
