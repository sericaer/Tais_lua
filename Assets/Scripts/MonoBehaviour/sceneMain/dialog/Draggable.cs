using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Draggable : MonoBehaviour, IDragHandler
{
    private Vector2 Min;
    private Vector2 Max;

    void Start()
    {
        Min = new Vector2(transform.GetComponent<RectTransform>().rect.width/2, transform.GetComponent<RectTransform>().rect.height/2);
        Max = new Vector2(transform.parent.GetComponent<RectTransform>().rect.width - Min.x, transform.parent.GetComponent<RectTransform>().rect.height - Min.y);
    }
        

    public void OnDrag (PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;

        if (screenPoint.x < Min.x)
        {
            screenPoint.x = Min.x;
        }
        if (screenPoint.y < Min.y)
        {
            screenPoint.y = Min.y;
        }
        if (screenPoint.x > Max.x)
        {
            screenPoint.x = Max.x;
        }
        if (screenPoint.y > Max.y)
        {
            screenPoint.y = Max.y;
        }

        var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);

    }
}
