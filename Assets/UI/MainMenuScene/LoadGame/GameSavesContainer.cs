using System;
using System.Collections.Generic;

using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GameSavesContainer : VisualElement
{
    public static readonly string GameSavesContainerUSSClassName = "game-saves-container";

    public static readonly string NoSavesTextUSSClassName = "no-saves-text";
    public static readonly List<string> GameSaveCardUSSClassNameList = new() { "game-save-card" };

    private TextElementWithClassAndName m_NoSavesText = new("NoSavesText", new List<string> { NoSavesTextUSSClassName });


    private bool saveLoaded = false;


    public List<GameSaveData> Saves
    {
        get => GameSaveManager.Instance.GetSaves();

    }

    public GameSavesContainer()
    {

        name = "GameSavesContainer";
        AddToClassList(GameSavesContainerUSSClassName);

        generateVisualContent += GenerateVisualContent;

    }

    public void LoadSave()
    {
        if (GameSaveManager.Instance is not null && !saveLoaded)
        {
            if (Saves.Count <= 0)
            {
                Add(m_NoSavesText);
            }
            else
            {
                int i = 0;
                foreach (GameSaveData save in Saves)
                {
                    // Add GameSave for each save data
                    GameSaveCard gameSaveCard = new(save, i, GameSaveCardUSSClassNameList);
                    Add(gameSaveCard);
                    i++;
                }
            }
            saveLoaded = true;
        }

    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }
}