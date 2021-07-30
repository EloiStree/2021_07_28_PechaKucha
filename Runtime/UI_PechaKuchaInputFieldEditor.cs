using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PechaKuchaInputFieldEditor : MonoBehaviour
{

    public PechaKuchaDataMono m_dataStorage;


    [Header("Slides")]

    public UI_PechaSlideEdit[] m_1_20Slide= new UI_PechaSlideEdit[20];


    [Header("Input")]
    public UI_MetaReminder m_pitchReminder;
    public InputField m_oneTweet;
    public InputField m_oneSentence;
    public InputField m_name;
    public InputField m_website;
    public InputField m_contactInfo;
    public UI_ImageLoader m_avataIcon;
    //public UI_PechaImageLoader m_imageLoader;



    public void LoadFromXML()
    {

        PechaKuchaImporterXML.LoadFromXML(Clipboard , out PechaKuchaWithMeta pecha);
        m_dataStorage.SetMetaData(pecha);
        LoadFromDataStorage();
    }

    public void LoadFromDataStorage()
    {
        PechaKuchaWithMeta pecha = m_dataStorage.GetMetaData();

        m_oneSentence.text =pecha.GetOneSentencePitch();
        m_oneTweet.text =pecha.GetOneTweetPitch();
        m_website.text =pecha.GetAuthor().m_websiteToFindAuthor  ;
        m_name.text =pecha.GetAuthor().m_name ;
        m_contactInfo.text= pecha.GetAuthor().m_contactInformation ;
        m_pitchReminder.SetWith(pecha.m_topicOfPechaKucha);
        for (uint i = 0; i < m_1_20Slide.Length; i++)
        {
            PechaKuchaUtility.Int0To19AsSlideId(i, out PechaSlideId id);
            pecha.GetImagePathOrUrl(id, out string uri);
            m_1_20Slide[i].m_imageLoader.SetUIWithPath(uri);
            pecha.GetReminder(id, out PechaMetaReminder reminder);
            m_1_20Slide[i].m_metaReminder.SetWith(reminder);
        }

        m_avataIcon.SetUIWithPath(pecha.GetAuthor().m_avatarAsBase64);
    }

    public void SaveAsXML()
    {
        SaveDataInStorage();
        PechaKuchaImporterXML.SaveAsXML(m_dataStorage.GetMetaData(), out string text);
        Clipboard = text;
    }
    public void SaveDataInStorage()
    {
        PechaKuchaWithMeta pecha = new PechaKuchaWithMeta();
        pecha.SetOneSentencePitch(m_oneSentence.text);
        pecha.SetTweetPitch(m_oneTweet.text);
        pecha.GetAuthor().m_websiteToFindAuthor = m_website.text;
        pecha.GetAuthor().m_name = m_name.text;
        pecha.GetAuthor().m_contactInformation = m_contactInfo.text;
        pecha.SetPechaKuchaGlobalReminder(m_pitchReminder.GetMetaReminder());
        for (uint i = 0; i < m_1_20Slide.Length; i++)
        {
            m_1_20Slide[i].GetImageAndUri(out string uri, out Texture2D texture);
            Base64ToImage.ConvertTextureToBase64(texture, Base64ToImage.SupportedImageTypeB64.PNG, out bool cs, out string ds, out string dms);
            PechaKuchaUtility.Int0To19AsSlideId(i, out PechaSlideId id);
            pecha.SetSlidePath(id, uri);
            pecha.SetSlideAsBase64(id, dms);

            m_1_20Slide[i].GetReminder(out PechaMetaReminder reminder);
            pecha.SetMetaReminder(id, reminder);
        }



        m_avataIcon.GetPathAndImage(out string p, out Texture2D img);
        Base64ToImage.ConvertTextureToBase64(img, Base64ToImage.SupportedImageTypeB64.PNG, out bool c, out string d, out string dm);
        pecha.GetAuthor().m_avatarAsBase64 = dm;


        m_dataStorage.SetMetaData(pecha);
        m_dataStorage.TryToLoadAllImagesFromData();
    }
    internal static string Clipboard
    {//https://flystone.tistory.com/138
        get
        {
            TextEditor _textEditor = new TextEditor();
            _textEditor.Paste();
            return _textEditor.text;
        }
        set
        {
            TextEditor _textEditor = new TextEditor
            { text = value };

            _textEditor.OnFocus();
            _textEditor.Copy();
        }
    }
}
