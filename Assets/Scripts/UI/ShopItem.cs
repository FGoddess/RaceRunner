using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField]
    public Image Image { get; set; }

    [field: SerializeField]
    public Button Button { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI ButtonText { get; set; }
}
