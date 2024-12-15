using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public enum Effect
    {
        SLASH,
        LIGHTBEAM
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
        Transform effectObj = null;

        switch (effect)
        {
            case Effect.SLASH:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/SlashEffect"));
                effectObj.position = pos;
                break;
            case Effect.LIGHTBEAM:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/LightBeamEffect"));
                effectObj.position = pos;
                break;
        }

        return effectObj;
    }
}
