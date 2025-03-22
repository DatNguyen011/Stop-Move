using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    public Transform TF;
    public Transform playerTF;
    public Camera gameCamera;
    [SerializeField] Vector3 offsetMainMenu;
    [SerializeField] Quaternion rotationMainMenu;
    [SerializeField] Vector3 offsetShop;
    [SerializeField] Vector3 offset;
    [SerializeField] Quaternion rotation;

    private Vector3 currentOffset;
    private Quaternion currentRotation;

    private void Start()
    {
        gameCamera = GetComponent<Camera>();
        currentOffset = offsetMainMenu;
        currentRotation = rotationMainMenu;
    }
    private void LateUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + currentOffset, Time.deltaTime * 5f);
        TF.rotation = Quaternion.Lerp(TF.rotation, currentRotation, Time.deltaTime*5f );
    }

    public void UpdateCameraOffset(int level)
    {
        currentOffset += new Vector3(0, .2f * level, -.1f * level);
    }

    public void ChangeState(int state)
    {
        if (state == 1)
        {
            currentOffset = offsetMainMenu;
            currentRotation = rotationMainMenu;
        }else if (state == 2)
        {
            currentOffset = offset;
            currentRotation = rotation;
        }else if(state == 3)
        {
            currentOffset = offsetShop;
        }
    }
}
