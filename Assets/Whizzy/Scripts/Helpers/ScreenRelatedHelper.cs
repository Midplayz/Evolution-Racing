using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class ScreenRelatedHelper : MonoBehaviour
{

    public static ScreenRelatedHelper instance { get; private set; }
    EventSystem _eventSystem;

    private Camera _camera;
    private const float DEFAULTFLOATDATA = -100000;
    private Vector2 _minWidthAndHeightOfFollowCamera = new Vector2(DEFAULTFLOATDATA, DEFAULTFLOATDATA);
    private Vector2 _minWidthAndHeightOfSideViewCamera = new Vector2(DEFAULTFLOATDATA, DEFAULTFLOATDATA);
    private Vector2 _maxWidthAndHeightOfSideViewCamera = new Vector2(DEFAULTFLOATDATA, DEFAULTFLOATDATA);
    private Vector2 _maxWidthAndHeightOfFollowCamera = new Vector2(DEFAULTFLOATDATA, DEFAULTFLOATDATA);
    [SerializeField]
    private float _widthRatio, _heightRatio;

    private float _zOffset;
    private Vector3 _point1, _point2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _camera = Camera.main;
    }


    public void CalculateWidthAndHeight()
    {
        //float _zOffset = 0;

        //if (_followCamera.gameObject.activeInHierarchy)
        //{
        //    _zOffset = _followCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        //    _point1 = _camera.ScreenToWorldPoint(new Vector3(0, 0, _zOffset));
        //    _minWidthAndHeightOfFollowCamera = new Vector2(_point1.x, _point1.z);
        //    _point2 = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _zOffset));
        //    _maxWidthAndHeightOfFollowCamera = new Vector2(_point2.x, _point2.z);
        //}
        //else
        //{

        //    _zOffset = Mathf.Abs(_mainMenuCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.x);
        //    _point1 = _camera.ScreenToWorldPoint(new Vector3(0, 0, _zOffset));
        //    _minWidthAndHeightOfSideViewCamera = new Vector2(_point1.x, _point1.y);
        //    _point2 = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _zOffset));
        //    _maxWidthAndHeightOfSideViewCamera = new Vector2(_point2.x, _point2.y);
        //}

    }
    public bool IsOverUI(Vector2 position)
    {
        if (_eventSystem == null)
        {
            _eventSystem = EventSystem.current;
        }
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData eventData = new PointerEventData(_eventSystem);
        eventData.position = position;
        EventSystem.current.RaycastAll(eventData, results);

        if (results.Count > 0)
        {
            Debug.Log("Touched UI");
            foreach (RaycastResult raycast in results)
            {
                Debug.Log(raycast.gameObject.name);
            }
        }
        return results.Count > 0;
    }


    public bool IsVisibleInCameraWidthSide(Vector3 pos)
    {
        CalculateWidthAndHeight();
        return (pos.x > _minWidthAndHeightOfFollowCamera.x * _widthRatio && pos.x < _maxWidthAndHeightOfFollowCamera.x * _widthRatio);
    }
    public bool IsVisibleInCameraHeightSide(Vector3 pos)
    {
        CalculateWidthAndHeight();
        return (pos.z > _minWidthAndHeightOfFollowCamera.y && pos.z < _maxWidthAndHeightOfFollowCamera.y);
    }
    public Vector2 GetMinAndMaxWidth()
    {
        //CalculateWidthAndHeight();
        //if (_followCamera.gameObject.activeInHierarchy)
        //{
        //    return new Vector2(_minWidthAndHeightOfFollowCamera.x, _maxWidthAndHeightOfFollowCamera.x);
        //}
        //else
        //{
        //    return new Vector2(_minWidthAndHeightOfSideViewCamera.x, _maxWidthAndHeightOfSideViewCamera.x);
        //}

        return Vector2.zero;
    }
    public Vector2 GetMinAndMaxHeight()
    {
        //CalculateWidthAndHeight();
        //if (_followCamera.gameObject.activeInHierarchy)
        //{

        //    return new Vector2(_minWidthAndHeightOfFollowCamera.y, _maxWidthAndHeightOfFollowCamera.y);
        //}
        //else
        //{
        //    return new Vector2(_minWidthAndHeightOfSideViewCamera.y, _maxWidthAndHeightOfSideViewCamera.y);
        //}
        return Vector2.zero;
    }
    public Vector2 GetMinAndMaxHeightWRTYPosition(float yPos)
    {
        return new Vector2(CalculateMinWidthAndHeightWRTYPos(yPos).y, CalculateMaxWidthAndHeightWRTYPos(yPos).y);
    }
    public Vector2 CalculateMinWidthAndHeightWRTYPos(float yPos)
    {
        float _zOffset = Mathf.Abs(yPos);
        _point1 = _camera.ScreenToWorldPoint(new Vector3(0, 0, _zOffset));
        Vector2 minWidthAndHeightWRTYPos = new Vector2(_point1.x, _point1.z); //point1.z
        return minWidthAndHeightWRTYPos;
    }
    public Vector2 CalculateMaxWidthAndHeightWRTYPos(float yPos)
    {
        float _zOffset = Mathf.Abs(yPos);
        _point2 = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _zOffset));
        Vector2 maxWidthAndHeightWRTYPos = new Vector2(_point2.x, _point2.z);
        return maxWidthAndHeightWRTYPos;
    }
    public Vector2 GetScreenPositionFromWorld(Vector3 _pos, RectTransform img)
    {

        Vector3 positionOnScreen = _camera.WorldToScreenPoint(_pos);
        img.position = positionOnScreen;

        //img.rectTransform.position = new Vector3(img.rectTransform.position.x, img.rectTransform.position.y, 0);
        return img.position;

    }
    public bool isInsideBoundsOfRectTransform()
    {
        return false;
    }
}
