using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public enum Effect
    {
        SLASH
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public Transform CreateEffect(Effect effect, Vector2 pos, Quaternion rotation, Transform parent)
    {
        var effectObj = CreateEffect(effect, pos, rotation);
        effectObj.SetParent(parent);
        return effectObj;
    }

    public Transform CreateEffect(Effect effect, Vector2 pos, Quaternion rotation)
    {
        var effectObj = CreateEffect(effect, pos);
        effectObj.rotation = rotation;
        return effectObj;
    }

    public Transform CreateEffect(Effect effect, Vector2 pos)
    {
        switch (effect)
        {
            case Effect.SLASH:
                var effectObj = Instantiate(Resources.Load<Transform>("Effect/SlashEffect"));
                effectObj.position = pos;
                return effectObj;
        }

        return null;
    }
}
