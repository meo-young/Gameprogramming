using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSInfo : MonoBehaviour
{
    private float deltaTime = 0f;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        text.text = fps.ToString("F0") + "FPS";
    }
}
