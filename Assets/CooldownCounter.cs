using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownCounter : MonoBehaviour
{
    public Slider slider;

    public float Cooldown;

    public string Skill;

    void Update()
    {
        if (Cooldown >= 0)
        {
            Cooldown -= Time.deltaTime;
            slider.value = Cooldown;
        }
        //else if (Shooting.isAbleToUseEmpoweredShot == false)
        //{
        //    Shooting.isAbleToUseEmpoweredShot = true;
        //}
        if(Cooldown < 0 && Skill == "CrossedShot" && Attacking.instance.CanUseCrossedSpell == false)
        {
            Attacking.instance.CanUseCrossedSpell = true;
        }
        if (Cooldown < 0 && Skill == "Bolt" && Attacking.instance.CanUseBoltSpell == false)
        {
            Attacking.instance.CanUseBoltSpell = true;
        }
    }

    public void PutOnCoolDown(float cooldown)
    {
        Cooldown = cooldown;
        slider.maxValue = Cooldown;
    }
}
