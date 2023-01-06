using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private RectTransform handle, background;
        public Vector3 direction { get; private set; }
        private Vector2 mousePosition;
        private bool isMouseDown;

        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
                if (Vector2.Distance(mousePosition, background.anchoredPosition) < 250)
                    isMouseDown = true;
            }

            if (isMouseDown && Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);

                handle.anchoredPosition = Vector2.ClampMagnitude((mousePosition - background.anchoredPosition), 150) + background.anchoredPosition;
                direction = mousePosition - background.anchoredPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                handle.anchoredPosition = background.anchoredPosition;
                direction = Vector2.zero;
                isMouseDown = false;
            }
        }

    }

}
