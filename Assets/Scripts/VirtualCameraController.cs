using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    [SerializeField] private GameObject _virtualCameraTarget;
    [SerializeField] private GameObject _constraintTarget;
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

    private void Start()
    {
      _virtualCameraTarget.transform.rotation = Quaternion.Euler(_virtualCameraTargetAngleVerticalOffset, 0, 0);  
    }
    private void Update()
    {
        CameraTargetRot();
        ConstraintTargetPosition();
    }
    private void LateUpdate()
    {
        CameraRot();
    }
    public void CameraRotation(Vector2 input)
    {
        _input = input;
    }

    private void CameraTargetRot()
    {
        if (_input.sqrMagnitude >= 0.01f)
        {
        _virtualCameraTargetAngleHorizontal += _input.x;
        _virtualCameraTargetAngleVertical += _input.y;
        }

        if (_virtualCameraTargetAngleHorizontal < -360f) _virtualCameraTargetAngleHorizontal += 360f;
        if (_virtualCameraTargetAngleHorizontal > 360f) _virtualCameraTargetAngleHorizontal -= 360f;
        _virtualCameraTargetAngleVertical = Mathf.Clamp(_virtualCameraTargetAngleVertical, _bottomClamp, _topClamp);
    }
    private void ConstraintTargetPosition()
    {
        _constraintTarget.transform.position = _virtualCameraTarget.transform.position + _virtualCameraTarget.transform.forward;
    }

    private void CameraRot()
    {
        _virtualCameraTarget.transform.rotation = Quaternion.Euler(_virtualCameraTargetAngleVertical + _virtualCameraTargetAngleVerticalOffset, _virtualCameraTargetAngleHorizontal, 0);
    }
}
