using System.Collections;
using System.Linq;

using UnityEngine;
using TaisEngine;
using UnityEngine.UI;

public class ChaotingDetail : MonoBehaviour
{
    public GameObject partyPrefabs;
    public GameObject partyContent;
    public GameObject bufferContent;

    public Text prestige;

    internal Chaoting gmChaoting;

    public void OnConfrim()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        foreach(var party in GMData.inst.parties)
        {
            var panelParty = Instantiate(partyPrefabs, partyContent.transform) as GameObject;
            panelParty.GetComponent<PartyPanel>().gmParty = party;
        }

    }

    // Update is called once per frame
    void Update()
    {
        prestige.text = gmChaoting.prestige.ToString();
        partyContent.GetComponentsInChildren<PartyPanel>().Single(x => x.gmParty.is_first_select).transform.SetAsFirstSibling();
        partyContent.GetComponentsInChildren<PartyPanel>().Single(x => x.gmParty.is_power).transform.SetAsFirstSibling();
    }

}
