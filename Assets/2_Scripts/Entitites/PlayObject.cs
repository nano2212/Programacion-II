using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayObject : MonoBehaviour
{
    public bool IsUpdating = false;
    public List<PlayObject> pobjects = new List<PlayObject>();

    public void Initialize()
    {
        IsUpdating = true;
        OnInitialize();
    }
    public void DeInitialize()
    {
        IsUpdating = false;
        OnDeInitialize();
    }
    public void Pause()
    {
        IsUpdating = false;
        OnPause();
    }
    public void Resume()
    {
        IsUpdating = true;
        OnResume();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUpdating)
        {
            OnUpdate();
        }
    }

    void FixedUpdate()
    {
        if (IsUpdating)
        {
            OnFixedUpdate();
        }
    }
    protected abstract void OnInitialize();
    protected abstract void OnDeInitialize();
    protected abstract void OnResume();
    protected abstract void OnPause();
    protected abstract void OnUpdate();
    protected abstract void OnFixedUpdate();
}
