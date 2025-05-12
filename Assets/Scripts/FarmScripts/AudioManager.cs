using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip plantingSound;
    public AudioClip purchaseSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlantingSound()
    {
        audioSource.PlayOneShot(plantingSound);
    }
    public void PlayPurchaseSound()
    {
        audioSource.PlayOneShot(purchaseSound);
    }
    
}
