using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class FirstDraftSaveAndLoadImages : MonoBehaviour
{

    [SerializeField] string m_imageStoragePath;
    public string m_relativeToExeFolderForSlide = "temp\\currentslides";
    private void Awake()
    {

        m_imageStoragePath = Directory.GetCurrentDirectory() + "\\" + m_relativeToExeFolderForSlide;
    }
    public void SaveNearExecutable(PechaSlideId id, Texture2D image) {



    }


    public class ImageLoaderCallback {
        public string m_pathOrUrlUsed;
        public bool m_finishDownloading;
        public Texture2D m_downloaded;
        public string m_error;

        public void SetAsNotDownloaded(string error)
        {

            m_finishDownloading = true;
            m_error = error;
            NotifyAsDownloaded();
        }

        public bool HadError()
        {
            return m_error != null && m_error.Length >0;
        }

        public void SetAsDownloaded(Texture2D texture) {
            m_finishDownloading = true;
            m_downloaded = texture;
            NotifyAsDownloaded();
        }
        private void NotifyAsDownloaded()
        {
            if (m_toDoWhenDownloaded != null)
                m_toDoWhenDownloaded(this);
        }

        internal void Reset()
        {
            throw new NotImplementedException();
        }

        public CallBack m_toDoWhenDownloaded;
        public delegate void CallBack(ImageLoaderCallback info);
    }

   

    public static IEnumerator TryToLoadimageFromComputerOrWeb(string url, ImageLoaderCallback downloadInfo)
    {
        url = url.Trim();
        if (File.Exists(url)) {
            TryToLoadImageFromDisk(url, out bool found, out Texture2D img);
            if (found) {
                downloadInfo.SetAsDownloaded(img);
                yield break;
            }
        }
        yield return TryToLoadimageFromWeb(url, downloadInfo);
    
    }
        public static IEnumerator TryToLoadimageFromWeb(string url, ImageLoaderCallback downloadInfo)
    {
        url= url.Trim();
        if (url == null || url.Length <= 0) {
            downloadInfo.m_pathOrUrlUsed = "";
            downloadInfo.SetAsNotDownloaded("No uri given.");
            yield break ;
        }
        if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)) {

            downloadInfo.m_pathOrUrlUsed = "";
            downloadInfo.SetAsNotDownloaded("URI not well formed.");
            yield break;
        }
        downloadInfo.m_pathOrUrlUsed = url;
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                downloadInfo.SetAsNotDownloaded(uwr.error);
            }
            else
            {
                downloadInfo.SetAsDownloaded(DownloadHandlerTexture.GetContent(uwr));
            }
         }
        
    }

    public static void TryToLoadImageFromDisk(string path, out bool hasBeenFound, out Texture2D image) {

        path = path.Trim();
        if (!File.Exists(path))
        { hasBeenFound = false; image = null; return; }

        LoadTextureToFile(path, out image);
        hasBeenFound = image != null;

    }

    public static void LoadTextureToFile(string path, out Texture2D texture)
    {
        if (!File.Exists(path))
        {
            texture = null;
            return;
        }
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(path);
        texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);

    }


    public void TryToLoadImageNearExecutable(PechaSlideId id, out bool hasBeenFound, out Texture2D image)
    {
        GetPathNearExecutableOf(id, out string path);
        TryToLoadImageFromDisk(path, out hasBeenFound, out image);
    }

    public void SaveNearExe(PechaSlideId id, Texture2D texture) {
        GetPathNearExecutableOf(id, out string path);
        SaveTextureToFile(path, texture);
    }
    public static void SaveTextureToFile(string path , Texture2D texture)
    {
        string dirPath = Path.GetDirectoryName(path);
        if(!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
        System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
    }

    public void GetPathNearExecutableOf(PechaSlideId id, out string path) {
        path = m_imageStoragePath + "\\" + ((int)id) + ".png";
    }
}


