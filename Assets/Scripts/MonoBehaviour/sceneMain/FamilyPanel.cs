using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;

public class FamilyPanel : MonoBehaviour
{
    public TaisEngine.Family gmFamily;

    public LocalText background;
    public LocalText attitude;

    public List<TaisEngine.Person> persons = new List<TaisEngine.Person>();


    // Use this for initialization
    void Start()
    {
        background.format = gmFamily.background.name;
        attitude.format = gmFamily.attitude.ToString();
        persons = gmFamily.persons;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
