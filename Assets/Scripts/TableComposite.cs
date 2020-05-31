using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityUITable;

public class TableComposite : MonoBehaviour
{
    public Table title;
    public Table data;

    IDictionary<string, HeaderCellContainer> dataHeaders;
    IList<HeaderCellContainer> titleHeaders;

    // Start is called before the first frame update
    void Start()
    {
        titleHeaders = title.GetComponentsInChildren<HeaderCellContainer>();
        dataHeaders = data.GetComponentsInChildren<HeaderCellContainer>()
                              .ToDictionary(k => k.label.text, v => v); 

        foreach (var title in titleHeaders)
        {
            title.GetComponent<Button>().onClick.AddListener(() =>
            {
                data.ColumnTitleClicked(data.columns.Single(x => x.fieldName == title.columnInfo.fieldName));
            });

            title.columnInfo.columnTitle = TaisEngine.Mod.GetLocalString(title.columnInfo.columnTitle);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
