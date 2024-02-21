using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;

    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float pitch;

    private float currentZoom;

  
    // Update is called once per frame
    void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate()
    {
        transform.position = player.position - offSet * currentZoom;
        transform.LookAt(player.position + Vector3.up * pitch);
    }
}
