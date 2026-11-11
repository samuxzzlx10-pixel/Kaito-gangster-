using UnityEngine;
using UnityEngine.EventSystems;

public sealed class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private KaitoController player;
    [SerializeField, Range(0.1f, 1f)] private float handleRange = 0.6f;

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        if (background == null || handle == null || player == null) return;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, eventData.pressEventCamera, out Vector2 point)) return;

        Vector2 halfSize = background.rect.size * 0.5f;
        Vector2 normalized = new Vector2(
            halfSize.x > 0f ? point.x / halfSize.x : 0f,
            halfSize.y > 0f ? point.y / halfSize.y : 0f);
        Vector2 input = Vector2.ClampMagnitude(normalized, 1f);
        handle.anchoredPosition = Vector2.Scale(input, halfSize) * handleRange;
        player.SetMoveInput(input);
    }

    public void OnPointerUp(PointerEventData eventData) => ResetJoystick();

    private void OnDisable() => ResetJoystick();

    private void ResetJoystick()
    {
        if (handle != null) handle.anchoredPosition = Vector2.zero;
        if (player != null) player.SetMoveInput(Vector2.zero);
    }
}
