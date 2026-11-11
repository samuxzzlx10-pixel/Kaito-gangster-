using UnityEngine;
using UnityEngine.EventSystems;

public sealed class MobileFireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private KaitoCombat combat;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (combat != null) combat.SetFireHeld(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (combat != null) combat.SetFireHeld(false);
    }

    private void OnDisable()
    {
        if (combat != null) combat.SetFireHeld(false);
    }
}
