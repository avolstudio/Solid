using System;
using Solid.Core;
using UnityEngine;
using UnityEngine.EventSystems;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    public class MovableUIElement : SolidBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
    {
        [SerializeField] private RectTransform _rect;
        public event Action<PointerEventData> PointerDown, PointerUp, PointerHold;

        private Vector2 _offset;
        
        private Vector3 _startPosition;

        private bool _isInteractable = true;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isInteractable) return;

            _offset = eventData.position - (Vector2) transform.position;
            
            PointerDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            PointerUp?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            transform.position = (Vector2)Input.mousePosition - _offset;
            
            PointerHold?.Invoke(eventData);
        }

        protected override void OnAwake(params object[] parameters)
        {
            _startPosition = transform.position;

            _rect = GetComponent<RectTransform>();
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }

        public void SetInteractable(bool isInteractable)
        {
            _isInteractable = isInteractable;
        }
    }
}
