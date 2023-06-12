using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audiosource;
    [SerializeField] protected AudioClip au_hit;
    [SerializeField] protected float vol_hit;
    [SerializeField] protected AudioClip au_damage;
    [SerializeField] protected float vol_damage;

    protected virtual void  Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public virtual void PlaySound(string clip)
    {
        if (clip == "hit") audiosource.PlayOneShot(au_hit, vol_hit);
        if (clip == "damage") audiosource.PlayOneShot(au_damage, vol_damage);
    }
}
