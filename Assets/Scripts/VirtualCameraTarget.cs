using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraTarget : MonoBehaviour
{
    [SerializeField] private GameObject _virtualCameraTarget;
    [SerializeField] private int _topClamp;
    [SerializeField] private int _bottomClamp;
    [SerializeField] private float _virtualCameraTargetAngleVerticalOffset;
    private float _virtualCameraTargetAngleHorizontal;
    private float _virtualCameraTargetAngleVertical;
    private Vector2 _input;

    #if !UNITY_ANDROID
    private void OnApplicationFocus(bool focus)
    {
            Cursor.lockState = true ? CursorLockMode.Locked : CursorLockMode.None;
    }
#endif
    private void Awake()
    {
        
    }

    private void Start()
    {
      _virtualCameraTarget.transform.rotation = Quaternion.Euler(_virtualCameraTargetAngleVerticalOffset, 0, 0);  
    }
    private void LateUpdate()
    {
        CameraRot();
    }
    public void CameraRotation(Vector2 input)
    {
        _input = input;
    }

    private void CameraRot()
    {
        if (_input.sqrMagnitude >= 0.01f)
        {
            
        _virtualCameraTargetAngleHorizontal += _input.x;// * Time.deltaTime;
        _virtualCameraTargetAngleVertical += _input.y;// * Time.deltaTime;
        }

        if (_virtualCameraTargetAngleHorizontal < -360f) _virtualCameraTargetAngleHorizontal += 360f;
        if (_virtualCameraTargetAngleHorizontal > 360f) _virtualCameraTargetAngleHorizontal -= 360f;
        _virtualCameraTargetAngleVertical = Mathf.Clamp(_virtualCameraTargetAngleVertical, _bottomClamp, _topClamp);

        _virtualCameraTarget.transform.rotation = Quaternion.Euler(_virtualCameraTargetAngleVertical+ _virtualCameraTargetAngleVerticalOffset, _virtualCameraTargetAngleHorizontal, 0);
    }
}
