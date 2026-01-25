using MySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected Vector2 dragOffset =  Vector2.zero;
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            dragOffset = (Vector2)transform.position - eventData.position;
        }
    
        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = MyExtensions.ClampToScreen(eventData.position + dragOffset);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // do nothing
        }
    }
}
