using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    /// <summary> Loading Progress Ex) 0.0f ~ 1.0f </summary>
    public class LoadingUI : UIBase
    {
        public float LoadingProgress
        {
            set
            {
                progressText.text = $"{value * 100.0f:0.0} %";
                progressBar.fillAmount = value;
            }
        }
        public TMPro.TextMeshProUGUI progressText;
        public UnityEngine.UI.Image progressBar;

        
    }
}
