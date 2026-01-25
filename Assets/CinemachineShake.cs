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

    public void ShakeCamera(float intensity)
    {
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
    }

    public void StopShake()
    {
        cinemachineBasicMultiChannelPerlin.AmplitudeGain = 0;
    }
}
