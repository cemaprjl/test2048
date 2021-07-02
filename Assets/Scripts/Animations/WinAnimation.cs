using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimation : MonoBehaviour
{
    private Action<WinAnimation> _callback;

    public void Play(Action<WinAnimation> callback)
    {
        _callback = callback;
        StartCoroutine(AnimateItem());
    }

    private IEnumerator AnimateItem()
    {
        var cam = Camera.main.transform;
        Ray ray = new Ray(cam.position, cam.rotation * Vector3.forward);
        var pos = ray.GetPoint(10f);
        var mag = (pos - transform.position).magnitude;
        yield return new WaitForSeconds(0.3f);
        while (mag > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * mag * 3f);
            mag = (pos - transform.position).magnitude;
            yield return new WaitForEndOfFrame();
        }
        
        _callback?.Invoke(this);
    }
}
