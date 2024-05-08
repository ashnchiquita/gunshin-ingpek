using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStatisticsDisplayController : MainMenuScreenController
{
    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);

        GameStatisticsContainer gameStatisticsContainer = rootElement.Query<GameStatisticsContainer>("GameStatisticsContainer");

        gameStatisticsContainer.LoadStatistics();

    }
}
