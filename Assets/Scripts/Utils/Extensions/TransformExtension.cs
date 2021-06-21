using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static void DestroyChildren(this Transform parentItem, bool immediate = false)
    {
        int childnum = parentItem.childCount;
        while (childnum > 0)
        {
            Transform child = parentItem.GetChild(0);
            child.SetParent(null);
            if (immediate)
            {
                GameObject.DestroyImmediate(child.gameObject);    
            }
            else
            {
                GameObject.Destroy(child.gameObject);
            }
            childnum--;
        }
    }
}
