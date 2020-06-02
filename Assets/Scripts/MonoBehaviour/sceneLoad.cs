using System;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using TaisEngine;


public class sceneLoad : MonoBehaviour
{
    GameObject loadErrorPanel;

    // Use this for initialization
    void Start()
    {

        Config.Load();

        try
        {
            Directory.CreateDirectory(GMSerialize.savePath);

            Mod.Load();
        }
        catch(Exception e) 
        {
            Debug.Log(e.Message);
            Debug.Log(e.InnerException.Message);

            loadErrorPanel.SetActive(true);

            loadErrorPanel.transform.Find("title").GetComponent<Text>().text = e.Message;
            loadErrorPanel.transform.Find("detail").GetComponent<Text>().text = e.InnerException.Message;
        }

        LocalText.getLocalString = Mod.GetLocalString;

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
