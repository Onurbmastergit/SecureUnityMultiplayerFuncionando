using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsUI : MonoBehaviour
{
    #region Variables

    [SerializeField] Transform popupContainer;
    [SerializeField] GameObject craftPopup;

    #endregion

    #region Functions

    public void OpenCraftPopup()
    {
        craftPopup.SetActive(true);

    }

    public void OpenGatherPopup()
    {
        GatherPopup.Show(popupContainer);
    }

    #endregion
}
