using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class SkillContainer : MonoBehaviour , ICollectables
{
    public Skill skill;
    public TMP_Text PriceText;
    public void Collect()
    { GameObject player = GameObject.FindWithTag("Player");
        Player p1 = player.GetComponent<Player>();
        if ( p1.coin >= skill.Price)  //doesn't work
        {
            p1.coin -= skill.Price;
            skill.Have = true;
            Destroy(this.gameObject); 
        }
         //Player.Instance.AddSkill(skill);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
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
