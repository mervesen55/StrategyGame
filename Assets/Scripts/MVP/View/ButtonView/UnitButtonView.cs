using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonView : ButtonViewBase
{

    public bool isInitilazed;

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;

    

    public override void SetIcon(Sprite sprite)
    {
        iconImage.sprite = sprite;
    }

    public override void SetName(string text)
    {
        nameText.text = text;
    }
}
