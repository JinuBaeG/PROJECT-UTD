using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class TitleUI : UIBase
    {
        public void OnClickStartButton()
        {
            Main.Instance.ChangeScene(SceneType.Game);
        }

        public void OnClickExitButton()
        {
            
        }
    }
}
