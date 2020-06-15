using System;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;

using Tools;

public class sceneLoad : MonoBehaviour
{
    public GameObject loadErrorPanel;

    // Use this for initialization
    void Start()
    {
        Application.logMessageReceivedThreaded += Log.exceptionLogCallback;

        Log.INFO("!!!!!APPLICATION START!!!!!");

        Config.Load();

        try
        {
            Directory.CreateDirectory(GMSerialize.savePath);

            Mod.Load();
        }
        catch(Exception e) 
        {
            Log.ERRO(e.Message);
            Log.ERRO(e.InnerException.Message);

            loadErrorPanel.SetActive(true);

            loadErrorPanel.transform.Find("title").GetComponent<Text>().text = e.Message;
            loadErrorPanel.transform.Find("detail").GetComponent<Text>().text = e.InnerException.Message;
            return;
        }

        LocalText.getLocalString = Mod.GetLocalString;

        SceneManager.LoadScene("sceneStart");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLoadErrorConfirm()
    {
        loadErrorPanel.SetActive(false);
        Config.Reset();
        Config.Save();

        SceneManager.LoadScene("sceneLoad");
    }

}
