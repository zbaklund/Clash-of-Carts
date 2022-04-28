using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public RectTransform bar;
    private float max_hp;
    private float curr_hp;

    public void SetMaxHealth(float max_hp)
    {
        this.max_hp = max_hp;
        curr_hp = max_hp;
        bar.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetCurrHealth(float curr_hp)
    {
        this.curr_hp = curr_hp;
        bar.localScale = new Vector3(this.curr_hp / this.max_hp, 1f, 1f);
    }

    void Update() {
        // MAKE THE HEALTH BAR ALWAYS FACE THE PLAYER
    }
}
