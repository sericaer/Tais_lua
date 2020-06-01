using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModSelectPanel : MonoBehaviour
{
    public GameObject modTogglePrefabs;
    public GameObject modToggleContent;

    public GameObject restartPanel;

    public Dictionary<string, string> modDict;

    void Start()
    {
        modDict = getAllMod();

        foreach (var mod in modDict.Keys)
        {
            var panel = Instantiate(modTogglePrefabs, modToggleContent.transform);
            panel.GetComponentInChildren<Text>().text = mod;
            panel.name = mod;

            panel.GetComponentInChildren<Toggle>().isOn = TaisEngine.Config.inst.select_mods.Contains(mod);
        }

    }

    public void OnConfirm()
    {
        var selected = modToggleContent.GetComponentsInChildren<Toggle>().Where(x => x.isOn).Select(x => x.GetComponentInChildren<Text>().text).ToList();
        Debug.Log($"select mod {string.Join(",", selected)}");


        if (selected.Except(TaisEngine.Config.inst.select_mods).Any() && selected.Count()== TaisEngine.Config.inst.select_mods.Count())
        {
            this.gameObject.SetActive(false);
            return;
        }


        TaisEngine.Config.inst.select_mods = selected;
        TaisEngine.Config.Save();

        restartPanel.SetActive(true);
    }

    public void OnCanel()
    {
        this.gameObject.SetActive(false);
    }

    public void OnReStart()
    {
        SceneManager.LoadScene("sceneLoad");
    }

    private Dictionary<string, string> getAllMod()
    {
        var rslt = new Dictionary<string, string>();

        var modRoot = $"{Application.streamingAssetsPath}/mod/";

        foreach(var path in Directory.EnumerateDirectories(modRoot))
        {
            var infoFile = $"{path}/info.json";
            if (!File.Exists(infoFile))
            {
                continue;
            }

            var modInfo = JsonConvert.DeserializeObject<MOD_INFO>(File.ReadAllText(infoFile));
            rslt.Add(modInfo.name, path);
        }

        return rslt;
    }

    public class MOD_INFO
    {
        public string name;
        public string author;
    }
}
