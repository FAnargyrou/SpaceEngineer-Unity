using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Play();
                return;
            }
        }
        Debug.LogWarning($"Sound with name {name} was not found!!");
    }

    public void Stop(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == name)
            {
                sound.source.Stop();
                return;
            }
        }
        Debug.LogWarning($"Sound with name {name} was not found!!");
    }
}
