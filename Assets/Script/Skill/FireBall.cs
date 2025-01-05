using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "FireBall", menuName = "Skill/ActiveSkill/FireBall", order = 1)]
public class FireBall : ActiveSkill
{
    [Header("FireBall Settings")]
    public float damage = 20f;
    public float debuffDamage = 5f;
    public float debuffDuration = 3f;
    public float range = 5f;
    public float cooldown = 5f;

    [Header("FireBall Visual Effect")]
    public GameObject fireBallPrefab; // พรีแฟบของลูกบอลไฟ
    public float effectSpeed = 10f;   // ความเร็วของ effect

    [Header("Target Layer")]
    public LayerMask targetLayer;

    public override IEnumerator OnUse()
    {
        if (Player.Instance == null)
        {
            Debug.LogWarning("Player instance not found.");
            yield break;
        }

        Vector3 playerPosition = Player.Instance.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = playerPosition.z;

        Vector3 direction = (mousePosition - playerPosition).normalized;
        Vector3 targetPosition = playerPosition + direction * range;

        if (fireBallPrefab != null)
        {
            GameObject fireBallEffect = Instantiate(fireBallPrefab, playerPosition, Quaternion.identity);
            if (SkillManager.Instance != null)
            {
                SkillManager.Instance.StartSkillCoroutine(MoveEffectToTarget(fireBallEffect, targetPosition));
            }
            else
            {
                Debug.LogError("SkillManager.Instance is null");
            }
        }
        else
        {
            Debug.LogError("fireBallPrefab is null");
        }

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(targetPosition, 1f, targetLayer);
        Debug.Log($"Hit Targets: {hitTargets.Length}");

        foreach (Collider2D targetCollider in hitTargets)
        {
            if (targetCollider.TryGetComponent<Character>(out var target))
            {
                target.TakeDamage(damage);
                target.ApplyDebuff(new DamageOverTimeDebuff(debuffDamage, debuffDuration));
            }
        }

        yield return new WaitForSeconds(cooldown);
    }

    private IEnumerator MoveEffectToTarget(GameObject effect, Vector3 targetPosition)
    {
        while (Vector3.Distance(effect.transform.position, targetPosition) > 0.1f)
        {
            effect.transform.position = Vector3.MoveTowards(effect.transform.position, targetPosition, effectSpeed * Time.deltaTime);
            yield return null;
        }

        // ทำลาย effect เมื่อถึงเป้าหมาย
        Destroy(effect);
    }
}

public class DamageOverTimeDebuff
{
    private float damagePerSecond;
    private float duration;

    public DamageOverTimeDebuff(float damagePerSecond, float duration)
    {
        this.damagePerSecond = damagePerSecond;
        this.duration = duration;
    }

    public IEnumerator ApplyDebuff(Character target)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.TakeDamage(damagePerSecond * Time.deltaTime); // สร้างความเสียหายต่อเนื่อง
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
