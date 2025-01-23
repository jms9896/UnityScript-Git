using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRectTransformPosition : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 anchoredPositionBackUp;

    private void Awake()
    {
        if (gameObject.TryGetComponent(out RectTransform rt))
        {
            rectTransform = rt;
            anchoredPositionBackUp = rectTransform.anchoredPosition;
        };
    }

    private void OnDisable()
    {
        if (rectTransform != null && anchoredPositionBackUp != null)
        {
            rectTransform.anchoredPosition =anchoredPositionBackUp;
        }
    }
}
