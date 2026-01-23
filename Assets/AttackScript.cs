using System.Collections;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    float speed;
    public void stutterFrame()
    {
        Debug.Log("stutterFrame");
        speed = anim.speed;
        anim.speed = -1;
        StartCoroutine(restartFrame());
    }

    IEnumerator restartFrame()
    {
        yield return new WaitForSeconds(1f);
        anim.speed = speed; 
    }
}
