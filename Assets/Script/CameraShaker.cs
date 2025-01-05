using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    [SerializeField] private CinemachineFollow cinemachineFollow;
    private Vector3 defaultOffset;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;

        defaultOffset = cinemachineFollow.FollowOffset;
    }         

    public void TriggerShake(float shakeDuration, float shakeTime, float shakeMagnitude) =>
        StartCoroutine(TriggerShakeIE(shakeDuration, shakeTime, shakeMagnitude));

    private IEnumerator TriggerShakeIE(float shakeDuration, float shakeTime, float shakeMagnitude)
    {
        float start = 0;

        Vector3 firstPos = defaultOffset;

        while (true)
        {
            float startShake = 0;
            Vector3 direction = Random.onUnitSphere * shakeMagnitude;
            Vector3 startPos = cinemachineFollow.FollowOffset;
            direction.z = startPos.z;

            while (true)
            {
                yield return new WaitForEndOfFrame();
                startShake += Time.deltaTime;

                cinemachineFollow.FollowOffset =
                    Vector3.Lerp(startPos, direction, startShake / shakeDuration);

                if (startShake > shakeDuration) break;
            }

            start += startShake;

            if (start > shakeTime) break;
        }

        float endShake = 0;
        Vector3 endPos = cinemachineFollow.FollowOffset;

        while (true)
        {
            yield return new WaitForEndOfFrame();
            endShake += Time.deltaTime;

            cinemachineFollow.FollowOffset =
                Vector3.Lerp(endPos, firstPos, endShake / shakeDuration);

            if (endShake > shakeDuration) break;
        }
    }
}
