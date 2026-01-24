using System.Collections;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    float Timer = 0.25f;
    public void stutterFrame()
    {
        Time.timeScale = 0;
        StartCoroutine(refrsh());
    }

    IEnumerator refrsh()
    {
        yield return new WaitForSecondsRealtime(Timer);
        Time.timeScale = 1;
    }
}
