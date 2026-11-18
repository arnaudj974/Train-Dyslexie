using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(VirtualMouseInput))]
public class VirtualCursorOffset : MonoBehaviour
{
    VirtualMouseInput vm;
    RectTransform rt;
    Canvas canvas;
    RectTransform canvasRect;

    void Awake()
    {
        vm = GetComponent<VirtualMouseInput>();
        rt = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>().rootCanvas;
        canvasRect = (RectTransform)canvas.transform;
    }
    void OnEnable()
    {
        StartCoroutine(CenterCursor());
    }

    System.Collections.IEnumerator CenterCursor()
    {
        yield return null;

        if (vm.virtualMouse != null)
        {
            Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            InputState.Change(vm.virtualMouse.position, center);
        }
    }

    void LateUpdate()
    {
        if (vm.virtualMouse == null) return;

        // Position réelle de la souris virtuelle, en pixels écran
        Vector2 screenPos = vm.virtualMouse.position.ReadValue();

        // Pixels écran -> unités Canvas, puis recentrage car l'ancre est au milieu
        rt.anchoredPosition = screenPos / canvas.scaleFactor - canvasRect.rect.size / 2f;
    }
}