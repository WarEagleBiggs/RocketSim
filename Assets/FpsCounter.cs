using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    public float fps;
    public TextMeshProUGUI text;
    

    // Update is called once per frame
    void Update()
    {
        fps = 1 / Time.smoothDeltaTime;
        text.SetText(fps.ToString("f0"));
    }
}
