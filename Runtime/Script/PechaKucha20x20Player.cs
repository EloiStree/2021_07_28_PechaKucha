using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PechaKucha20x20Player : MonoBehaviour
{

    public PechaKuchaLoadedInformation m_loadedInformation;
    public pechaKuchaGivenInformation m_givenInformatoin;


}


public enum PechaSlideId:uint{_1=1, _2 = 2, _3 = 3, _4 = 4, _5 = 5, _6 = 6, _7 = 7, _8 = 8, _9 = 9, _10 = 10, _11 = 11, _12 = 12, _13 = 13, _14 = 14, _15 = 15, _16 = 16, _17 = 17, _18 = 18, _19 = 19, _20 = 20 }

public interface PechaKucha20x20
{
    public void GetSlidePathOrUrl(PechaSlideId id, out string pathOrUrl);
    public void GetSlideAsImage(PechaSlideId id, out Texture2D texture);
}

public interface PechaKucha20x20MetaData
{
    public void GetSlideTitle(out string title);
    public void GetSlideFourWord(out string fourword1, out string fourword2, out string fourword3, out string fourword4);
    public void GetSlideFourWord(out string [] fourwords);

}
public interface PechaKuchaPlayer
{
    public void GetCurrentTime(out double secondsPlayed);
    public void GetCurrentSlideId(out PechaSlideId slideId);
   
    public bool IsPlaying();
    public void Play();
    public bool IsPaused();
    public void Pause();

    public void StartToZero();
}



public class PechaKuchaUtility {


    public static void GetSlideId(double timeInSeconds, out PechaSlideId id) {
        id = (PechaSlideId)((timeInSeconds / 20.0)+1);
    }
    public static PechaSlideId[] GetAllSlideAsArray()
    {
        return (PechaSlideId[]) Enum.GetValues(typeof(PechaSlideId));
    }

    public static void GetIndex0To19(PechaSlideId slideId, out int index)
    {
        index = -1 + (int)slideId;
    }
    public static void GetIndex1To20(PechaSlideId slideId, out int index)
    {
        index = (int)slideId;
    }

    public static void Int0To19AsSlideId(uint index0To19, out PechaSlideId slideId)
    {
        if (index0To19 > 19) throw new Exception("Index should not be over 19");
        if (index0To19 < 0) throw new Exception("Index should not be under 0");
        slideId = (PechaSlideId)(index0To19 +1);
    }
    public static void Int1To20AsSlideId(string index1To20, out bool converted, out PechaSlideId slideId) {
        if (uint.TryParse(index1To20, out uint i))
        {
            Int1To20AsSlideId(i, out slideId);
            converted = true;
        }
        else {
            slideId = PechaSlideId._1;
            converted = false;
        }
    }

    public static void Int1To20AsSlideId(uint index1To20, out PechaSlideId slideId)
    {
        if (index1To20 > 20) throw new Exception("Index should not be over 20");
        if (index1To20 < 1) throw new Exception("Index should not be under 1");
        slideId = (PechaSlideId)(index1To20 );
    }

    public static void IsTimeOut(float timerInSeconds, out bool isTimeout)
    {
        isTimeout= timerInSeconds > (20f * 20f);
    }

    public static void GetTotalTimeLeft(float timeInSeconds, out float timeLeft)
    {
        timeLeft = (20 * 20) - timeInSeconds;
    }

    public static void GetSlideTimeLeft(float timeInSeconds, out float timeLeft)
    {
       timeLeft = 20.0f-(timeInSeconds % 20.0f);
    }

    public static void GetTotalTimeLeft(float timeInSeconds, out float minutesLeft, out float secondsLeft)
    {
        GetTotalTimeLeft(timeInSeconds, out float timeLeft);
        float div = (int)(timeLeft / 60f);
        float rest = (timeLeft-(div*60.0f));
        minutesLeft = div;
        secondsLeft = rest;
    }
}



