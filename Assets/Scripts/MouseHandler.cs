using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    private bool _isSwiping = false;
    private Camera _camera;
    private Vector3 _currentMousePosition;
    private SwipeableItem _selectedItem;
    private ExplosionLauncher _explosionLauncher;
    private ItemSwiper _itemSwiper;

    private void Awake()
    {
        _explosionLauncher = GetComponent<ExplosionLauncher>();
        _itemSwiper = GetComponent<ItemSwiper>();
        _camera = Camera.main;
    }

    public void Update()
    {
        ProcessClickDown();

        ProcessSwipe();

        ProcessClickUp();
    }

    private Ray GetRay(Vector3 position) => _camera.ScreenPointToRay(position);

    private void ProcessClickDown()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) == false)
            return;

        _isSwiping = true;
        _currentMousePosition = Input.mousePosition;

        if (Physics.Raycast(GetRay(_currentMousePosition), out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out SwipeableItem item))
                _selectedItem = item;
            else
                if (_explosionLauncher != null)
                _explosionLauncher.MakeExplosionIn(hit);
        }
    }

    private void ProcessSwipe()
    {
        if (_isSwiping == false) return;

        if (_currentMousePosition == Input.mousePosition) return;

        if (_selectedItem == null) return;

        _currentMousePosition = Input.mousePosition;

        Ray touchRay = GetRay(_currentMousePosition);

        if (_itemSwiper != null)
            _itemSwiper.SwipeItem(_selectedItem, touchRay);
    }
    private void ProcessClickUp()
    {
        if (Input.GetMouseButtonUp(LeftMouseButton) == false)
            return;

        _selectedItem = null;
        _isSwiping = false;
    }
}
