using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TaisEngine;

public class DialogEsc : MonoBehaviour
{
    public Toggle toggle;

    public void onClickSave()
    {
        GMSerialize.Save(GMData.inst);
    }

    public void onClickQuit()
    {
        SceneManager.LoadScene("sceneStart");
    }

    public void onClickCancel()
    {
        //gameObject.SetActive(false);
        toggle.isOn = false;
    }

    void OnEnable()
    {
        this.transform.SetAsLastSibling();
        Timer.Pause();
    }

    void OnDisable()
    {
        Timer.unPause();
    }
}
