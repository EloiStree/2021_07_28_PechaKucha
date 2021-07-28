using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PechaKuchaDataToUI : MonoBehaviour
{
    public PechaKuchaDataMono m_targetDate;
    public DefaultPechaKuchaPlayerUIMono m_playerUI;


    public void SetTime(float time) {
        m_playerUI.SetTime(time);
    }

    public void SetFromSlideIdChange(PechaSlideId from, PechaSlideId to)
    {

        m_targetDate.GetTextures().GetSlideTexture(to, out Texture2D t);
        m_playerUI.SetTexture(t);
        m_targetDate.GetMetaData().GetReminder(to, out PechaMetaReminder reminder);
        m_playerUI.SetTitle(reminder.GetTitle());
        m_playerUI.SetWords(reminder.GetWords().GetAll()) ;

    }

}
