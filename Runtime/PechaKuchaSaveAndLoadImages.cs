using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class PechaKuchaSaveAndLoadImages : MonoBehaviour
{
    public static void SaveNearExecutable(PechaSlideId id, Texture2D image)
    {
        SaveAndLoadImagesUtility.SaveTextureAsPNG(id.ToString(), image);
    }

    public static void TryToLoadImageNearExecutable(PechaSlideId id, out bool hasBeenFound, out Texture2D image)
    {
        SaveAndLoadImagesUtility.GetPathNearExecutableOfAsPNG(id.ToString(), out string path);
        SaveAndLoadImagesUtility.LoadTextureFromFile(path, out hasBeenFound, out image);
    }

    public static void SaveNearExe(PechaSlideId id, Texture2D texture)
    {
        SaveAndLoadImagesUtility.GetPathNearExecutableOfAsPNG(id.ToString(), out string path);
        SaveAndLoadImagesUtility.SaveTextureAsPNG(path, texture);
    }

    public static void GetPathNearExecutableOf(PechaSlideId id, out string path)
    {
        SaveAndLoadImagesUtility.GetPathNearExecutableOfAsPNG(id.ToString(), out path);
    }
}


