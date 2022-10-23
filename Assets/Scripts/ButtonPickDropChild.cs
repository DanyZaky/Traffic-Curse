using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPickDropChild : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GrabStuff grabStuff;

    public void OnPointerDown(PointerEventData eventData)
    {
        //grabStuff.isActivate = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        grabStuff.PickorDropChild();
        
        //grabStuff.isActivate = false;
    }
}
