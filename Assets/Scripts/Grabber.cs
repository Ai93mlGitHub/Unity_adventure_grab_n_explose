using UnityEngine;

public class Grabber
{
    private Rigidbody _grabbedObject;
    private Vector3 _offset;
    private IGrabbable _grabbableObject;
    private int _groundLayerMask;
    private Vector3 _targetPosition;

    public Grabber(LayerMask layerMask) => _groundLayerMask = layerMask;

    public void TryGrabObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IGrabbable grabbable = hit.collider.GetComponent<IGrabbable>();
            if (grabbable != null)
            {
                _grabbedObject = hit.collider.GetComponent<Rigidbody>();
                _grabbableObject = grabbable;

                if (_grabbedObject != null)
                {
                    _offset = _grabbedObject.transform.position - GetTargetWorldPosition();
                    _grabbableObject.OnGrab();
                }
            }
        }
    }

    public void DragObject()
    {
        if (_grabbedObject != null)
        {
            Vector3 targetPosition = GetTargetWorldPosition() + _offset;
            _grabbedObject.MovePosition(targetPosition);
        }
    }

    public void ReleaseObject()
    {
        if (_grabbableObject != null)
            _grabbableObject.OnRelease();

        _grabbedObject = null;
        _grabbableObject = null;
    }

    private Vector3 GetTargetWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit groundHit;

        if (Physics.Raycast(ray, out groundHit, Mathf.Infinity, _groundLayerMask))
        {
            _targetPosition = groundHit.point;
            return _targetPosition;
        }

        return _targetPosition;
    }
}
