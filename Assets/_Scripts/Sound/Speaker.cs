using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Speaker : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public AudioSource AudioSource { get { return audioSource; } }

}
