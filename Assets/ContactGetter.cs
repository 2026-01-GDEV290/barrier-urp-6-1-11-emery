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
    List<GameObject> Decals;

    [SerializeField]
    GameObject DecalProj;

    private void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");

        playerAtk = GameObject.FindGameObjectWithTag("PlayerMain").GetComponent<AttackScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;

        if (playerAtk != null)
        {
            playerAtk.pitchSet(  1 + (Mathf.Abs(HP - 3) * .10f));
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
                sourceBreak.pitch = Random.Range(0.15f, 0.3f);
                sourceBreak.Play();
            }
            foreach (GameObject decal in Decals)
            {
                Destroy(decal);
            }
            playerAtk.stutterFrame(1.5f);
            Invoke("Refresh", 10f);
        }
        else
        {
            playerAtk.stutterFrame(1f);
        }

        hitParticle.transform.position = Physics.ClosestPoint(playerSword.transform.position, myCollider, transform.position, transform.rotation);

        hitParticle.Play();


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
