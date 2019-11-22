using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaCollision : MonoBehaviour
{
    private ContactPoint[] c;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private GameObject bigSplash;
    [SerializeField]
    private GameObject waterSplash;
    [SerializeField]
    private ParticleSystem heart;
    [SerializeField]
    private ParticleSystem breakHeart;
    [SerializeField]
    private ParticleSystem no;



    public void PlayNoEffect()
    {
        if (!no.isPlaying)
            no.Play();
    }

    private float touchSpeed = 4.5f;

    private Speaker speaker;
    private Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speaker = GetComponentInChildren<Speaker>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        c = collision.contacts;

        if (collision.gameObject.tag == "Water")
        {
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
            SoundManager.Instance.PlayOneShot3DSe(ESeTable.WaterJump, speaker);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            Grab grab = collision.gameObject.GetComponent<Grab>();
            if (grab != null)
            {
                if (grab.GetVelocity().magnitude < touchSpeed && !heart.isPlaying)
                {
                    heart.Play();
                    animator.SetTrigger("Spoil");
                    SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_4, speaker);
                }
                else if (grab.GetVelocity().magnitude >= touchSpeed && !breakHeart.isPlaying)
                    breakHeart.Play();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Dolly"))
            ;
        else
            Instantiate(hitPrefab, c[0].point, Quaternion.identity);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Stage"))
            return;

        c = collision.contacts;

        //Debug.Log(c[0].normal + " " + c[0].separation);
        transform.position = transform.position + c[0].normal * -c[0].separation;

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hand"))
        {
            animator.SetTrigger("Idle");
            heart.Stop();
            breakHeart.Stop();
        }
        else if (collision.gameObject.tag == "Water")
        {
            SoundManager.Instance.PlayOneShot3DSe(ESeTable.WaterDown, speaker);
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
            Instantiate(waterSplash, transform);
        }
    }
}
