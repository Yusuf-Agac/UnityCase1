using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    private bool _isFloatingFlag = false;
    private bool _isMessageQueueIsOn = false;
    private int _messageCount = 0;
    public float floatSpeed = 1.75f;
    
    private RectTransform _canvasRectTransform;
    public GameObject messageBoxPrefab;
    private float _floatHeight = 0f;
    private float _targetHeight;
    private float _tempHeight;
    private VerticalLayoutGroup _verticalLayoutGroup;

    private void Awake()
    {
        _canvasRectTransform = GetComponent<RectTransform>();
        _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _floatHeight = messageBoxPrefab.transform.GetComponent<RectTransform>().rect.height;
        _floatHeight += _verticalLayoutGroup.spacing;
        _targetHeight = _canvasRectTransform.anchoredPosition.y;
        _tempHeight = _targetHeight;
    }
    
    public void CleanMessageBox()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _canvasRectTransform.anchoredPosition = new Vector2(0, _tempHeight);
        _targetHeight = _tempHeight;
        _messageCount = 0;
        _isFloatingFlag = false;
        _isMessageQueueIsOn = false;
    }

    public void AddMessage(string message, bool isPlayerMessage = false)
    {
        GameObject messageBox = Instantiate(messageBoxPrefab, transform);
        var tmpText = messageBox.transform.GetChild(0).GetComponent<TMP_Text>();
        tmpText.text = message;
        if (isPlayerMessage) { tmpText.alignment = TextAlignmentOptions.Right; }
        
        _messageCount++;
        
        if (!_isMessageQueueIsOn)
        {
            StartCoroutine(MessageQueue()); 
            _isMessageQueueIsOn = true;
        }
    }
    
    private IEnumerator MessageQueue()
    {
        while (true)
        {
            if (_messageCount > 0 && !_isFloatingFlag)
            {
                _messageCount--;
                _targetHeight += _floatHeight;
                StartCoroutine(FloatUp());
            }
            yield return null;
        }
    }

    private IEnumerator FloatUp()
    {
        _isFloatingFlag = true;
        
        while (_isFloatingFlag)
        {
            _canvasRectTransform.anchoredPosition = Vector2.Lerp(_canvasRectTransform.anchoredPosition, new Vector2(0, _targetHeight), Time.deltaTime * floatSpeed);
            if (Math.Abs(_canvasRectTransform.anchoredPosition.y - _targetHeight) < 2f)
            {
                _canvasRectTransform.anchoredPosition = new Vector2(0, _targetHeight);
                break;
            }
            yield return null;
        }

        _isFloatingFlag = false;
    }

}
