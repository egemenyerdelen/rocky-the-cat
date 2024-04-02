using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] GameObject cross;
    public Vector3 crossPos {  get {  return cross.transform.position; } }
    [SerializeField] float maxOffsetRange = 1f;
    [SerializeField] Vector3 offSet;
    [SerializeField] float cameraShakeRate = 6f;

    Vector3 mousePosition;
    Vector3 mousePosWorld;
    Vector3 mouseOffset;
    Camera mainCamera;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    CinemachineCameraOffset cinemachineCameraOffset;
    PlayerDizzy playerDizzy;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        mainCamera = Camera.main;
        cinemachineCameraOffset = GetComponentInChildren<CinemachineCameraOffset>();
        cinemachineVirtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        noise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        playerDizzy = GetComponent<PlayerDizzy>();
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition();
        MouseLookOffset();

        if (playerDizzy.DizzyMeter >= 10f)
        {
            CameraShake(cameraShakeRate);
        }
        else
        {
            CameraShake(0f);
        }
    }

    private void MousePosition()
    {
        mousePosition = Input.mousePosition;
        Vector3 mouseCamPos = mainCamera.ScreenToWorldPoint(mousePosition);

        if (playerDizzy.DizzyMeter >= 5f)
        {
            // Calculate offset based on Perlin noise
            float offsetX = Mathf.PerlinNoise(Time.time * 3, 0) * 2 - 1;
            float offsetY = Mathf.PerlinNoise(0, Time.time * 3) * 2 - 1;

            Vector3 denemeOffset = new Vector3(offsetX, offsetY, 0) * 2;
            //Debug.Log(denemeOffset);
            mouseCamPos += denemeOffset;
        }

        cross.transform.position = new Vector3(mouseCamPos.x, mouseCamPos.y, 0);

    }

    private void MouseLookOffset()
    {
        // Get the player's position
        Vector3 playerPos = gameObject.transform.position;

        // Convert mouse position to world space
        mousePosWorld = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        // Calculate offset vector from player to mouse
        mouseOffset = (mousePosWorld - playerPos).normalized * maxOffsetRange;

        // Apply offset to camera's position
        cinemachineCameraOffset.m_Offset = mouseOffset + offSet;
    }

    private void CameraShake(float shake)
    {
        noise.m_AmplitudeGain = shake;
        noise.m_FrequencyGain = shake;
    }
}
