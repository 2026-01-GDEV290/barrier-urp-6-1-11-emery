using UnityEngine;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField]
    CinemachineCamera cinemachineVirtualCamera;
    private float shakeTimer;

    [SerializeField]
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private void Awake()
    {
        CinemachineCamera cinemachineVirtualCamera = GetComponent<CinemachineCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
               cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0;
            }
        }

    }
}
