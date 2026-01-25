using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScrollBarController : MonoBehaviour
    {
        public bool controllable = false;

        private Scrollbar scrollbar;
        private float scrollStep;

        private void Start()
        {
            scrollbar = GetComponent<Scrollbar>();
            
            
            //scrollStep = scrollbar.
        }

        private void Scroll()
        {
            if(!controllable) return;

            float scrollDelta = Input.mouseScrollDelta.y;
            
            if(scrollDelta == 0) return;
            
            
        }
    }
}
