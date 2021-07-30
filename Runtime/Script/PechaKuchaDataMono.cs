using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PechaKuchaDataMono : MonoBehaviour
{
    public PechaKuchaWithMeta m_metaDate;
    public PechaKuchaSlideInMemoryTexture m_textures;

    public PechaKuchaWithMeta GetMetaData() { return m_metaDate; }
    public PechaKuchaSlideInMemoryTexture GetTextures() { return m_textures; }

    public void SetMetaData(PechaKuchaWithMeta metaData) { m_metaDate = metaData; }
    public void SetTextures(PechaKuchaSlideInMemoryTexture textures) { m_textures = textures; }

    public void TryToLoadAllImagesFromData()
    {
     StartCoroutine(   StartLoading());
    }

    private IEnumerator StartLoading()
    {
        int i = 1;
        foreach (string item in m_metaDate.GetAllImagePathOrUrl())
        {

            ImageLoaderCallback callback = new ImageLoaderCallback();
            
            yield return SaveAndLoadImagesUtility.TryToLoadimageFromDataOrURI(item, callback);

            if(!callback.HadError())
                m_textures.SetSlideTexture((PechaSlideId)i, callback.m_downloaded);

            i++;
        }
    }

}
