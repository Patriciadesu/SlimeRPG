using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Heal", menuName = "Skill/ActiveSkill/Heal", order = 1)]
public class Heal : ActiveSkill
{
    public float healAmount = 50f;

    public override IEnumerator OnUse()
    {
        Debug.Log($"Using Heal Skill. Healing for {healAmount} HP");

        if (Player.Instance != null)
        {
            Player.Instance.Heal(healAmount); // เรียกใช้ Heal
            Debug.Log($"Player's health after healing: {Player.Instance.health}/{Player.Instance.MaxHealth}");
        }
        else
        {
            Debug.LogWarning("Player instance is null. Cannot heal.");
        }

        yield break;
    }

}
