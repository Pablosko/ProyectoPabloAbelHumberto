using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundScript : MonoBehaviour
{
    public static SoundScript instance;
    [SerializeField]
    public List<AudioClip> AmbientalMusicClips = new List<AudioClip>();
    [SerializeField]
    public List<AudioClip> AmbientalEffectsClips = new List<AudioClip>();
    [SerializeField]
    public AudioClip LevelUpPlayerEffectClip;
    [SerializeField]
    public AudioClip HitButtonEffectClip;
    [SerializeField]
    public List<AudioClip> RollEffectsClips = new List<AudioClip>();
    [SerializeField]
    public AudioClip BuyHeroEffectClip;
    [SerializeField]
    public AudioClip DropHeroEffectClip;
    [SerializeField]
    public AudioClip SellHeroEffectClip;
    [SerializeField]
    [Header("AudioMixer")]
    public AudioMixer audioMixer;

    public AudioSource music;
    public AudioSource effect;
    public AudioSource ui;

    void Start()
    {
        instance = this;
        AmbientalMusicClips.AddRange(Resources.LoadAll<AudioClip>("Audios/Ambiental/Music"));
        AmbientalEffectsClips.AddRange(Resources.LoadAll<AudioClip>("Audios/Ambiental/Effects"));
        LevelUpPlayerEffectClip = Resources.Load<AudioClip>("Audios/Effects/Game/LevelUpPlayer");
        RollEffectsClips.AddRange(Resources.LoadAll<AudioClip>("Audios/Effects/Game/Roll"));
        HitButtonEffectClip = Resources.Load<AudioClip>("Audios/Effects/UI/HitButton");
        SellHeroEffectClip = Resources.Load<AudioClip>("Audios/Effects/Game/SellHero");
        BuyHeroEffectClip = Resources.Load<AudioClip>("Audios/Effects/Game/BuyHero");
        DropHeroEffectClip = Resources.Load<AudioClip>("Audios/Effects/Game/DropHero");
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("VolumeMaster", volume);
    }
    public void SetVolumeEffects(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", volume);
    }
    public void SetVolumeAmbiental(float volume)
    {
        audioMixer.SetFloat("VolumeAmbiental", volume);
    }
    public void SetVolumeEffectsUI(float volume)
    {
        audioMixer.SetFloat("VolumeEffectsUI", volume);
    }
    public void SetVolumeEffectsInGame(float volume)
    {
        audioMixer.SetFloat("VolumeEffectsInGame", volume);
    }
    public void SetVolumeAmbientalEffects(float volume)
    {
        audioMixer.SetFloat("VolumeAmbientalEffects", volume);
    }
    public void SetVolumeAmbientalMusic(float volume)
    {
        audioMixer.SetFloat("VolumeAmbientalMusic", volume);
    }
    public void PlayMusic(bool state)
    {
        music.clip = AmbientalMusicClips[Random.Range(0, AmbientalEffectsClips.Count)];
        music.Play();
        if (!state)
            music.Stop();
    }
    public void PlayRandomEffects()
    {
        music.clip = AmbientalEffectsClips[Random.Range(0, AmbientalEffectsClips.Count)];
        music.Play();
    }
}
