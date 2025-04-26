using UnityEngine;
using UnityEngine.UI;

public class BuildingView : ConstructionViewBase
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Slider healthSlider;

    public override void SetHealth(int current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)current / max;
        }
    }


    private void OnMouseDown()
    {
        RaiseClickEvent();
    }

}
