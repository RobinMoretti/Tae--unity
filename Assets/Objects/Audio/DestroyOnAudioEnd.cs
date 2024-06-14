using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioEnd : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
            Destroy(this);
            return;
        }

        // Check if Play on Awake is enabled
        if (audioSource.playOnAwake)
        {
            // Subscribe to the finished event
            StartCoroutine(CheckIfAudioFinished());
        }
        else
        {
            Debug.LogWarning("AudioSource does not have Play on Awake enabled.");
        }
    }

    private IEnumerator CheckIfAudioFinished()
    {
        // Wait until the audio clip finishes playing
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
