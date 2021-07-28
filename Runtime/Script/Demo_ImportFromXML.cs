using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public class Demo_ImportFromXML : MonoBehaviour
{
    public TextAsset m_xmlToImport;
    public PechaKuchaDataMono m_dataLoaded;
    [TextArea(0,10)]
    public string m_exportXml;




    [ContextMenu("Load from XML")]
    public void LoadFromXML()
    {
        PechaKuchaWithMeta meta = new PechaKuchaWithMeta();

        string strXML = m_xmlToImport.text;

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(strXML);

        XmlNodeList xNodeList = xml.SelectNodes("/xml/slideuri");
        foreach (XmlNode xNode in xNodeList)
        {
            string id = xNode.Attributes["id"].Value;
            string uri = xNode.InnerText;
            PechaKuchaUtility.Int1To20AsSlideId(id, out bool converted, out PechaSlideId slideId);
            if (converted)
                meta.SetSlidePath(slideId, uri);
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

        XmlNode pecha = xml.SelectSingleNode("/xml/pechakucha");
        PechaMetaReminder info= new PechaMetaReminder();
        info.SetTitle(pecha.Attributes["title"].Value);
        info.SetWordOne(pecha.Attributes["word1"].Value);
        info.SetWordTwo(pecha.Attributes["word2"].Value);
        info.SetWordThree(pecha.Attributes["word3"].Value);
        info.SetWordFour(pecha.Attributes["word4"].Value);
        meta.m_topicOfPechaKucha = info;

        XmlNode author = xml.SelectSingleNode("/xml/author");
        PechaAuthor a = meta.GetAuthor();
        a.m_name = author.Attributes["name"].Value;
        a.m_websiteToFindAuthor = author.Attributes["website"].Value;
        a.m_contactInformation = author["howtocontact"].InnerText;

        m_dataLoaded.SetMetaData(meta);

        m_dataLoaded.TryToLoadAllImagesFromData();
    }
    [ContextMenu("Save As XML")]
    public void SaveAsXML()
    {
       PechaSlideId [] ids= PechaKuchaUtility.GetAllSlideAsArray();
        PechaKuchaWithMeta meta = m_dataLoaded.GetMetaData();



        PechaMetaReminder info=meta.m_topicOfPechaKucha;
        StringBuilder sb = new StringBuilder();
        sb.Append("<xml>\n");

        sb.Append("\n");
        sb.Append(string.Format("\t<pechakucha title=\"{0}\" word1 =\"{1}\" word2 =\"{2}\" word3 =\"{3}\" word4 =\"{4}\"/>\n",
            info.GetTitle(), info.GetWordOne(), info.GetWordTwo(), info.GetWordThree(), info.GetWordFour()));

        sb.Append("\n");

        string uri;
        foreach (PechaSlideId id in ids)
        {
            meta.GetImagePathOrUrl(id, out uri);
            sb.Append(string.Format("\t<slideuri id = \"{0}\" > {1} </slideuri>\n", (int)id, uri));

        }

        sb.Append("\n");
        PechaMetaReminder reminder;
        foreach (PechaSlideId id in ids)
        {

            meta.GetReminder(id, out reminder);
            sb.Append(string.Format("\t<metainfo  id=\"{0}\" title=\"{1}\" word1 =\"{2}\" word2 =\"{3}\" word3 =\"{4}\" word4 =\"{5}\" />\n", 
                (int)id, reminder.GetTitle(), reminder.GetWordOne(), reminder.GetWordTwo(), reminder.GetWordThree(), reminder.GetWordFour()));

        }
        sb.Append("\n");

        sb.Append(string.Format("\t<author name=\"{0}\" website=\"{0}\"\n",
           meta.m_mainAuthor.m_name, meta.m_mainAuthor.m_websiteToFindAuthor));
        sb.Append("\t\t<howtocontact>" + meta.m_mainAuthor.m_contactInformation+ "</howtocontact>\n");
        sb.Append("\t</author>\n");
        sb.Append("\n");
        sb.Append("</xml>\n");

        m_exportXml = sb.ToString();
    }

}
