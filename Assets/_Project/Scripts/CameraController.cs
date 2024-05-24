using System.Collections;
using Cinemachine;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, PlayerControls.IPlayerActions
{
    [SerializeField] private Camera _MainCamera;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _minZoomLimit;
    [SerializeField] private float _maxZoomLimit;
    
    [SerializeField] private float _edgeSize;
    [SerializeField] private float _moveCameraInitialSpeed;
    [SerializeField] private float _moveCameraSpeedMultiplayer;

    private PlayerControls _playerControls;
    private Vector3 _dragOrigin;

    private Coroutine _moveCoroutine;
    

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _playerControls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }


    public void OnZoom(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        
        var zoomDirection = context.ReadValue<float>();
        
        switch (zoomDirection)
        {
            case > 0:
                _camera.m_Lens.OrthographicSize -= 0.5f;
                break;
            case < 0:
                _camera.m_Lens.OrthographicSize += 0.5f;
                break;
        }
        
        if (_camera.m_Lens.OrthographicSize <= _minZoomLimit)
        {
            _camera.m_Lens.OrthographicSize = _minZoomLimit;
        } 
        else if (_camera.m_Lens.OrthographicSize >= _maxZoomLimit)
        {
            _camera.m_Lens.OrthographicSize = _maxZoomLimit;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _dragOrigin = _MainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }

        if (!context.performed) return;
        
        var mouseCurrentPos = _MainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        var distance = mouseCurrentPos - _dragOrigin;
        transform.position += new Vector3(-distance.x, -distance.y, 0);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }

        var position = context.ReadValue<Vector2>();

        if (position.x > Screen.width - _edgeSize)
        {
            _moveCoroutine = StartCoroutine(MoveCamera(Vector3.right));
            return;
        }
        if (position.x < _edgeSize)
        {
            _moveCoroutine = StartCoroutine(MoveCamera(Vector3.left));
            return;
        }
        if (position.y > Screen.height - _edgeSize)
        {
            _moveCoroutine = StartCoroutine(MoveCamera(Vector3.up));
            return;
        }
        if (position.y < _edgeSize)
        {
            _moveCoroutine = StartCoroutine(MoveCamera(Vector3.down));
            return;
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        RaycastHit hit; 
        Vector3 coor = Mouse.current.position.ReadValue();
        if (Physics.Raycast(_MainCamera.ScreenPointToRay(coor), out hit)) 
        {
            hit.collider.GetComponent<IClickable>()?.OnClick();
            print("ON Clicki to CameraController");
        }
    }

    private IEnumerator MoveCamera(Vector3 obj)
    {
        var multiplayer = _moveCameraSpeedMultiplayer;
        while (true)
        {
            transform.position += obj * Time.deltaTime * _moveCameraInitialSpeed * multiplayer;
            multiplayer += Time.deltaTime;
            yield return null;
        }
    }
}
