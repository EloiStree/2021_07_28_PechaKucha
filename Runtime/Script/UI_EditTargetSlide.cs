using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EditTargetSlide : MonoBehaviour
{
    public UI_ImageLoader m_imageLoader;
    public UI_MetaReminder m_metaReminder;
    public UI_PechaSlideSelection m_slideSelected;


    public void SetSelectedSlide(PechaSlideId slideId)
    {
        m_slideSelected.SetWith(slideId);
    }

    public void SetSelectedSlide(int index1To20)
    {
        m_slideSelected.SetWith1To20(index1To20);
    }

    public void SetImagePath(string path) {
        m_imageLoader.SetUIWithPath(path);
    }

    public void SetReminder(PechaMetaReminder reminder) {
        m_metaReminder.SetWith(reminder);
    }


    public void GetFullInfo(out PechaSlideId slideId, out string imagePathOrUrl, out Texture2D loadedTexture, out PechaMetaReminder metaInformation) {
        m_slideSelected.GetSlideSelected(out slideId);
        m_imageLoader.GetPathAndImage(out imagePathOrUrl, out loadedTexture);
        metaInformation = m_metaReminder.GetMetaReminder();
    
    }



    

}
