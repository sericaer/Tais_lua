﻿using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
    public class TooltipStyle : MonoBehaviour
    {
        public Sprite topLeftCorner;
        public Sprite topRightCorner;
        public Sprite bottomLeftCorner;
        public Sprite bottomRightCorner;
        public int tipOffset;
        public LayoutElement mainTextContainer;

        private void Update()
        {
            if(bodyText.text.StartsWith("%") && bodyText.text.EndsWith("%"))
            {
                bodyText.gameObject.SetActive(false);
            }
            else
            {
                bodyText.gameObject.SetActive(true);
            }
        }

        private void Start()
        {
            bodyText = transform.Find("BodyText").GetComponent<Text>();
        }

        private Text bodyText;
    }
}
