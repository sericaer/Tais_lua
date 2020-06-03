using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TaisEngine;

public class SaveSelectPanel : MonoBehaviour
{
    public SaveFileContent saveFileContent;

    public Text selectText;
    public Button btnConfirm;

    // Use this for initialization
    void OnEnable()
    {
        saveFileContent.canSelectd = true;
        saveFileContent.RefreshSave();
    }
    // Update is called once per frame
    void Update()
    {
        selectText.text = saveFileContent.seleced;
        if(selectText.text == "")
        {
            selectText.text = "----";
            btnConfirm.interactable = false;
        }
        else
        {
            btnConfirm.interactable = true;
        }
    }

    public void OnCancel()
    {
        gameObject.SetActive(false);
    }

    public void OnConfirm()
    {
        gameObject.SetActive(false);

        GMData.inst = GMSerialize.Load(selectText.text);
        SceneManager.LoadScene("sceneMain");

    }
}
