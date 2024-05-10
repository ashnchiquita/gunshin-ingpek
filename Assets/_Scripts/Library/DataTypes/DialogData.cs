using System;
using UnityEngine;

[Serializable]
public class DialogData
{
    public string Text;
    public Texture2D OverlayBackground;
    public Color OverlayColor;
    public bool PersonLActive;
    public string PersonLName;
    public Texture2D PersonLImage;
    public bool PersonRActive;
    public string PersonRName;
    public Texture2D PersonRImage;
    public DialogAnimation animation;
    public Audio audio;
}