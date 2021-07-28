using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PechaKuchaPlayerLogicMono : MonoBehaviour
{

    public bool m_isPlaying;
    public PechaKuchaTimer m_timePasted;


    public void StartPlayer()
    {
        m_isPlaying = true;
        m_timePasted.ResetToZero();
        m_startPlaying.Invoke();
        m_onSlideChanged.Invoke(PechaSlideId._1, PechaSlideId._1);
        m_onTimeChanged.Invoke(0);
    }
    public void StopPlayer()
    {

        m_isPlaying = false;
        m_stopPlaying.Invoke();
    }

    [System.Serializable]
    public class TimeChangeUnityEvent : UnityEvent<float> { }
    public TimeChangeUnityEvent m_onTimeChanged;
    public PechaKuchaSlideFromToEvent m_onSlideChanged;

    public UnityEvent m_startPlaying;
    public UnityEvent m_stopPlaying;

    void Update()
    {
        if (m_isPlaying)
        {

            m_timePasted.GetTime(out float previous);
            m_timePasted.GetSlideId(out PechaSlideId previousId);
            m_timePasted.AddTime(Time.deltaTime);
            m_timePasted.GetTime(out float current);
            m_timePasted.GetSlideId(out PechaSlideId currentId);

            m_onTimeChanged.Invoke(current);
            if (previousId != currentId)
            {
                m_onSlideChanged.Invoke(previousId, currentId);
            }
            if (m_timePasted.IsTimeout())
            {
                StopPlayer();
            }

        }

    }


}





[Serializable]
public class PechaKuchaTimer {

    [SerializeField] float m_timer;

    public void AddTime(float time)
    {
        m_timer += time;
    }
    public void SetTime(float time)
    {
        m_timer = time;
    }
    public void GetTime(out float time) { time = m_timer; }
    public void GetSlideId(out PechaSlideId id) {
        PechaKuchaUtility.GetSlideId(m_timer, out id);
    }


    public void SetAt(PechaSlideId slideId) {
        PechaKuchaUtility.GetIndex0To19(slideId, out int index);
        m_timer = ((float)index) * 20.0f;
    }
    public void ResetToZero() {
        m_timer = 0;
    }

    public bool IsTimeout()
    {
        PechaKuchaUtility.IsTimeOut(m_timer, out bool isTimeout);
        return isTimeout;
    }
}