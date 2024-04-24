using System;
using System.Collections.Generic;
using System.IO;
using _Scripts.Core.Game.Data.Currency;
using _Scripts.Core.Game.Data.Position;
using _Scripts.Core.Game.Data.Statistics;
using _Scripts.Core.Game.Data.Story;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameSaveData : MonoBehaviour 
{
    // Static Instance
    public static GameSaveData Instance;

    // Attributes
    public DifficultyType difficulty = DifficultyType.NORMAL;
    public List<string> events = new();
    // Save the currency of the player
    public CurrencyData currencyData = new();
    // Save the position and the level of the player
    public PositionData positionData = new();
    // Save the story state of the player
    public StoryData storyData;
    public StatisticsData statisticsData = new();
    
    // Constructor
    private void Awake()
    {
        Instance = this;
        storyData = new StoryData(events);
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        GameDataWrapper wrapper = new GameDataWrapper
        {
            difficulty = this.difficulty,
            events = this.events,
            currencyData = this.currencyData,
            positionData = this.positionData,
            storyData = this.storyData,
            statisticsData = this.statisticsData
        };

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
        Debug.Log("Game saved to " + Application.persistentDataPath + "/savegame.json");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameDataWrapper wrapper = JsonUtility.FromJson<GameDataWrapper>(json);

            this.difficulty = wrapper.difficulty;
            this.events = wrapper.events;
            this.currencyData = wrapper.currencyData;
            this.positionData = wrapper.positionData;
            this.storyData = wrapper.storyData;
            this.statisticsData = wrapper.statisticsData;
            Debug.Log("Game loaded from " + path);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
    
}

[Serializable]
public class GameDataWrapper
{
    public DifficultyType difficulty;
    public List<String> events;
    public CurrencyData currencyData;
    public PositionData positionData;
    public StoryData storyData;
    public StatisticsData statisticsData;
}
