using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float lookSpeed = 6.0f;
    private Transform cameraTransform;
    private float rotY, rotX, lookUpDownValue, lookLeftRightValue;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
