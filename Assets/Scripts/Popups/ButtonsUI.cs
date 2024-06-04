using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUI : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform popupContainer;
    [SerializeField] Transform buildContainer;

    #endregion

    #region Functions

    public void OpenBuildPopup()
    {
        CraftPopup.Show(popupContainer, buildContainer);
    }

    public void OpenGatherPopup()
    {
        GatherPopup.Show(popupContainer);
    }

    #endregion
}
