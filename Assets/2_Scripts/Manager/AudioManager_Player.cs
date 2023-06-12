using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Player : AudioManager
{
    [SerializeField] AudioClip au_stepL;
    [SerializeField] AudioClip au_stepR;
    [SerializeField] float vol_step;
    bool stepBool;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void PlaySound(string clip)
    {
        base.PlaySound(clip);
        if(clip == "step") AudioStep();
    }
    
    void AudioHit()
    {
        audiosource.PlayOneShot(au_hit, vol_hit);
    }

    void AudioDamage()
    {
        audiosource.PlayOneShot(au_damage, vol_damage);
    }

    void AudioStep()
    {
        if (stepBool)
        {
            audiosource.PlayOneShot(au_stepL, vol_step);
        }
        else
        {
            audiosource.PlayOneShot(au_stepR, vol_step);
        }
        stepBool = !stepBool;
    }
}
