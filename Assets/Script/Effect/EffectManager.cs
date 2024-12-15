using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public enum Effect
    {
        SLASH,
        LIGHTBEAM,
        FIREBREATH1,
        FIREBREATH2,
        METEORITE
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
                break;
            case Effect.LIGHTBEAM:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/LightBeamEffect"));
                break;
            case Effect.FIREBREATH1:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/FireBreath1Effect"));
                break;
            case Effect.FIREBREATH2:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/FireBreath2Effect"));
                break;
            case Effect.METEORITE:
                effectObj = Instantiate(Resources.Load<Transform>("Effect/MeteoriteEffect"));
                break;
        }

        effectObj.position = pos;

        return effectObj;
    }
}
