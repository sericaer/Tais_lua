using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class TaishouDetail : MonoBehaviour
{
    internal TaisEngine.Taishou gmTaishou;

    public new LocalText name;
    public LocalText age;
    public LocalText background;

    public void OnConfrim()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        name.format = gmTaishou.name;
        background.format = gmTaishou.background.name;
    }

    // Update is called once per frame
    void Update()
    {
        age.format = gmTaishou.age.ToString();
    }
}
