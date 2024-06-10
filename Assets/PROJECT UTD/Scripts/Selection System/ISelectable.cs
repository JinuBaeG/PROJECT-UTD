using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public interface ISelectable 
    {

        public GameObject Selection { get; }

        void Select();

        void Deselect();
    }
}
