using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public MoveTouch Touch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Touch.Point3DDir != Vector3.zero)
        {
            transform.forward = Touch.Point3DDir;
            transform.Translate(transform.forward.normalized * Time.deltaTime * 5f, Space.World);
        }
    }
}
