using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI.Extensions;
//using UnityEngine.UI.Extensions;


public class sceneLoad : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        TaisEngine.Config.Load($"{Application.streamingAssetsPath}/config.json");
        TaisEngine.Mod.Load($"{Application.streamingAssetsPath}/mod/");

        LocalText.getLocalString = TaisEngine.Mod.GetLocalString;

        SceneManager.LoadScene("sceneStart");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
