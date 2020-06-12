using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    private string txt1;
    private void Start()
    {
        txt1 = "";
    }
    private void OnGUI()
    {
        txt1 = GUILayout.TextField(txt1, GUILayout.Width(100));
        if (GUILayout.Button("camera Go"))
        {
            Camera.main.transform.Translate(Vector3.forward);
        }
    }
}
