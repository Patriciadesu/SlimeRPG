using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] Vector2[] itemSellingLocations;
    [SerializeField] Skill[] skillsForSale;
    [SerializeField] SkillContainer[] skillContainers;
    [SerializeField] float itemPriceMultiplier;
    /// <summary>
    /// idk what i am doing but whatever.
    /// </summary>
    void RandomShopItem()
    {
        int indexA = 0;
        int indexB = 0;
        int indexC = 0;
        int indexD = 0;
        while (indexA == indexB || indexA == indexC || indexA == indexD || indexB == indexC || indexB == indexD || indexD == indexC)
        {
        indexA = UnityEngine.Random.Range(0, skillsForSale.Length - 1);
        indexB = UnityEngine.Random.Range(0, skillsForSale.Length - 1);
        indexC = UnityEngine.Random.Range(0, skillsForSale.Length - 1);
        indexD = UnityEngine.Random.Range(0, skillsForSale.Length - 1);
        }
        skillContainers[0] = GetComponent<SkillContainer>();
        skillContainers[1] = GetComponent<SkillContainer>();
        skillContainers[2] = GetComponent<SkillContainer>();
        skillContainers[3] = GetComponent<SkillContainer>();
        
    }

}
