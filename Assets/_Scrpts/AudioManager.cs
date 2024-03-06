using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip EndSound;
    public AudioClip ShootSound;
    public AudioClip PlayerInjuredSound;
    public AudioClip EnemyDieSound;
    public AudioClip SpatteSound;
    public AudioClip GuideDrop;
    public AudioClip Backgroundmusic;
    public AudioClip EndBgm;
    public AudioClip EndBgm_fail;



    private void Awake()
    {
        instance = this;
    }

}
