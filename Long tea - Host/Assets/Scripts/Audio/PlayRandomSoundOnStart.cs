using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSoundOnStart : MonoBehaviour
{
    [SerializeField] private List<AudioClip> randomClips = new List<AudioClip>();
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(randomClips[Random.Range(0, randomClips.Count)]);
    }
}
