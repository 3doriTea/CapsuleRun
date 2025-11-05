using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class MoveSliderController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;
    private Slider slider;
    public float Value
    {
        get { return slider.value; }
    }
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        slider.value = 0.0f;  // スライダーが離されたら 0.0にする
    }
}
