using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class SkillContainer : MonoBehaviour , ICollectables
{
    public Skill skill;
    public TMP_Text PriceText;
    public void Collect()
    {
        Player p1 = GameObject.FindWithTag("Player").GetComponent<Player>();
        skill.Have = true;
        Destroy(this.gameObject);
        //if ( p1.coin >= skill.Price)  //doesn't work
        //{
        //    p1.coin -= skill.Price;
        //}
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                Collect();
            }
        }
    }

    void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
    public void GetSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = skill.SkillSprite;
        PriceText.text = skill.Price.ToString();

    }
    public void ChangeItem(Skill NewSkill)
    {
        skill = NewSkill;
        GetSprite();
    }
}
