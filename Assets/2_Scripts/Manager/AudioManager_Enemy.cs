using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Enemy : AudioManager
{
    [SerializeField] AudioClip au_spin;
    [SerializeField] float vol_spin;
    bool stepBool;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    public override void PlaySound(string clip)
    {
        base.PlaySound(clip);
        if (clip == "spin") audiosource.PlayOneShot(au_spin, vol_spin);
    }
}
