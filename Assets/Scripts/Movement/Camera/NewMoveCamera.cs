using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoveCamera
{
    public Transform PlayerCameraPosition;
    public Transform CameraHolderPositionl;




    // Update is called once per frame
    public void UpdatingCameraHolderPos(Transform _cameraHolderTrans, Transform _playerCamTrans)
    {
        _cameraHolderTrans.position = _playerCamTrans.position;
    }
}
