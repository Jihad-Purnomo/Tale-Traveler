using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst;

    public AudioSource audioObject;
    public List<ObjectSound> soundToStop = new List<ObjectSound>();

    private void Awake()
    {
        if (Inst = null) Inst = this;
    }

    public void PlayAudio(ObjectSound sound, Transform spawn)
    {
        AudioSource audioSource = Instantiate(audioObject, spawn.position, Quaternion.identity);

        audioSource.clip = sound.audioClip;
        audioSource.volume = sound.volume;
        audioSource.loop = sound.loop;

        audioSource.Play();

        for (int i = 0; i < soundToStop.Count; i++)
        {
            if (soundToStop[i] == sound)
            {
                audioSource.Stop();
                soundToStop.Remove(soundToStop[i]);
            }
        }

        if (!audioSource.isPlaying)
        {
            Destroy(audioSource);
        }
    }

    public void StopAudio(ObjectSound sound)
    {
        soundToStop.Add(sound);
    }
}

[System.Serializable]
public class ObjectSound
{
    public AudioClip audioClip;
    [Range(0f, 1f)]public float volume;
    public bool loop;
}
