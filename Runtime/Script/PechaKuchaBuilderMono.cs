using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PechaKuchaBuilderMono : MonoBehaviour
{
    public PechaKuchaWithMeta m_pechaKuchaEdited;
    public PechaKuchaSlideInMemoryTexture m_pechaKuchaTexture;
    public PechaKuchaMetaInfoUnityEvent m_metaInfoChanged;
    public PechaKuchaTexturesUnityEvent m_textureChanged;


    public void SetSlideMeta(PechaSlideId id, PechaMetaReminder reminderInfo) {
        m_pechaKuchaEdited.SetMetaReminder(id, reminderInfo);
        m_metaInfoChanged.Invoke(m_pechaKuchaEdited);
    }

    public void SetGlobalMeta(PechaMetaReminder reminderInfo) {
        m_pechaKuchaEdited.SetPechaKuchaGlobalReminder(reminderInfo);
        m_metaInfoChanged.Invoke(m_pechaKuchaEdited);
    }
    public void SetCurrentTexture(PechaSlideId id, Texture2D texture) {
        m_pechaKuchaTexture.SetSlideTexture(id, texture);
        m_textureChanged.Invoke(m_pechaKuchaTexture);
    }

    public void SetImageUrl(PechaSlideId id, string url)
    {
        m_pechaKuchaEdited.SetSlidePath(id, url);
        m_metaInfoChanged.Invoke(m_pechaKuchaEdited);
    }
}


[System.Serializable]
public class PechaKuchaTexturesUnityEvent : UnityEvent<PechaKuchaSlideInMemoryTexture> { };

[System.Serializable]
public class PechaKuchaMetaInfoUnityEvent : UnityEvent<PechaKuchaWithMeta> { };