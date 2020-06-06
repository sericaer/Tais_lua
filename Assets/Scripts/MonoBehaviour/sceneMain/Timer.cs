using System;
using System.Collections;
using TaisEngine;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Timer : MonoBehaviour
{
    public const int MaxSpeed = 4;
    public const int MinSpeed = 1;

    public static int currSpeed
    {
        get
        {
            return _currSpeed;
        }
        set
        {
            if(value > MaxSpeed)
            {
                _currSpeed = MaxSpeed;
            }
            if(value < MinSpeed)
            {
                _currSpeed = MinSpeed;
            }
        }
    }

    public static void Pause()
    {
        pauseCount++;
    }

    public static void unPause()
    {
        pauseCount--;
    }

    void Start()
    {
#if UNITY_EDITOR
        _currSpeed = 10;
#else
        _currSpeed = MinSpeed;
#endif
        UniTask.Run(async () =>
        {
            try
            {
                while (!GMData.inst.quit)
                {
                    await UniTask.WaitUntil(() => !isPaused);
                    await GMData.inst.DaysInc(CreateDialog);
                    await UniTask.Delay(1000/ currSpeed);
                }
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        });
    }

    void Update()
    {

    }

    public async UniTask CreateDialog(EventDef.Interface gevent)
    {
        if (gevent != null)
        {

            {
                await UniTask.SwitchToMainThread();
                GetComponentInParent<sceneMain>().CreateEventDialogAsync(gevent);
            }


            await UniTask.WaitUntil(() => !isPaused);
        }
    }

    private bool isPaused
    {
        get
        {
            return (isUserPause || pauseCount != 0);
        }
    }

    //private float m_fWaitTime;

    public static bool isUserPause = false;
    //public static bool isSysPause = false;


    private static int pauseCount = 0;

    private static int _currSpeed;
}
