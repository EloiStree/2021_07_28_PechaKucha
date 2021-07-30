using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_ImageLoader : MonoBehaviour
{

    public InputField m_givenPathOrUrl;
    public Texture2D m_textureLoaded;
    public RawImage m_debugDisplay;
    public AspectRatioFitter m_debugRatio;
    public UnityEvent m_onImageLoaded;


    public void SetUIWithPath(string path)
    {
        m_givenPathOrUrl.text = path;
        UpdateUI2Texture();
    }
    public void UpdateUI2Texture() {
        m_downloadimage = true;
    }
    private bool m_downloadimage;
    public void Update()
    {
        if (m_downloadimage) {

            ImageLoaderCallback imageCallback = new ImageLoaderCallback();
            imageCallback.m_toDoWhenDownloaded = (ImageCallback);
            StartCoroutine(SaveAndLoadImagesUtility.TryToLoadimageFromDataOrURI(m_givenPathOrUrl.text, imageCallback));
            m_downloadimage = false;
        }
        
    }

    private void ImageCallback(ImageLoaderCallback info)
    {
        Debug.Log("Test");
        m_givenPathOrUrl.caretColor = info.HadError() ? Color.red : Color.black;
        m_textureLoaded= info.m_downloaded;
        if (m_debugDisplay != null) { 
            m_debugDisplay.texture = info.m_downloaded;
            if (info.m_downloaded != null)
                m_debugRatio.aspectRatio = 
                    (float)info.m_downloaded.width /
                    (float)info.m_downloaded.height;
            else m_debugRatio.aspectRatio = 1;
        }
        m_onImageLoaded.Invoke();
    }

    public void GetPathAndImage(out string imagePathOrUrl, out Texture2D loadedTexture)
    {
        imagePathOrUrl = m_givenPathOrUrl.text;
        loadedTexture = m_textureLoaded;
    }
}
