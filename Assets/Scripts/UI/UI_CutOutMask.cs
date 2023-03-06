#region

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

#endregion

namespace Game.UI
{
    public class UI_CutOutMask : RawImage
    {
        public override Material materialForRendering
        {
            get
            {
                Material mat = new Material(base.materialForRendering);
                mat.SetInt("_StencilComp", (int) CompareFunction.NotEqual);
                return mat;
            }
        }
    }
}