using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioManagerHub : MonoBehaviour

{
    [Header("Back To Menu Button")]
    public Button BackMenuBtn;
    [FMODUnity.EventRef]
    public string SFXBackButton;
    public FMOD.Studio.EventInstance BackBtnSnd;

    [Header("SFX Uncover Map")]
    [FMODUnity.EventRef]
    public string SFXMapUncover;
    public FMOD.Studio.EventInstance MapSnd;

    [Header("Music")]
    [FMODUnity.EventRef]
    public string MusicEvent;
    public FMOD.Studio.EventInstance SceneMusic;
    
    [Header("Transition to Menu")]
    public AudioManagerMenu AudioMenu;
       
    void Start()
    {
        BackMenuBtn.onClick.AddListener(BackBtnSound);
    }

    /// ----- BACK TO MENU BUTTON -----///
    public void BackBtnSound()
    {
        BackBtnSnd = FMODUnity.RuntimeManager.CreateInstance(SFXBackButton);
        BackBtnSnd.start();

        StopMusicFade(); //stop hub music
        AudioMenu.PlayMusic(); //start menu music
    }
   
    /// ----- MAP UNCOVER SFX  -----///
    public void MapUncover()
    {
        //To script with the hub music system in conjunction with the "Hub.cs" script
        MapSnd = FMODUnity.RuntimeManager.CreateInstance(SFXMapUncover);
        MapSnd.start();
    }

    /// ----- MUSIC START  -----///
    public void PlayMusic()
    {
        SceneMusic = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        SceneMusic.start();
    }
    /// ----- MUSIC STOP -----///
    void OnDestroy() //crude alternative while the custom button isnt working
    {
        StopMusicFade();
    }

    public void StopMusicFade()
    {
        SceneMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
