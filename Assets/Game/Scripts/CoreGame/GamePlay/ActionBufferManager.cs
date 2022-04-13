using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ActionBufferManager : MonoBehaviour
{
    public static ActionBufferManager Instance;
    CompositeDisposable _disposable;
    private void Awake()
    {
        Instance = this;
        _disposable = new CompositeDisposable();
    }

    public void ActionDelayTime(Action action ,float timedelay)
    {
        Observable.Timer(TimeSpan.FromSeconds(timedelay)).Subscribe(l => { action.Invoke(); }).AddTo(_disposable);
    }
}
