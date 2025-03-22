using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    
    public List<AudioSource> BGSound = new List<AudioSource>();
    public List<AudioSource> OneShot_Sound = new List<AudioSource>();        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayerBGMusic(SoundList soundList)
    {
        if(soundList==SoundList.BGMainMenu)
        {
            BGSound[0].Play();
            BGSound[1].Stop();
        }
        else if (soundList == SoundList.BGInGame)
        {
            BGSound[1].Play();
            BGSound[0].Stop();
        }
    }
    public void PlayOneShot(SoundList soundList)
    {
        if(soundList == SoundList.BtnClick)
        {
            OneShot_Sound[0].Play();
        }
        else if(soundList == SoundList.Shot)
        {
            OneShot_Sound[1].Play();
        }
        else if (soundList == SoundList.Dead)
        {
            OneShot_Sound[2].Play();
        }
        else if (soundList == SoundList.Win)
        {
            OneShot_Sound[3].Play();
        }
    }
}
public enum SoundList
{
    BGMainMenu,
    BGInGame,
    BtnClick,
    Shot,
    Dead,
    Win
}