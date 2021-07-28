using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PechaKuchaEditorUIMono : MonoBehaviour
{
    public PechaKuchaBuilderMono m_pechaBuilder;

    public UI_EditTargetSlide m_slideFocusEditor;

    public void SaveCurrentThenLoadSelectionIndex1To20(int index)
    {
        PechaKuchaUtility.Int1To20AsSlideId((uint)index, out PechaSlideId i);
        SaveCurrentThenLoadSelection(i);
    }
    public void SaveCurrentThenLoadSelection(PechaSlideId nextSlide) {

        SaveCurrentEditionToData();
        LoadSlideToBeEdited(nextSlide);
    }

    public void SaveCurrentEditionToData()
    {
        m_slideFocusEditor.GetFullInfo(out PechaSlideId id, out string url, out Texture2D loadedTexture, out PechaMetaReminder info);
        m_pechaBuilder.SetSlideMeta(id, info);
        m_pechaBuilder.SetImageUrl(id, url);
        m_pechaBuilder.SetCurrentTexture(id, loadedTexture);
    }
    public void SaveCurrentEditionToDataToIndex1To20(int index)
    {
        PechaKuchaUtility.Int1To20AsSlideId((uint)index, out PechaSlideId i);
        SaveCurrentEditionToDataTo(i);
    }
    public void SaveCurrentEditionToDataTo(PechaSlideId idTarget)
    {
        m_slideFocusEditor.GetFullInfo(out PechaSlideId id, out string url, out Texture2D loadedTexture, out PechaMetaReminder info);
        m_pechaBuilder.SetSlideMeta(idTarget, info);
        m_pechaBuilder.SetImageUrl(idTarget, url);
        m_pechaBuilder.SetCurrentTexture(idTarget, loadedTexture);
    }

    public void LoadSlideToBeEditedIndex1To20(int index)
    {
        PechaKuchaUtility.Int1To20AsSlideId((uint)index, out PechaSlideId i);
        LoadSlideToBeEdited(i);
    }
        public void LoadSlideToBeEdited(PechaSlideId slideId) {

        m_slideFocusEditor.SetSelectedSlide(slideId);

        m_pechaBuilder.m_pechaKuchaEdited.GetReminder(slideId, out PechaMetaReminder reminder);
        m_slideFocusEditor.SetReminder(reminder);

        m_pechaBuilder.m_pechaKuchaEdited.GetImagePathOrUrl(slideId, out string path);
        m_slideFocusEditor.SetImagePath(path);
    }


}
