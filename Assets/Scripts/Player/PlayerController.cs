using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string Mouse_X = "Mouse X";
    private const string Mouse_Y = "Mouse Y";
    private const string Vertical = "Vertical";
    private const string Horizontal = "Horizontal";


    private const float Speed_Scale = 5f,
                        Turn_Speed = 250f;

    private float _mouseX = 0f,
                  _mouseY = 0f,
                  _vertical = 0f,
                  _horizontal = 0f,
                  _currentAngleX = 0f;

    [SerializeField] private bool _useJoystick = false;

    [SerializeField] private Joystick joystickLeft;
    [SerializeField] private Joystick joystickRight;

    [SerializeField] private float _maxClamp = 60;

    private CharacterController characterController;
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
        if (!_useJoystick)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_useJoystick)
        {
            _mouseX = joystickLeft.Horizontal;
            _mouseY = joystickLeft.Vertical;
            _vertical = joystickRight.Vertical;
            _horizontal = joystickRight.Horizontal;
        }
        else
        {
            _mouseX = Input.GetAxis(Mouse_X);
            _mouseY = Input.GetAxis(Mouse_Y);
            _vertical = Input.GetAxis(Vertical);
            _horizontal = Input.GetAxis(Horizontal);
        }

        RotatePlayer();
        MoveCharacter();  
    }

    private void RotatePlayer()
    {
        transform.Rotate(new Vector3(0f, _mouseX * Turn_Speed * Time.deltaTime, 0f));

        _currentAngleX += _mouseY * Turn_Speed * Time.deltaTime * -1f;

        _currentAngleX = Mathf.Clamp(_currentAngleX, -_maxClamp, _maxClamp);

        _camera.transform.localEulerAngles = new Vector3(_currentAngleX, 0f, 0f);

    }

    private void MoveCharacter()
    {
        Vector3 velocity = new Vector3(_horizontal, 0f, _vertical);

        velocity = transform.TransformDirection(velocity) * Speed_Scale;

        characterController.Move(velocity * Time.deltaTime);
    }
}
