using UnityEngine;

[ExecuteInEditMode]
public class SafeAreaFitter : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Rect _lastSafeArea;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        OnRectTransformDimensionsChange();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (GetSafeArea() != _lastSafeArea && _canvas != null)
        {
            _lastSafeArea = GetSafeArea();
            FitToSafeArea();
        }
    }

    private void FitToSafeArea()
    {
        var safeArea = GetSafeArea();
        var inverseSize = new Vector2(1f, 1f) / _canvas.pixelRect.size;
        var newAnchorMin = Vector2.Scale(safeArea.position, inverseSize);
        var newAnchorMax = Vector2.Scale(safeArea.position + safeArea.size, inverseSize);

        _rectTransform.anchorMin = newAnchorMin;
        _rectTransform.anchorMax = newAnchorMax;

        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;
    }

    private Rect GetSafeArea()
    {
        return Screen.safeArea;
    }
}