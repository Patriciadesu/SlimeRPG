using UnityEngine;
[CreateAssetMenu(fileName = "SpeedItem")]
public class SpeedItem : UsableItem
{
    public float speedBonus;

    public override void ApplyEffect()
    {
        Player.Instance.Speed += speedBonus;
    }

    public override void RemoveEffect()
    {
        Player.Instance.Speed -= speedBonus;
    }

}
