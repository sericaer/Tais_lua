using System;
using System.Collections;
using TaisEngine;
using Tools;
using UniRx.Async;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Timer : MonoBehaviour
{
    public const int MaxSpeed = 4;
    public const int MinSpeed = 1;

    // AfterAssembliesLoaded is called before BeforeSceneLoad
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void InitUniTaskLoop()
    {
        var loop = PlayerLoop.GetCurrentPlayerLoop();
        PlayerLoopHelper.Initialize(ref loop);
    }

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
            else
            {
                _currSpeed = value;
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
        _currSpeed = 100;
#else
        _currSpeed = MinSpeed;
#endif
        UniTask.Run(async () =>
        {
            try
            {
                while (!GMData.inst.end_flag)
                {
                    //await UniTask.WaitUntil(() => !isPaused);

                    Debug.Log("a");

                    await GMData.inst.DaysInc(CreateDialog);

                    Debug.Log(1000 / currSpeed);
                    await UniTask.Delay(1000/currSpeed, true);

                    Debug.Log("b");
                }

                SceneManager.LoadSceneAsync("sceneEnd");
            }
            catch(Exception e)
            {
                await UniTask.SwitchToMainThread();
                GetComponentInParent<sceneMain>().CreatErrorDialog(e.Message);
                Log.ERRO(e.Message);
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
