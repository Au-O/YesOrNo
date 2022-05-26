using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private void Start()
    {

    }
    public void ShakeEffect(float shakeLevel, float setShakeTime, float shakeFps, bool isShake)
    {
        CameraShakeEffect cse = transform.GetComponent<CameraShakeEffect>();
        cse.shake(shakeLevel, setShakeTime, shakeFps, isShake);
    }
}