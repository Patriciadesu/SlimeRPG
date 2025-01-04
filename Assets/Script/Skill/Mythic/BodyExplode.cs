using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BodyExplode", menuName = "Skill/ActiveSkill/BodyExplode", order = 1)]
public class BodyExplode : ActiveSkill
{
    [Header("Skill Settings")]
    public float baseDamage = 10f; // ����������¾�鹰ҹ
    public float baseRadius = 2f; // ����վ�鹰ҹ
    public float damagePerLevel = 5f; // ����������·��������鹵�������
    public float radiusPerLevel = 1f; // ����շ��������鹵�������

    [Header("Target Layer")]
    public LayerMask damageableLayer; // �������ͧ������·���Ѻ�������������

    public override IEnumerator OnUse()
    {
        if (Player.Instance == null)
        {
            Debug.LogWarning("Player instance not found.");
            yield break;
        }

        // �Ѻ Level �ͧ Player
        int level = Player.Instance.Level;

        // �ӹǳ Damage ��� Radius ��� Level
        float damage = baseDamage + (level - 1) * damagePerLevel;
        float radius = baseRadius + (level - 1) * radiusPerLevel;

        // ���ҧ���Դ
        Explode(Player.Instance.transform.position, damage, radius);

        yield break;
    }

    private void Explode(Vector3 position, float damage, float radius)
    {
        // �Ϳ࿡�������Դ
        Debug.Log($"Explosion at {position} with radius {radius} and damage {damage}");

        // �����������������
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, damageableLayer);

        foreach (var hitCollider in hitColliders)
        {
            // ��Ǩ�ͺ������������ Character
            if (hitCollider.TryGetComponent<Character>(out var target))
            {
                target.TakeDamage(damage);
                Debug.Log($"Damaged {target.name} for {damage} HP.");
            }
        }
    }
}
