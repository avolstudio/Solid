using System;
using Solid.Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Solid.UI
{
    public class MovableUIElement : SolidBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
    {
        
        public event Action<PointerEventData> PointerDown, PointerUp, PointerHold;

        private Vector2 _offset;
        
        private Vector3 _startPosition;

        public void OnPointerDown(PointerEventData eventData)
        {
            _offset = eventData.position - (Vector2) transform.position;
            
            PointerDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = (Vector2)Input.mousePosition - _offset;
            
            PointerHold?.Invoke(eventData);
        }

        protected override void OnAwake(params object[] parameters)
        {
            _startPosition = transform.position;
        }

        public void ResetPosition()
        {
            transform.position = _startPosition;
        }
    }
}
