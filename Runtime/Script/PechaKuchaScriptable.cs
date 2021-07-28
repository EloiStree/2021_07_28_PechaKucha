using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PechaKuchaWithMeta", menuName = "ScriptableObjects/PechaKucha", order = 1)]
public class PechaKuchaScriptable : ScriptableObject
{
    [SerializeField] PechaKuchaWithMeta m_data = new PechaKuchaWithMeta();

    public PechaKuchaWithMeta GetDataRef()
    {
        return m_data;
    }
}
[System.Serializable]
public class PechaKuchaWithMeta
{
    public PechaMetaReminder m_topicOfPechaKucha = new PechaMetaReminder();
    public string[] m_1to20ImagesPathOrUrl = new string[20];
    public PechaMetaReminder[] m_pechaMetaInfo = new PechaMetaReminder[20];
    public PechaAuthor m_mainAuthor = new PechaAuthor();

    public PechaKuchaWithMeta() {
        for (int i = 0; i < 20; i++)
        {
            m_pechaMetaInfo[i] = new PechaMetaReminder();
        }
        
    }

    public string [] GetAllImagePathOrUrl()
    {
        return m_1to20ImagesPathOrUrl.ToArray() ;
    }

    public void SetPechaKuchaGlobalReminder(PechaMetaReminder reminder) {
        m_topicOfPechaKucha = reminder;
    }

    public void SetMetaReminder(PechaSlideId id, PechaMetaReminder reminderInfo)
    {
        int index = ((int)id) - 1;
        //should I copy it line by line instead of copy the ref ?
        m_pechaMetaInfo[index] = reminderInfo;
    }

    public void GetReminder(PechaSlideId slideId, out PechaMetaReminder reminder)
    {
        PechaKuchaUtility.GetIndex0To19(slideId, out int index);
        reminder = m_pechaMetaInfo[index];
    }

    public void GetImagePathOrUrl(PechaSlideId slideId, out string path)
    {
        PechaKuchaUtility.GetIndex0To19(slideId, out int index);
        path = m_1to20ImagesPathOrUrl[index];
    }

    public void SetSlidePath(PechaSlideId id, string pathOrUrl)
    {
        int index = ((int)id) - 1;
        //should I copy it line by line instead of copy the ref ?
        m_1to20ImagesPathOrUrl[index] = pathOrUrl;
    }

    public void SetAuthor(PechaAuthor author) {
        m_mainAuthor = author;
    }

    public PechaAuthor GetAuthor()
    {
        return m_mainAuthor;
    }
}


[System.Serializable]
public class PechaMetaReminder {

    [SerializeField] string m_slideTitle="";
    [SerializeField] PechaFourWords m_fourWords= new PechaFourWords();

    public PechaFourWords GetWords() { return m_fourWords; }


    public string GetTitle()
    {
        return m_slideTitle;
    }

    public void SetTitle(string text)
    {
       m_slideTitle = text ;
    }

    public string GetWordOne()
    {
        return m_fourWords.GetWordOne();
    }

    public string GetWordThree()
    {
        return m_fourWords.GetWordThree();
    }

    public string GetWordFour()
    {
        return m_fourWords.GetWordFour();
    }

    public string GetWordTwo()
    {
        return m_fourWords.GetWordTwo();
    }


    public void SetWordFour(string text)
    {
        m_fourWords.SetWordFour(text);
    }

    public void SetWordOne(string text)
    {
        m_fourWords.SetWordOne(text);
    }

    public void SetWordThree(string text)
    {
        m_fourWords.SetWordThree(text);
    }

    public void SetWordTwo(string text)
    {
        m_fourWords.SetWordTwo(text);
    }

}

[System.Serializable]
public class PechaFourWords
{
    [SerializeField] string m_word1 = "";
    [SerializeField] string m_word2 = "";
    [SerializeField] string m_word3 = "";
    [SerializeField] string m_word4 = "";

    public string GetWordOne()
    {
        return m_word1;
    }

    public string GetWordThree()
    {
        return m_word3;
    }

    public string GetWordFour()
    {
        return m_word4;
    }

    public string GetWordTwo()
    {
        return m_word2;
    }


    public void SetWordFour(string text)
    {
        m_word4 = text;
    }

    public void SetWordOne(string text)
    {
        m_word1 = text;
    }

    public void SetWordThree(string text)
    {
        m_word3 = text;
    }

    public void SetWordTwo(string text)
    {
        m_word2 = text;
    }

    public string[] GetAll()
    {
        return new string[] { m_word1, m_word2, m_word3, m_word4 };
    }
}
[System.Serializable]
public class PechaAuthor
{
    public bool m_notDefined=true;
    public string m_name;
    public string m_contactInformation;
    public string m_websiteToFindAuthor;
}

