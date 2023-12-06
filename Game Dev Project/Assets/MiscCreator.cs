using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscCreator : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject plane;
    void Start()
    {
        plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
