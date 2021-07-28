using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Pecha1to20Group : MonoBehaviour
{ 
    public UI_PechaSlide [] m_1to20Slide = new UI_PechaSlide[20];
    public PechaKuchaSlideEvent m_onSlideSelected;


    public void SetImages(PechaKuchaSlideInMemoryTexture textures) {

        foreach (PechaSlideId id in PechaKuchaUtility.GetAllSlideAsArray())
        {
            textures.GetSlideTexture(id, out Texture2D text);
            SetImage(id, text);
        }
       
    
    }

    public void SetImage(PechaSlideId id, Texture2D image) {

        int index = -1+(int)id;
        if (m_1to20Slide.Length > index)
            m_1to20Slide[index].SetImageWith(image);
    }

    public void NotifySlideSelected(int id1To20OfSlide) {
        m_onSlideSelected.Invoke(id1To20OfSlide);
    }
}


[System.Serializable]
public class PechaKuchaSlideEvent : UnityEvent<int> { }
[System.Serializable]
public class PechaKuchaSlideFromToEvent : UnityEvent<PechaSlideId,PechaSlideId> { }