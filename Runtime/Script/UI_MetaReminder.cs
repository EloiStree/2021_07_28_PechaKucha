using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_MetaReminder : MonoBehaviour
{
    public PechaMetaReminder m_reminder;
    public InputField m_title;
    public InputField m_word1;
    public InputField m_word2;
    public InputField m_word3;
    public InputField m_word4;
    public ReminderUnityEvent m_onUserEnterInput;

    public void UpdateUIToDate()
    {
        m_reminder.SetTitle(m_title.text);
        m_reminder.GetWords().SetWordOne(m_word1.text);
        m_reminder.GetWords().SetWordTwo(m_word2.text);
        m_reminder.GetWords().SetWordThree(m_word3.text);
        m_reminder.GetWords().SetWordFour(m_word4.text);
        m_onUserEnterInput.Invoke(m_reminder);
    }

    public PechaMetaReminder GetMetaReminder()
    {
        return m_reminder;
    }
    public void SetWith(PechaMetaReminder reminder)
    {
        m_reminder = reminder;
        UpdateDataToUI();
    }

    public void UpdateDataToUI()
    {

        m_title.text=  m_reminder.GetTitle();
        m_word1.text = m_reminder.GetWords().GetWordOne();
        m_word2.text = m_reminder.GetWords().GetWordTwo();
        m_word3.text = m_reminder.GetWords().GetWordThree();
        m_word4.text = m_reminder.GetWords().GetWordFour();
    }
}

[System.Serializable]
public class ReminderUnityEvent: UnityEvent<PechaMetaReminder>{}