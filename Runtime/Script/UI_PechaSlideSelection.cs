using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PechaSlideSelection : MonoBehaviour
{
    public Dropdown m_slideSelection;
    public PechaKuchaSlideEvent m_slideChangeFrom;
    public PechaKuchaSlideEvent m_slideChangeTo;

    public int m_previousSlideSelected;
    private void Start()
    {
        InvokeRepeating("CheckForChange", 0, .5f);
    }

    private void CheckForChange() {

        if (m_slideSelection.value != m_previousSlideSelected)
        {
            int p = m_previousSlideSelected, n = m_slideSelection.value;
            m_slideChangeFrom.Invoke(p + 1);
            m_slideChangeTo.Invoke(n + 1);
            m_previousSlideSelected = n;
        }
    }




    public void GetSlideSelected(out PechaSlideId id)
    {
        int index = m_slideSelection.value + 1;
        index = Mathf.Clamp(index,1,20);
        id = (PechaSlideId) index;
    }

    [ContextMenu("Calibrate Menu 1-20")]
    private void CalibrateMenu()
    {

        if (m_slideSelection != null) {
            List<Dropdown.OptionData> tmp = new List<Dropdown.OptionData>();
            for (int i = 0; i < 20; i++)
            {
                tmp.Add(new Dropdown.OptionData() { text = "Slide " + (i + 1) });
            }
            m_slideSelection.options = tmp;
            m_slideSelection.SetValueWithoutNotify(0);
        }
    }

    public void SetWith(PechaSlideId slideId, bool notifyChange=false)
    {
        int index =-1+ (int)slideId ;
        if (!notifyChange) {
            m_previousSlideSelected = index;
        }
        m_slideSelection.SetValueWithoutNotify(index);
    }

   
    internal void SetWith1To20(int index1_20, bool notifyChange = false)
    {
        PechaKuchaUtility.Int1To20AsSlideId((uint)index1_20, out PechaSlideId slideId);
        SetWith(slideId);
    }
}
