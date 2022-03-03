using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomOrderAudioPlaylist : MonoBehaviour
{
    [SerializeField] private AudioClip[] randomClips;
    [SerializeField] private AudioSource audioSource = null;

    private List<AudioClip> availableClips = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        if (!audioSource) audioSource = GetComponent<AudioSource>();
        availableClips = new List<AudioClip>(randomClips);
        PlayNextRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextRandom();
        }
    }

    private void PlayNextRandom()
    {
        if(availableClips.Count > 0)
        {
            AudioClip randomClip = availableClips[Random.Range(0, availableClips.Count)];
            availableClips.Remove(randomClip);
            audioSource.clip = randomClip;
            audioSource.Play();
        }
        else
        {
            availableClips = new List<AudioClip>(randomClips);
            PlayNextRandom();
            return;
        }
    }
}
