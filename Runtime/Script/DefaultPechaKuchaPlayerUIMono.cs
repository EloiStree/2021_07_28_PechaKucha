using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultPechaKuchaPlayerUIMono : MonoBehaviour
{
    public bool m_displayMeta;
    public UI_PechaSlide m_slide;

    public Text m_slideId;
    public Text m_timeLeft;
    public Text m_timeBeforeNextSlide;
    public Text m_title;
    public Text m_fourWords;



    public void SetTitle(string title) => m_title.text = title;
    public void SetWords(params string [] words) => m_fourWords.text = string.Join(" - ", words);
    public void SetTime(float timeInSeconds) {

        PechaKuchaUtility.GetTotalTimeLeft(timeInSeconds, out float minutesLeft, out float secondsLeft);
        PechaKuchaUtility.GetSlideTimeLeft(timeInSeconds, out float timeLeftSlide);

        if( minutesLeft>1)
            m_timeLeft.text = string.Format("{0:0.0}m {1:0}", minutesLeft, secondsLeft);
        else m_timeLeft.text = string.Format("{0:0.0}",  secondsLeft);

        m_timeBeforeNextSlide.text = string.Format("{0:0.0}", timeLeftSlide);
            PechaKuchaUtility.GetSlideId(timeInSeconds, out PechaSlideId id);
        m_slideId.text = ""+((int)id);

    }
    public void SetTexture(Texture2D texture) {
        m_slide.SetImageWith(texture);
    }
}
