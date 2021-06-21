using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinAnimation : MonoBehaviour
{
    private Action<JoinAnimation> _callback;

    public void Play(Action<JoinAnimation> callback)
    {
        _callback = callback;
        StartCoroutine(AnimateItem());
    }

    private IEnumerator AnimateItem()
    {
        var scale = 1f;
        while (scale < 1.3f)
        {
            scale += Time.deltaTime * 5f;
            transform.localScale = Vector3.one * scale; 

            yield return new WaitForEndOfFrame();
        }
        while (scale > 1f)
        {
            scale -= Time.deltaTime * 3f;
            transform.localScale = Vector3.one * scale; 

            yield return new WaitForEndOfFrame();
        }
        
        _callback?.Invoke(this);
    }
}
