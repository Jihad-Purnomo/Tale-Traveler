using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    public AudioSource audioObject;
    public Dictionary<ObjectSound, AudioSource> PlayingSound = new();

    private void Awake()
    {
        if (Inst == null) Inst = this;
    }

    public void PlayAudio(ObjectSound sound, Transform spawn)
    {
        if (!PlayingSound.ContainsKey(sound))
        {
            AudioSource audioSource = Instantiate(audioObject, spawn.position, Quaternion.identity);

            audioSource.clip = sound.audioClip;
            audioSource.volume = sound.volume;
            audioSource.loop = sound.loop;

            PlayingSound.Add(sound, audioSource);

            audioSource.Play();

            if (!audioSource.loop)
            {
                Destroy(audioSource.gameObject, audioSource.clip.length);
                PlayingSound.Remove(sound);
            }
        }
    }

    public void StopAudio(ObjectSound sound)
    {
        if (PlayingSound.ContainsKey(sound))
        {
            Destroy(PlayingSound[sound].gameObject);
            PlayingSound.Remove(sound);
        }
    }
}

[System.Serializable]
public class ObjectSound
{
    public AudioClip audioClip;
    [Range(0f, 1f)]public float volume;
    public bool loop;
}
