using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContactGetter : MonoBehaviour
{
    [SerializeField]
    float shakeIntensity;
    [SerializeField]
    float shakeTime;

    int HP = 3;
    [SerializeField]
    GameObject brokenSelf;
    GameObject current;
    [SerializeField]
    MeshRenderer[] parts;

    [SerializeField]
    AudioSource sourceHit;
    [SerializeField]
    AudioSource sourceBreak;

    [SerializeField]
    ParticleSystem hitParticle;

    [SerializeField]
    float ParticleSize = 0.25f;
    [SerializeField]
    BoxCollider myCollider;

    [SerializeField]
    GameObject playerSword;

    [SerializeField]
    AttackScript playerAtk;

    [SerializeField]
    CinemachineShake cinemachineShake;

    [SerializeField]
    List<GameObject> Decals;

    [SerializeField]
    GameObject DecalProj;

    private void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");

        playerAtk = GameObject.FindGameObjectWithTag("PlayerMain").GetComponent<AttackScript>();

        cinemachineShake = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;
        sourceHit.pitch = Random.Range(1,1.5f);
        sourceHit.Play();

        AttackScript anim;

        if (!(playerSword.gameObject.GetComponent<AttackScript>() == null))
        {
            Debug.Log("got ATK scrpt");
            anim = playerSword.gameObject.GetComponent<AttackScript>();
            anim.stutterFrame();
        }

        GameObject newDecal = Instantiate(DecalProj,transform.position,Quaternion.identity);

        newDecal.transform.position = Physics.ClosestPoint(playerSword.transform.position, myCollider, transform.position, transform.rotation);

        Vector3 direction = transform.position - newDecal.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        newDecal.transform.rotation = rotation;

        Decals.Add(newDecal);

        if (HP <= 0)
        {
            current = Instantiate(brokenSelf, transform.position, Quaternion.identity);
            myCollider.enabled = false;
            foreach (MeshRenderer part in parts)
            {
                part.enabled = false;
                sourceBreak.pitch = Random.Range(1,1.5f);
                sourceBreak.Play();
            }
            foreach (GameObject decal in Decals)
            {
                Destroy(decal);
            }
            Invoke("Refresh", 3f);
        }

        hitParticle.transform.position = Physics.ClosestPoint(playerSword.transform.position, myCollider, transform.position, transform.rotation);

        hitParticle.Play();

        cinemachineShake.ShakeCamera(shakeIntensity, shakeTime);
    }

    void Refresh()
    {
        myCollider.enabled = true;
        Destroy(current); 
        foreach (MeshRenderer part in parts)
        {
            part.enabled = true;

        }

        HP = 3;
    }
}
