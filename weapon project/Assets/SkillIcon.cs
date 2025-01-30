using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Item item;
    public Image icon;
    public Slider slider;

    public void Setup(Item item)
    {
        if (item)
        {
            this.item = item;
            icon.sprite = item.icon;

            if (item is ActiveItem it)
            {
                if (it.maxCooldown > 0)
                    slider.maxValue = it.maxCooldown;
            }
            else if (item is PassiveItem pt)
            {
                if (pt.maxCooldown > 0)
                    slider.maxValue = pt.maxCooldown;
            }
        } else
        {
            this.item = null;
            icon.sprite = null;
            slider.maxValue = 1;
            slider.value = 0;
        }
    }

    private void Update()
    {
        if (item)
        {
            if (item is ActiveItem it)
            {
                if (it.cooldown > 0)
                    slider.value = it.cooldown;
            }
            else if (item is PassiveItem pt)
            {
                if (pt.cooldown > 0)
                    slider.value = pt.cooldown;
            }
        }
    }

}
