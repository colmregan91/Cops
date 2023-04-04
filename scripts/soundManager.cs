using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum deathSounds
{
    water,
    die
}

public enum soundEffects
{
    gunClick,
    coin,
    knife,
    finishGame, 
    shoot
}
public enum gameSounds
{
    gameover,
    checkpoint,


}
public class soundManager : MonoBehaviour
{
    public static soundManager Instance;

    public AudioSource SfxSource;
    public AudioSource jumpSource;
    public AudioSource gameSource;
    public AudioSource dieSource;
    public AudioSource backgroundSource;
    [Space]
    public AudioClip gameover;
    public AudioClip cp;
    public AudioClip knife;
    public AudioClip coin;
    public AudioClip gunClick;
    public AudioClip splash;
    public AudioClip die;
    public AudioClip winMusic;
    public AudioClip shoot;

    private const float STARTING_VOLUME = 0.3f; // constant start volume

    private float curSFXvolume = STARTING_VOLUME; 
    private float curBackgroundVolume = STARTING_VOLUME;
    private float curJumpVolume = STARTING_VOLUME;

    void Awake()
    {
        if (Instance != null) // set up singleton
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SfxSource.volume = STARTING_VOLUME; // reset values 
        gameSource.volume = STARTING_VOLUME;
        dieSource.volume = STARTING_VOLUME;
    }


    public void playSFXsound(soundEffects sound) // play a sound effect 
    {
       switch (sound)
        {
            case soundEffects.gunClick:
                PlaygunPickUpClip();
                break;
            case soundEffects.coin:
                PlayCoinClip();
                break;
            case soundEffects.knife:
                PlayKnifeClip();
                break;
            case soundEffects.finishGame:
                PlayEndGameMusic();
                break;
            case soundEffects.shoot:
                PlayShootClip();
                break;

        }
    }

    public void PlayDeathClip(deathSounds soundType)
    {
        switch (soundType)
        {
            case deathSounds.water:
                PlaySplashClip();
                break;
            case deathSounds.die:
                PlayDieClip();
                break;
        }
    }

    public void playGamesound(gameSounds sound)
    {
        switch (sound)
        {
            case gameSounds.checkpoint:
                PlaycpClip();
                break;
            case gameSounds.gameover:
                PlayGOClip();
                break;

        }
    }
    public void ChangeJumpVolume(float value)
    {
        jumpSource.volume = value;// changed from the options menu
        curJumpVolume = value;// changed from the options menu
    }
    public void ChangeBackgroundVolume (float value)
    {
        backgroundSource.volume = value;// changed from the options menu
        curBackgroundVolume = value;// changed from the options menu
    }

    public void ChangeSFXVolume(float value)
    {
        SfxSource.volume = value; // changed from the options menu
        gameSource.volume = value; // changed from the options menu
        dieSource.volume = value; // changed from the options menu
        curSFXvolume = value; // changed from the options menu
    }

    public void PlayJumpClip()
    {
        jumpSource.Play();
    }
    private void PlayCoinClip()
    {
        SfxSource.clip = coin;
        SfxSource.Play();
    }
    private void PlayEndGameMusic()
    {
        backgroundSource.Stop(); // stop playing background music
        SfxSource.clip = winMusic;
        SfxSource.Play();
    }
    private void PlayKnifeClip()
    {
        SfxSource.clip = knife;
        SfxSource.Play();
    }
    private void PlayShootClip()
    {
        SfxSource.clip = shoot;
        SfxSource.Play();
    }

    private void PlaygunPickUpClip()
    {
        SfxSource.clip = gunClick;
        SfxSource.Play();
    }
    private void PlayGOClip()
    {
        gameSource.clip = gameover;
        gameSource.Play();
    }
    private void PlaycpClip()
    {
        gameSource.clip = cp;
        gameSource.Play();
    }


    private void PlaySplashClip()
    {
        dieSource.clip = splash;
        dieSource.Play();
    }
    private void PlayDieClip()
    {
        dieSource.clip = die;
        dieSource.Play();
    }


    public float getSFXvolume() // used when saving sound settings
    {
        return curSFXvolume;
    }
    public float getBackgroundVolume()// used when saving sound settings
    {
        return curBackgroundVolume;
    }
    public float getJumpVolume()// used when saving sound settings
    {
        return curJumpVolume;
    }
}
