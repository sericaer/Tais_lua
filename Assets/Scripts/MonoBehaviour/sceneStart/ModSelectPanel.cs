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

    public GameObject restartPanel;

    public Dictionary<string, string> modDict;

    void Start()
    {
        modDict = getAllMod();

        foreach (var mod in modDict.Keys)
        {
            var panel = Instantiate(modTogglePrefabs, transform.Find("content"));
            panel.GetComponentInChildren<Text>().text = mod;
            panel.name = mod;

            panel.GetComponentInChildren<Toggle>().isOn = TaisEngine.Config.inst.select_mods.ContainsKey(mod);
        }

    }

    public void OnModSelected()
    {
        var selected = GetComponentsInChildren<Toggle>().Where(x => x.isOn).Select(x => x.name).ToList();
        Debug.Log($"select mod {string.Join(",", selected)}");

        var refreshDict = selected.ToDictionary(k => k, v => modDict[v]);

        if (refreshDict.Keys.Equals(TaisEngine.Config.inst.select_mods.Keys))
        {
            this.gameObject.SetActive(false);
            return;
        }


        TaisEngine.Config.inst.select_mods = refreshDict;
        TaisEngine.Config.Save();

        restartPanel.SetActive(true);
    }

    public void OnModCanel()
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
