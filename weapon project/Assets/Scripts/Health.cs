using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHearts;
    public Sprite emptyHearts;

    void Update(){
            if(health > numOfHearts){
                health = numOfHearts;
            }
    // Lembrar otimizar o código e meter a funcionar, por algum motivo it doesnt work how i expected
        for (int i = 0; i < hearts.Length; i++) {
            if(i < health){
                hearts[i].sprite = fullHearts;
            } else {
                hearts[i].sprite = emptyHearts;
            }
            if(i < numOfHearts){
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

}
