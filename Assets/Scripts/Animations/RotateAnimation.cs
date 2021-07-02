using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateAnimation : MonoBehaviour
{
    private float _rotationSpeed = 20f;
    private Vector3 _rotationVector;

    public void Play()
    {
        StartCoroutine(AnimateItem());
    }

    private IEnumerator AnimateItem()
    {
        _rotationSpeed = Random.Range(20f, 50f);
        _rotationVector = Vector3.zero;
        while (true)
        {
            _rotationVector.y += Time.deltaTime * _rotationSpeed;
            _rotationVector.x += Time.deltaTime * (_rotationSpeed / 2f);
            transform.localRotation = Quaternion.Euler(_rotationVector);
            _rotationVector.y %= 360f;
            _rotationVector.x %= 360f;
            yield return new WaitForEndOfFrame();
        }
    }
}
