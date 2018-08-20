using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Play Button")]
    public Button BtnPlay;
    [FMODUnity.EventRef]
    public string SFXPlayButton;
    public FMOD.Studio.EventInstance PlaySnd;

	
    [Header("Reset Button")]
    public Button BtnReset;
    [FMODUnity.EventRef]
    public string SFXResetButton;
    public FMOD.Studio.EventInstance ResetSnd;


    [Header("Music")]
    [FMODUnity.EventRef]
    public string MusicEvent;
    public FMOD.Studio.EventInstance SceneMusic;
	
    [Header("Music Stop")]
    public bool StopMusic;

    void Start()
    {
        BtnPlay.onClick.AddListener(PlaySound);
        BtnReset.onClick.AddListener(ResetSound);
        PlayMusic();
    }
	

    public void PlayMusic()
    {
        SceneMusic = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        SceneMusic.start();
    }
	
    public void PlaySound()
    {
        Debug.Log("Bouton Play");
        PlaySnd = FMODUnity.RuntimeManager.CreateInstance(SFXPlayButton);
        PlaySnd.start();

        StopMusicFade();
    }
	
    public void ResetSound()
    {
        Debug.Log("Bouton Reset");
        ResetSnd = FMODUnity.RuntimeManager.CreateInstance(SFXResetButton);
        ResetSnd.start();
    }
	
    void MusicStop()
    {
        if(StopMusic)
        {
            Debug.Log("Music Stop");
            StopMusicFade();
        }
    }
	
    public void StopMusicFade()
    {
        Debug.Log("Music Stop and slow fade");
        SceneMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
