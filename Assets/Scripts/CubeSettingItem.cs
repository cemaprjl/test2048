using System;
using UnityEngine;
[Serializable]
public class CubeSettingItem
{
    public Texture Texture;
    public Material Material;
    [Range(1,12)]
    public int Index;
    public int Value => 1 << Index;
}
