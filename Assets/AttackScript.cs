using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    float Timer = 0.25f;
    [SerializeField]
    GameObject backGroundCam;
    [SerializeField]
    GameObject forwardGroundCam;
    [SerializeField]
    CinemachineShake cinemachineShake;
    [SerializeField]
    AudioSource AudioHit;
    [SerializeField]
    AudioSource AudioStab;

    [SerializeField]
    Volume globaleVolume;
    [SerializeField]
    VolumeProfile Impact;
    [SerializeField]
    VolumeProfile Comic;

    private void Start()
    {
        cinemachineShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineShake>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            globaleVolume.profile = Impact;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            globaleVolume.profile = Comic;
        }
    }
    public void pitchSet(float pitch)
    {
        AudioHit.pitch = pitch;
        AudioStab.pitch = pitch;
    }

    public void stutterFrame(float mult)
    {
        Time.timeScale = .15f;
        backGroundCam.SetActive(true);
        forwardGroundCam.SetActive(true);
        StartCoroutine(refrsh(mult));
        cinemachineShake.ShakeCamera(1.5f);
        AudioHit.Play();
        AudioStab.Play();
    }


    IEnumerator refrsh(float mult)
    {
        yield return new WaitForSecondsRealtime((Timer * mult));
        backGroundCam.SetActive(false);
        forwardGroundCam.SetActive(false);
        cinemachineShake.StopShake();
        Time.timeScale = 1;
    }
}
