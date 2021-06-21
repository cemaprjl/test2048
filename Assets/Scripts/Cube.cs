using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public delegate void OnChangeCube(Cube target);
    public event OnChangeCube OnDestroyedEvent;
    public event OnChangeCube OnGrowEvent;
    
    public CubeSettings Settings;
//    [Range(0, 11)]
    [SerializeField]
    [Range(0, 11)]
    private int _value;
    private int _validatedValue = -100;

    public int Val
    {
        get => _value;
        set {
            SetValue(value);
        }
    }
    
    private CubeSettingItem _settingsItem;

    private Rigidbody rigBody => GetComponent<Rigidbody>();
    
    public bool IsBusy { get; set; } = false;
    public int Score => 1 << _value;

    private Vector3 _tmpForce;
    
    public override string ToString()
    {
        return $"Cube[{Score}]";
    }

    public void SetValue(int val)
    {
        _value = val;
        OnValidate();
    }
    
    private void OnValidate()
    {   
        if(Settings == null || _value == _validatedValue) {
            return;
        }
        _value = Math.Min(Settings.Items.Count - 1, _value);
        _value = Math.Max(0, _value);
        _settingsItem = Settings.Items[_value];
        var meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.sharedMaterial = _settingsItem.Material;
        _validatedValue = _value;
    }

    public void EnablePhysics(bool enable)
    {
        rigBody.isKinematic = !enable;
        GetComponent<BoxCollider>().enabled = enable;
    }

    public void Shoot(Vector3 force)
    {
        rigBody.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsBusy)
        {
            return;
        }
        if (other.gameObject.layer == 6)
        {
            Debug.Log("GAME OVER");
            return;
        }
        var otherCube = other.gameObject.GetComponent<Cube>();
        if (otherCube != null && !otherCube.IsBusy)
        {
            if (otherCube.Val == _value)
            {
                IsBusy = true;
                otherCube.IsBusy = true;
                SetValue(_value + 1);
                _tmpForce = rigBody.GetPointVelocity(transform.position);
                EnablePhysics(false);
                otherCube.DestroyCube();
                gameObject.AddComponent<JoinAnimation>().Play(JoinComplete);
            }
        }
    }

    private void JoinComplete(JoinAnimation component)
    {
        Destroy(component);
        transform.localScale = Vector3.one; 
        EnablePhysics(true);
        IsBusy = false;
        rigBody.AddForce(_tmpForce + Vector3.up, ForceMode.Impulse);
//        rigBody.AddForce((Vector3.up * 1f + Vector3.forward * 3f), ForceMode.Impulse);
        
        OnGrowEvent?.Invoke(this);
    }

    
    private void DestroyCube()
    {
        EnablePhysics(false);
        gameObject.AddComponent<DestroyAnimation>().Play(OnDestroyAnimComplete);
    }

    private void OnDestroyAnimComplete(DestroyAnimation component)
    {
        Destroy(component);
        OnDestroyedEvent?.Invoke(this);
    }

    public void Reset()
    {
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        IsBusy = false;
    }
}


