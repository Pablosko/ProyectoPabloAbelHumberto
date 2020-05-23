using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    [SerializeField]
    AudioClip AmbientalMusicMenuClip;
    [SerializeField]
    AudioSource AmbientalMusicMenu;
    [SerializeField]
    AudioClip AmbientalMusicInGameClip;
    [SerializeField]
    AudioSource AmbientalMusicInGame;
    [SerializeField]
    AudioClip AmbientalEffectInGameClip;
    [SerializeField]
    AudioSource AmbientalEffectInGame;
    [SerializeField]
    AudioClip LevelUpPlayerEffectClip;
    [SerializeField]
    AudioSource LevelUpPlayerEffect;
    [SerializeField]
    AudioClip HitButtonEffectClip;
    [SerializeField]
    AudioSource HitButtonEffect;
    [SerializeField]
    AudioClip RollEffectClip;
    [SerializeField]
    AudioSource RollEffect;
    [SerializeField]
    AudioClip BuyHeroEffectClip;
    [SerializeField]
    AudioSource BuyHeroEffect;
    [SerializeField]
    AudioClip SellHeroEffectClip;
    [SerializeField]
    AudioSource SellHeroEffect;
    [SerializeField]
    AudioClip TakeHeroEffectClip;
    [SerializeField]
    AudioSource TakeHeroEffect;
    [SerializeField]
    AudioClip DropHeroEffectClip;
    [SerializeField]
    AudioSource DropHeroEffect;
    [SerializeField]
    AudioClip LevelUpHeroEffectClip;
    [SerializeField]
    AudioSource LevelUpHeroEffect;
    [SerializeField]
    AudioClip TierUpHeroEffectClip;
    [SerializeField]
    AudioSource TierUpHeroEffect;
    [SerializeField]
    AudioClip HitEffectClip;
    [SerializeField]
    AudioSource HitEffect;
    [SerializeField]
    AudioClip DeadEffectClip;
    [SerializeField]
    AudioSource DeadEffect;
    [SerializeField]
    AudioClip HabilityEffectClip;
    [SerializeField]
    AudioSource HabilityEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetAudio(AmbientalMusicMenuClip, AmbientalMusicMenu, true);
        SetAudio(AmbientalMusicInGameClip, AmbientalMusicInGame, true);
        SetAudio(AmbientalEffectInGameClip, AmbientalEffectInGame, true);
        SetAudio(LevelUpPlayerEffectClip, LevelUpPlayerEffect, false);
        SetAudio(HitButtonEffectClip, HitButtonEffect, false);
        SetAudio(BuyHeroEffectClip, BuyHeroEffect, false);
        SetAudio(SellHeroEffectClip, SellHeroEffect, false);
        SetAudio(TakeHeroEffectClip, TakeHeroEffect, false);
        SetAudio(DropHeroEffectClip, DropHeroEffect, false);
        SetAudio(LevelUpHeroEffectClip, LevelUpHeroEffect, false);
        SetAudio(TierUpHeroEffectClip, TierUpHeroEffect, false);
        SetAudio(HitEffectClip, HitEffect, false);
        SetAudio(DeadEffectClip, DeadEffect, false);
        SetAudio(HabilityEffectClip, HabilityEffect, false);
        PlayAudio(AmbientalMusicMenu);
        PlayAudio(AmbientalMusicInGame);
        PlayAudio(AmbientalEffectInGame);
    }
    void SetAudio(AudioClip aC, AudioSource aS, bool isLoop)
    {
        aS.clip = aC;
        if (isLoop)
        {
            aS.loop = true;
        }
    }
    public void PlayAudio(AudioSource aS)
    {
        aS.Play();
    }
}
