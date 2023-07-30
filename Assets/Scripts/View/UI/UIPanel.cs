using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public void SetShowing(bool showing)
    {
        if(showing == gameObject.activeSelf)
        {
            return;
        }

        gameObject.SetActive(showing);
        OnShowingChanged(showing);
    }

    protected virtual void OnShowingChanged(bool showing)
    {

    }
}
