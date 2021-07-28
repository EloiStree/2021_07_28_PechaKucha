using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PechaSlide : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] RawImage m_slide;
    [SerializeField] AspectRatioFitter m_ratioOfSlide;
    public Texture2D m_emptyImageByDefault;
    public UnityEvent m_onClick;


    private void Start()
    {
        SetImageWith(m_emptyImageByDefault);
    }

    public void SetImageWith(Texture2D slideImage) {

        if (slideImage == null)
        {
            m_slide.texture = m_emptyImageByDefault;
            m_ratioOfSlide.aspectRatio = (float)m_emptyImageByDefault.width / (float)m_emptyImageByDefault.height;
        }
        else {


            m_ratioOfSlide.aspectRatio = (float)slideImage.width / (float)slideImage.height;
            m_slide.texture = slideImage;
        }
    }
    public void GetCurrentSlideTexture(out Texture2D currentSlide) {

        //TO VERIFY
       currentSlide = (Texture2D) m_slide.texture;
    }


    public  void OnPointerDown(PointerEventData eventData)
    {
        m_onClick.Invoke();
    }

}

