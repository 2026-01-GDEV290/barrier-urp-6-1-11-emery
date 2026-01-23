using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ContactGetter : MonoBehaviour
{
    int HP = 6;
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

    private void Start()
    {
        playerSword = GameObject.FindGameObjectWithTag("PlayerSword");
    }

    private void OnTriggerEnter(Collider other)
    {
        HP--;
        sourceHit.pitch = Random.Range(1,1.5f);
        sourceHit.Play();

        hitParticle.transform.position = Physics.ClosestPoint(playerSword.transform.position, myCollider, transform.position, transform.rotation);

        hitParticle.Play();

        AttackScript anim;

        if (!(playerSword.gameObject.GetComponent<AttackScript>() == null))
        {
            Debug.Log("got ATK scrpt");
            anim = playerSword.gameObject.GetComponent<AttackScript>();
            anim.stutterFrame();
        }

       

        if (HP <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            current = Instantiate(brokenSelf, transform.position, Quaternion.identity);
            myCollider.enabled = false;
            foreach (MeshRenderer part in parts)
            {
                part.enabled = false;
                sourceBreak.pitch = Random.Range(1,1.5f);
                sourceBreak.Play();
            }
            Invoke("Refresh", 3f);
        }
    }

    void Refresh()
    {
        myCollider.enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        Destroy(current); 
        foreach (MeshRenderer part in parts)
        {
            part.enabled = true;

        }
        HP = 6;
    }
}
