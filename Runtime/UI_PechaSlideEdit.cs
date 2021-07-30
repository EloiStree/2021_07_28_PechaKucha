using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PechaSlideEdit : MonoBehaviour
{
    public UI_ImageLoader m_imageLoader;
    public UI_MetaReminder m_metaReminder;


    public void GetImageUri(out string uri)
    {
        m_imageLoader.GetPathAndImage(out uri, out Texture2D texture);
    }
    public void GetImageAndUri(out string uri, out Texture2D texture)
    {
        m_imageLoader.GetPathAndImage(out uri, out  texture);
    }
    public void GetImageTexture(out Texture2D texture) {
        m_imageLoader.GetPathAndImage(out string path, out  texture);
    }
    public void GetReminder(out PechaMetaReminder reminder) { reminder = m_metaReminder.GetMetaReminder(); }
}
