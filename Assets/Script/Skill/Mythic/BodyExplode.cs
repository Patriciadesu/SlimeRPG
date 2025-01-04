using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BodyExplode", menuName = "Skill/ActiveSkill/BodyExplode", order = 1)]
public class BodyExplode : ActiveSkill
{
    [Header("Skill Settings")]
    public float baseDamage = 10f; // ความเสียหายพื้นฐาน
    public float baseRadius = 2f; // รัศมีพื้นฐาน
    public float damagePerLevel = 5f; // ความเสียหายที่เพิ่มขึ้นต่อเลเวล
    public float radiusPerLevel = 1f; // รัศมีที่เพิ่มขึ้นต่อเลเวล

    [Header("Target Layer")]
    public LayerMask damageableLayer; // เลเยอร์ของเป้าหมายที่รับความเสียหายได้

    public override IEnumerator OnUse()
    {
        if (Player.Instance == null)
        {
            Debug.LogWarning("Player instance not found.");
            yield break;
        }

        // รับ Level ของ Player
        int level = Player.Instance.Level;

        // คำนวณ Damage และ Radius ตาม Level
        float damage = baseDamage + (level - 1) * damagePerLevel;
        float radius = baseRadius + (level - 1) * radiusPerLevel;

        // สร้างระเบิด
        Explode(Player.Instance.transform.position, damage, radius);

        yield break;
    }

    private void Explode(Vector3 position, float damage, float radius)
    {
        // เอฟเฟกต์การระเบิด
        Debug.Log($"Explosion at {position} with radius {radius} and damage {damage}");

        // ค้นหาเป้าหมายในรัศมี
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, damageableLayer);

        foreach (var hitCollider in hitColliders)
        {
            // ตรวจสอบว่าเป้าหมายเป็น Character
            if (hitCollider.TryGetComponent<Character>(out var target))
            {
                target.TakeDamage(damage);
                Debug.Log($"Damaged {target.name} for {damage} HP.");
            }
        }
    }
}
