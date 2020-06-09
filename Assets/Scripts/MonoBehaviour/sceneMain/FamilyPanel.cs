using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class FamilyPanel : MonoBehaviour
{
    public TaisEngine.Family gmFamily;

    public LocalText background;
    public LocalText attitude;

    // Use this for initialization
    void Start()
    {
        background.format = gmFamily.background.name;
    }

    // Update is called once per frame
    void Update()
    {
        attitude.format = gmFamily.attitude.ToString();
    }
}
