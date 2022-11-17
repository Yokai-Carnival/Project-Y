using UnityEngine;

public class ShooterCameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Vector2 _xRotationRange = new(-90,-90);
    [SerializeField] private Vector2 _yRotationRange = new(-45,45);
    private float _cameraRotationX = 0;
    private float _playerRotationY = 0;

    [SerializeField] private Camera _cam;

    private void Start()
    {
        HideCursor();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void LateUpdate()
    {
        UpdateMouseLook();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _mouseSensitivity;
        _cameraRotationX -= mouseDelta.y;
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, _xRotationRange.x, _xRotationRange.y);

        _playerRotationY -= mouseDelta.x;
        _playerRotationY = Mathf.Clamp(_playerRotationY, _yRotationRange.x, _yRotationRange.y);

        _cam.transform.localEulerAngles = Vector3.right * _cameraRotationX;
        transform.localEulerAngles = _playerRotationY * -1 * Vector3.up;
    }
}
