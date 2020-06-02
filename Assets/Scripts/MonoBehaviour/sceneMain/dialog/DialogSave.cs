using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TaisEngine;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI.Extensions;

public class DialogSave : MonoBehaviour
{
    public GameObject saveItemPrefabs;
    public GameObject saveItemContent;
    public InputField saveFileNameInput;

    public GameObject saveErrorDialog;

    public List<GameObject> saveItems;

    public void onClickSave()
    {
     
        try
        {
            GMSerialize.Save(saveFileNameInput.text, GMData.inst);
            RefreshSave();
        }
        catch(Exception e)
        {
            saveErrorDialog.transform.Find("content/title").GetComponent<LocalText>().format = e.Message;
            saveErrorDialog.SetActive(true);
        }
    }

    public void onClickCancel()
    {
        gameObject.SetActive(false);
    }

    public void onClickDelete(string saveFileName)
    {
        File.Delete($"{GMSerialize.savePath}/{saveFileName}.save");
        RefreshSave();
    }

    public void onSaveErrorDialogConfirm()
    {
        saveErrorDialog.SetActive(false);
    }

    void Start()
    {
        saveItems = new List<GameObject>();
        saveFileNameInput.text = DateTime.Now.ToString("yyyyMMddHHmmss");
        RefreshSave();
    }

    private void RefreshSave()
    {
        foreach(var elem in saveItems)
        {
            Destroy(elem);
        }
        saveItems.Clear();

        foreach (var elem in Directory.EnumerateFiles(GMSerialize.savePath, "*.save"))
        {
            var saveItem = Instantiate(saveItemPrefabs, saveItemContent.transform) as GameObject;

            var saveFileName = Path.GetFileNameWithoutExtension(elem);
            saveItem.GetComponentInChildren<Text>().text = saveFileName;
            saveItem.GetComponentInChildren<Button>().onClick.AddListener(() => { onClickDelete(saveFileName); });

            saveItems.Add(saveItem);
        }
    }
}
