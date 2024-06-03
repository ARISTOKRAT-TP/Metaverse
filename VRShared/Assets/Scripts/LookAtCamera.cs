using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject mainCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
