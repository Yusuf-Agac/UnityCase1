using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    private bool _isFloatingFlag = false;
    private int _messageCount = 0;
    public float floatSpeed = 1.75f;
    
    private RectTransform _canvasRectTransform;
    public GameObject messageBoxPrefab;
    private float _floatHeight = 0f;
    private float _targetHeight;
    private VerticalLayoutGroup _verticalLayoutGroup;

    private void Awake()
    {
        StartCoroutine(MessageQueue());
        _canvasRectTransform = GetComponent<RectTransform>();
        _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _floatHeight = messageBoxPrefab.transform.GetComponent<RectTransform>().rect.height;
        _floatHeight += _verticalLayoutGroup.spacing;
        _targetHeight = _canvasRectTransform.anchoredPosition.y;
        AddMessage("Welcome to the game!");
        AddMessage("This is a test message.", true);
        AddMessage("Welcome to the game!");
        AddMessage("This is a test message.", true);
        AddMessage("Welcome to the game!");
        AddMessage("This is a test message.", true);
    }
    
    public void CleanMessageBox()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddMessage(string message, bool isPlayerMessage = false)
    {
        GameObject messageBox = Instantiate(messageBoxPrefab, transform);
        var tmpText = messageBox.transform.GetChild(0).GetComponent<TMP_Text>();
        tmpText.text = message;
        if (isPlayerMessage) { tmpText.alignment = TextAlignmentOptions.Right; }
        _messageCount++;
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

        while (true)
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
