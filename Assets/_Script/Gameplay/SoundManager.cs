using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] Sound_Attacks,Sound_Dying, Sound_Laughing, Sound_Click, Sound_UnitSelect, Sound_BuildingDestroyed;
    [SerializeField]private AudioClip Sound_SetTarget, Sound_NextWave, Sound_BuildTower;
    [SerializeField]private AudioClip[] Music_Songs;
    [SerializeField] private GameObject SoundObject;
    [SerializeField] private AudioSource MusicObject;
    private AudioSource _audioSource;
    private AudioSourcePool audioSourcePool;
    private int currentMusicIndex = 2;
    public float SoundVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public enum Sound
    {
        UnitAttack,
        UnitDying,
        UnitLaughing,
        UnitSelect,
        SetTarget,
        NextWave,
        UIClick,
        BuildingTower,
        BuildingDestroyed,
    }
    public static SoundManager Instance{get;private set;}
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        audioSourcePool = GetComponent<AudioSourcePool>();
        currentMusicIndex = Random.Range(0,4);
        PlayMusicNext();
    }
    ///// PLAY SOUND VOIDS //////////////
    public void PlaySound(Sound value,Vector3 position)
    {
       switch (value)
       {
        case (Sound.UnitAttack):
            audioSourcePool.PlayAtPoint(Sound_Attacks[Random.Range(0,Sound_Attacks.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.UnitDying):
            audioSourcePool.PlayAtPoint(Sound_Dying[Random.Range(0,Sound_Dying.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.UnitLaughing):
            audioSourcePool.PlayAtPoint(Sound_Laughing[Random.Range(0,Sound_Laughing.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;  
        case (Sound.UnitSelect):
            audioSourcePool.PlayAtPoint(Sound_UnitSelect[Random.Range(0,Sound_UnitSelect.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.BuildingDestroyed):
            audioSourcePool.PlayAtPoint(Sound_BuildingDestroyed[Random.Range(0,Sound_BuildingDestroyed.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.UIClick):
            audioSourcePool.PlayAtPoint(Sound_Click[Random.Range(0,Sound_Click.Length)], position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
            // SOLOS 
        case (Sound.NextWave):
            audioSourcePool.PlayAtPoint(Sound_NextWave, position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.SetTarget):
            audioSourcePool.PlayAtPoint(Sound_SetTarget, position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        case (Sound.BuildingTower):
            audioSourcePool.PlayAtPoint(Sound_BuildTower, position,SoundObject.GetComponent<AudioSource>(), SoundVolume);
            break;
        
        default:
            Debug.Log(" ERROR SOUND.");
            break;
       }
    }
    ////////////////////////////////////
    public void OnChangeMusicVolume(float value)
    {
        MusicVolume  = value;
        
    }
    public void OnChangeSoundVolume(float value)
    {
        SoundVolume = value;
    }
    public void PlayMusicNext()
    {
        MusicObject.PlayOneShot(Music_Songs[currentMusicIndex], MusicVolume);
        
        Invoke(nameof(EventOnEnd),Music_Songs[currentMusicIndex].length);
    }
     void EventOnEnd()
    {
        if(Application.isEditor) Debug.LogWarning("audio finished!");
        currentMusicIndex ++;
        if(currentMusicIndex >= 2)
            currentMusicIndex = 0;
        PlayMusicNext();
    }
    
}
