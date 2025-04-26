using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonView : ButtonViewBase
{
    [SerializeField] private Sprite icon;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private Image image;

    //public override void SetIcon(Sprite sprite) => icon = sprite;
    public override void SetIcon(Sprite sprite)
    {
        icon = sprite;
        image.sprite = sprite;
    }

    public override void SetName(string text) => nameText.text = text;
}
