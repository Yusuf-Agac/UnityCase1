using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bargain
{
    public class ChatBox : MonoBehaviour
    {
        private bool _isFloating = false;
        private bool _isMessageQueueRunning = false;
        private int _messageCount = 0;
        public float floatSpeed = 1.75f;
    
        private RectTransform _contentRectTransform;
        public GameObject messageBoxPrefab;
        private float _additiveHeight = 0f;
        private float _targetHeight;
        private float _tempHeight;
        private VerticalLayoutGroup _verticalLayoutGroup;

        private void Awake()
        {
            _contentRectTransform = GetComponent<RectTransform>();
            _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            _additiveHeight = messageBoxPrefab.transform.GetComponent<RectTransform>().rect.height;
            _additiveHeight += _verticalLayoutGroup.spacing;
            _targetHeight = _contentRectTransform.anchoredPosition.y;
            _tempHeight = _targetHeight;
        }
    
        public void CleanMessageBox()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            _contentRectTransform.anchoredPosition = new Vector2(0, _tempHeight);
            _targetHeight = _tempHeight;
            _messageCount = 0;
            _isFloating = false;
            _isMessageQueueRunning = false;
        }

        public void AddMessage(string message, bool isPlayerMessage = false)
        {
            GameObject messageBox = Instantiate(messageBoxPrefab, transform);
            var tmpText = messageBox.transform.GetChild(0).GetComponent<TMP_Text>();
            tmpText.text = message;
            if (isPlayerMessage) { tmpText.alignment = TextAlignmentOptions.Right; }
        
            _messageCount++;
        
            if (!_isMessageQueueRunning)
            {
                StartCoroutine(MessageQueue()); 
                _isMessageQueueRunning = true;
            }
        }
    
        private IEnumerator MessageQueue()
        {
            while (true)
            {
                if (_messageCount > 0 && !_isFloating)
                {
                    _messageCount--;
                    _targetHeight += _additiveHeight;
                    StartCoroutine(FloatUp());
                }
            
                yield return null;
            }
        }

        private IEnumerator FloatUp()
        {
            _isFloating = true;
        
            while (_isFloating)
            {
                _contentRectTransform.anchoredPosition = Vector2.Lerp(_contentRectTransform.anchoredPosition, new Vector2(0, _targetHeight), Time.deltaTime * floatSpeed);
                if (Math.Abs(_contentRectTransform.anchoredPosition.y - _targetHeight) < 2f)
                {
                    _contentRectTransform.anchoredPosition = new Vector2(0, _targetHeight);
                    break;
                }
            
                yield return null;
            }

            _isFloating = false;
        }

    }
}
