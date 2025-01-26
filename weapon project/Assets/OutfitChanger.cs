using UnityEngine;
using System.Collections.Generic; 

public class OutfitChanger : MonoBehaviour
{
    [Header("Sprite Changer")]
    public SpriteRenderer bodyPart;

    [Header("Sprites to Cycle Through")]
    public List<Sprite> options = new List<Sprite>();

    private int currentOption = 0;

    public void NextOption()
    {
        currentOption++;
        if (currentOption >= options.Count)
        {
            currentOption = 0;
        }
        bodyPart.sprite = options[currentOption]; // Corrigido: 'sprite' com "s" minúsculo
    }

    public void PreviousOption()
    {
        currentOption--;
        if (currentOption < 0)
        {
            currentOption = options.Count - 1;
        }

        bodyPart.sprite = options[currentOption]; // Corrigido: 'sprite' com "s" minúsculo
    }
}
