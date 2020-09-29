using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    //public Transform player;
    public float smoothFollow = 0.2f;
    public float mouseSense = 10f;
    public float cameraHeight = 0.8f;

    public float smoothZoom = 80f;
    float zoomSpeed = 10f;
    float zoomPos;
    float zoomPosY;
    float zoomMin = -1.4f;
    float zoomMax = -0.6f;
    float zoomMinY = 0.15f;
    float zoomMaxY = 0.5f;
    //float zoomInit = -1.25f;

    float xRot;
    float yRot;

    //public GameObject cameraController;    
    Camera cam;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }
        

    // Update is called once per frame
    void LateUpdate()
    {
        //POSITION
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 startPos = transform.position;
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y + cameraHeight, player.transform.position.z);
        Vector3 smoothPos = Vector3.Lerp(startPos, newPosition, smoothFollow * Time.deltaTime);
        transform.position = smoothPos;

        //ROTATION
        xRot += mouseX * mouseSense * Time.deltaTime;
        yRot -= mouseY * mouseSense * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, -40f, 40f);
        transform.rotation = Quaternion.Euler(yRot, xRot, 0);

        //ZOOM
        /*
        float zoomDir = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
        zoomPos = cam.transform.localPosition.z + (zoomDir * zoomPos * Time.deltaTime);
        zoomPosY = cam.transform.localPosition.y + ((zoomDir * 2.5f) * zoomPosY * Time.deltaTime);
        zoomPos = Mathf.Clamp(zoomPos, zoomMin, zoomMax);
        zoomPosY = Mathf.Clamp(zoomPosY, zoomMinY, zoomMaxY);
        //cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, zoomPos);
        cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, zoomPosY, zoomPos);
        */
    }
}
