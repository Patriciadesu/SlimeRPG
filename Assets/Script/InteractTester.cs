using UnityEngine;

public class InteractTester : MonoBehaviour
{
    public QuestNPC currentNPC;
    public Rigidbody2D rb2D;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<QuestNPC>(out QuestNPC npc))
        {
            currentNPC = npc;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<NPC>(out NPC npc) && currentNPC == npc)
        {
            currentNPC.UnInteract();
            currentNPC = null;
        }
    }

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.Interact();
        }
    }

    //void FixedUpdate()
    //{
    //    Move();
    //}

    //public void Move(){
    //    float x = Input.GetAxisRaw("Horizontal");
    //    float y = Input.GetAxisRaw("Vertical");

    //    Vector2 movement = new Vector2(x , y);
    //    rb2D.linearVelocity = movement * 5;
    //}

    public void GiveQuest(int index)
    {
        currentNPC.GiveQuest(index);
    }
}
