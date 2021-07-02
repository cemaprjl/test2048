using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    private Action<DestroyAnimation> _callback;

    public void Play(Action<DestroyAnimation> callback)
    {
        _callback = callback;
        StartCoroutine(AnimateItem());
    }

    private IEnumerator AnimateItem()
    {
        var scale = 1f;
        while (scale < 1.1f)
        {
            scale += Time.deltaTime * 2f;
            transform.localScale = Vector3.one * scale; 

            yield return new WaitForEndOfFrame();
        }
        while (scale > 0.1f)
        {
            scale -= Time.deltaTime * 10f;
            transform.localScale = Vector3.one * scale; 

            yield return new WaitForEndOfFrame();
        }
        
        _callback?.Invoke(this);
    }
}
