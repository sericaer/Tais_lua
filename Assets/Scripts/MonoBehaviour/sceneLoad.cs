using System;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
//using UnityEngine.UI.Extensions;


public class sceneLoad : MonoBehaviour
{
    GameObject loadErrorPanel;

    // Use this for initialization
    void Start()
    {

        TaisEngine.Config.Load();

        try
        {
            TaisEngine.Mod.Load();
        }
        catch(Exception e) 
        {
            Debug.Log(e.Message);
            Debug.Log(e.InnerException.Message);

            loadErrorPanel.SetActive(true);

            loadErrorPanel.transform.Find("title").GetComponent<Text>().text = e.Message;
            loadErrorPanel.transform.Find("detail").GetComponent<Text>().text = e.InnerException.Message;
        }

        LocalText.getLocalString = TaisEngine.Mod.GetLocalString;

        SceneManager.LoadScene("sceneStart");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnLoadErrorConfirm()
    {
        loadErrorPanel.SetActive(false);
    }

}
