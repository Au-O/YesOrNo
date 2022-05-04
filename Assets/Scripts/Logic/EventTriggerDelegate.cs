using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerDelegate : MonoBehaviour, IPointerDownHandler
{
    public UIDataShow ui;
    public void OnPointerDown(PointerEventData eventData)
    {
        //print("���£�������");
        ui.onClick();
    }

}