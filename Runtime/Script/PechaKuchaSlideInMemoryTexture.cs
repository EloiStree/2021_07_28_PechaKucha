using System;
using UnityEngine;
public class PechaKuchaCurrentTextureMono
{
    public PechaKuchaSlideInMemoryTexture m_data;
    internal void SetSlideTexture(PechaSlideId id, Texture2D texture)
    {
        m_data.SetSlideTexture(id, texture);
    }
}

[System.Serializable]
public class PechaKuchaSlideInMemoryTexture
{
    public Texture2D[] m_slides = new Texture2D[20];
    public void SetSlideTexture(PechaSlideId id, Texture2D texture)
    {
        int index = -1 + (int)id;
        m_slides[index] = texture;
    }
    public void GetSlideTexture(PechaSlideId id, out Texture2D texture) {
        int index = -1 + (int)id;
        texture = m_slides[index];

    }
}

