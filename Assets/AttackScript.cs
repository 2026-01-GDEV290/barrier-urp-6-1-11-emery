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

    private void Start()
    {
        cinemachineShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineShake>();
    }
    public void stutterFrame(float mult)
    {
        Time.timeScale = .15f;
        backGroundCam.SetActive(true);
        forwardGroundCam.SetActive(true);
        StartCoroutine(refrsh(mult));
        cinemachineShake.ShakeCamera(1.5f, Timer * mult);
    }

    IEnumerator refrsh(float mult)
    {
        yield return new WaitForSecondsRealtime((Timer * mult));
        backGroundCam.SetActive(false);
        forwardGroundCam.SetActive(false);
        Time.timeScale = 1;
    }
}
