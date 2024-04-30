using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public List<UIDocument> MainMenuUIDocuments;

    public void OnEnable()
    {
        DisplayUIDocument("MainMenu");
    }

    public void DisplayUIDocument(string displayedUIDocumentName)
    {
        foreach (var uiDocument in MainMenuUIDocuments)
        {
            if (uiDocument.ToString().Split(" ")[0] != displayedUIDocumentName)
            {
                uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            }
            else
            {
                uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            }
        }
    }



}
