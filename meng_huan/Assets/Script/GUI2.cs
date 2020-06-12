using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnGUI()
    {
        GUILayout.Window(1, new Rect(50, 50, 200, 100), Func1, "窗口1");
    }

    void Func1 (int id)
    {
        if (id == 1)
        {
            GUILayout.Button("是一个按钮");
        }
    }
}
