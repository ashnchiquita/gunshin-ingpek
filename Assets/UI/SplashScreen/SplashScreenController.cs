using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenController : ScreenController
{
    public int SplashScreenDurationSeconds;

    public new void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(EnterMainMenu());
    }

    IEnumerator EnterMainMenu()
    {
        yield return new WaitForSeconds(SplashScreenDurationSeconds);
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu, false, false);
    }


}