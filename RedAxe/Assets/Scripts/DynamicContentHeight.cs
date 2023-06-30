using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicContentHeight : MonoBehaviour
{
    private RectTransform _contentRectTransform;
    
    private void Awake()
    {
        _contentRectTransform = GetComponent<RectTransform>();
        UpdateContentHeight();
    }
    
    public void UpdateContentHeight()
    {
        float contentHeight = transform.Cast<Transform>().Sum(child => child.GetComponent<RectTransform>().rect.height);
        _contentRectTransform.sizeDelta = new Vector2(_contentRectTransform.sizeDelta.x, contentHeight);
    }
}
