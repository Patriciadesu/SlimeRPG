using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField, Tooltip("ID from Database")] string[] skillsForSale;  //Go Check Skill ID in SKillManager or Server IDK
    [SerializeField, Tooltip("Container GameObject")] SkillContainer[] skillContainers; 
    //[SerializeField, Tooltip("Multiply price")] float itemPriceMultiplier; //Not implemented
    private void Start()
    {
        RandomShopItem();
    }
    /// <summary>
    /// Random Item in shop by creating List and random 4 number in it
    /// then use the number to Get ID from skillmanager and boom! it should appear in shop
    /// </summary>
    void RandomShopItem()
    {
        if (skillsForSale.Length < 4)
        {
            Debug.LogError("Not enough skills available for random selection!");
            return;
        }
        List<int> indices = new List<int>();
        while (indices.Count < 4)
        {
            int newIndex = UnityEngine.Random.Range(0, skillsForSale.Length);
            if (!indices.Contains(newIndex))
            {
                indices.Add(newIndex);
            }
        }

        skillContainers[0].ChangeItem(SkillManager.Instance.GetSkillByID(skillsForSale[indices[0]]));
        skillContainers[1].ChangeItem(SkillManager.Instance.GetSkillByID(skillsForSale[indices[1]]));
        skillContainers[2].ChangeItem(SkillManager.Instance.GetSkillByID(skillsForSale[indices[2]]));
        skillContainers[3].ChangeItem(SkillManager.Instance.GetSkillByID(skillsForSale[indices[3]]));

        foreach (var container in skillContainers)
        {
            container.GetSprite();
        }


    }

}
