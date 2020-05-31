using UnityEngine;
using System.Collections;

public class ImpPanel : MonoBehaviour
{
    public GameObject taisDetail;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onTaisClick()
    {
        var taisDetailObj = Instantiate(taisDetail, GetComponentInParent<Canvas>().transform) as GameObject;
        taisDetailObj.GetComponent<TaishouDetail>().gmTaishou = TaisEngine.GMData.inst.taishou;
    }
}
