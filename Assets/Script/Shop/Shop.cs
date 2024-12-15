using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject[] itemSellingGameObject;
    [SerializeField] Skill[] skillsForSale;
    [SerializeField] SkillContainer[] skillContainers;
    [SerializeField] float itemPriceMultiplier;
    private void Start()
    {
        RandomShopItem();
    }
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

        skillContainers[0].ChangeItem(skillsForSale[indices[0]]);
        skillContainers[1].ChangeItem(skillsForSale[indices[1]]);
        skillContainers[2].ChangeItem(skillsForSale[indices[2]]);
        skillContainers[3].ChangeItem(skillsForSale[indices[3]]);

        foreach (var container in skillContainers)
        {
            container.GetSprite();
        }


    }

}
