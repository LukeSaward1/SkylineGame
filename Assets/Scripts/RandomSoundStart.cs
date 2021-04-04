using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundStart : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }

    private void Start()
    {
        float audiolength = musicSource.clip.length;
        musicSource.time = Random.Range(0, audiolength);
        StartCoroutine(StartFade(musicSource, 2f, musicSource.volume));
    }
}
