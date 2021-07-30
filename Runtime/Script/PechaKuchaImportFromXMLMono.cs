using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public class PechaKuchaImportFromXMLMono : MonoBehaviour
{
    public TextAsset m_xmlToImport;
    public PechaKuchaDataMono m_dataLoaded;
    [TextArea(0, 10)]
    public string m_exportXml;
    [ContextMenu("Load from XML")]
    public void LoadFromXML()
    {
        string strXML = m_xmlToImport.text;
        PechaKuchaImporterXML.LoadFromXML(strXML, out PechaKuchaWithMeta pechaKucha);
        m_dataLoaded.SetMetaData(pechaKucha);
    }
    [ContextMenu("Save As XML")]

    public void SaveAsXML()
    {
        PechaKuchaImporterXML. SaveAsXML(m_dataLoaded.GetMetaData(), out m_exportXml);
    }
}
    public class PechaKuchaImporterXML 
    { 


        public static void LoadFromXML(string xmlText, out PechaKuchaWithMeta pechaKucha)
    {
        PechaKuchaWithMeta meta = new PechaKuchaWithMeta();

        string strXML = xmlText;

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(strXML);

        XmlNodeList xNodeList = xml.SelectNodes("/xml/slideasuri");
        foreach (XmlNode xNode in xNodeList)
        {
            string id = xNode.Attributes["id"].Value;
            string uri = xNode.InnerText;
            PechaKuchaUtility.Int1To20AsSlideId(id, out bool converted, out PechaSlideId slideId);
            if (converted)
                meta.SetSlidePath(slideId, uri);
        }

         xNodeList = xml.SelectNodes("/xml/slideasbase64");
        foreach (XmlNode xNode in xNodeList)
        {
            string id = xNode.Attributes["id"].Value;
            string base64 = xNode.InnerText;
            PechaKuchaUtility.Int1To20AsSlideId(id, out bool converted, out PechaSlideId slideId);
            if (converted)
                meta.SetSlideAsBase64(slideId, base64);
        }
        xNodeList = xml.SelectNodes("/xml/metainfo");

        foreach (XmlNode xNode in xNodeList)
        {
            string id = xNode.Attributes["id"].Value;
            PechaKuchaUtility.Int1To20AsSlideId(id, out bool converted, out PechaSlideId slideId);
            if (converted) { 
                meta.GetReminder(slideId , out PechaMetaReminder r);
                r.SetTitle(xNode.Attributes["title"].Value);
                r.SetWordOne(xNode.Attributes["word1"].Value);
                r.SetWordTwo(xNode.Attributes["word2"].Value);
                r.SetWordThree(xNode.Attributes["word3"].Value);
                r.SetWordFour(xNode.Attributes["word4"].Value);
                
                meta.SetMetaReminder(slideId , r);
          
            }
        }

        XmlNode pecha = xml.SelectSingleNode("/xml/pitch");
        PechaMetaReminder info= new PechaMetaReminder();
        info.SetTitle(pecha.Attributes["title"].Value);
        info.SetWordOne(pecha.Attributes["word1"].Value);
        info.SetWordTwo(pecha.Attributes["word2"].Value);
        info.SetWordThree(pecha.Attributes["word3"].Value);
        info.SetWordFour(pecha.Attributes["word4"].Value);
        meta.SetOneSentencePitch(pecha.Attributes["onesentence"].Value);
        meta.SetTweetPitch(pecha.Attributes["onetweet"].Value);
        meta.m_topicOfPechaKucha = info;

        XmlNode author = xml.SelectSingleNode("/xml/author");
        PechaAuthor a = meta.GetAuthor();
        a.m_name = author.Attributes["name"].Value;
        a.m_websiteToFindAuthor = author.Attributes["website"].Value;
        a.m_contactInformation = author["howtocontact"].InnerText;
        a.m_avatarAsBase64 = author["avatarasbase64"].InnerText;

        pechaKucha = meta;
    }
   
    public static void SaveAsXML(PechaKuchaWithMeta pechaKucha, out string xmlText)
    {
       PechaSlideId [] ids= PechaKuchaUtility.GetAllSlideAsArray();
        PechaKuchaWithMeta meta = pechaKucha;



        PechaMetaReminder info=meta.m_topicOfPechaKucha;
        StringBuilder sb = new StringBuilder();
        sb.Append("<xml>\n");

        sb.Append("\n");
        sb.Append(string.Format("\t<pitch title=\"{0}\" word1 =\"{1}\" word2 =\"{2}\" word3 =\"{3}\" word4 =\"{4}\" onesentence=\"{5}\" onetweet=\"{6}\" />\n",
            info.GetTitle(), info.GetWordOne(), info.GetWordTwo(), info.GetWordThree(), info.GetWordFour(), meta.GetOneSentencePitch(), meta.GetOneTweetPitch() ));

        sb.Append("\n");
        sb.Append("\n");
        sb.Append("\n");

        string uri;
        foreach (PechaSlideId id in ids)
        {
            meta.GetImagePathOrUrl(id, out uri);
            sb.Append(string.Format("\t<slideasuri id = \"{0}\" > {1} </slideasuri>\n", (int)id, uri));

        }

        sb.Append("\n");
        sb.Append("\n\n\n\n\n\n\n\n");



        PechaMetaReminder reminder;
        foreach (PechaSlideId id in ids)
        {

            meta.GetReminder(id, out reminder);
            sb.Append(string.Format("\t<metainfo  id=\"{0}\" title=\"{1}\" word1 =\"{2}\" word2 =\"{3}\" word3 =\"{4}\" word4 =\"{5}\" />\n", 
                (int)id, reminder.GetTitle(), reminder.GetWordOne(), reminder.GetWordTwo(), reminder.GetWordThree(), reminder.GetWordFour()));

        }
        sb.Append("\n");
        sb.Append("\n\n\n\n\n\n\n\n");

        sb.Append(string.Format("\t<author name=\"{0}\" website=\"{0}\" >\n",
           meta.m_mainAuthor.m_name, meta.m_mainAuthor.m_websiteToFindAuthor));
        sb.Append("\t\t<howtocontact>" + meta.m_mainAuthor.m_contactInformation + "</howtocontact>\n");
        sb.Append("\t\t<avatarasbase64>" + meta.m_mainAuthor.m_avatarAsBase64 + "</avatarasbase64>\n");
        sb.Append("\t</author>\n");
        sb.Append("\n");
        sb.Append("\n\n\n\n\n\n\n\n");

        string base64;
        foreach (PechaSlideId id in ids)
        {
            meta.GetImageAsBase64(id, out base64);
            sb.Append(string.Format("\t<slideasbase64 id = \"{0}\" > {1} </slideasbase64>\n", (int)id, base64));

        }


        sb.Append("\n");
        sb.Append("\n\n\n\n\n\n\n\n");

        sb.Append("</xml>\n");

        xmlText = sb.ToString();
    }

}
