using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI.Extensions;

public class ReportPanel : MonoBehaviour
{
    public LocalText name;
    public LocalText age;
    public LocalText background;

    public void OnConfirm()
    {
        Destroy(this.gameObject);

        TaisEngine.GMData.New(TaisEngine.InitData.inst);

        SceneManager.LoadScene("sceneMain");
    }

    public void OnCancel()
    {
        Destroy(this.gameObject);

        GetComponentInParent<sceneInit>().CreateNameAgePanel();
    }



    // Use this for initialization
    void Start()
    {
        name.format = TaisEngine.InitData.inst.taishou.name;
        age.format = TaisEngine.InitData.inst.taishou.age.ToString();
        background.format = TaisEngine.InitData.inst.taishou.background;
    }
}
