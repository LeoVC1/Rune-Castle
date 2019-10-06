using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Movement { UP, DOWN, RIGHT, LEFT, NONE }
public class LabelMovement : MonoBehaviour
{
    public Movement moveStyle;
    public RectTransform _UIElement;
    Movement moveBack;
    RectTransform _Canvas;
    Vector2 startOffsetMax;
    Vector2 startOffsetMin;
    Vector2 panelSize;
    Vector2 canvasSize;
    public bool hideInStart = false;
    public GameObject graphs;
    private float startTime = 0;
    public float slideSpeed;
    public bool started;
    public bool ready;

    private void Start()
    {
        _UIElement = GetComponent<RectTransform>();
        _Canvas = GetComponent<RectTransform>();
        panelSize.x = _UIElement.rect.width;
        panelSize.y = _UIElement.rect.height;
        canvasSize.x = _Canvas.rect.width;
        canvasSize.y = _Canvas.rect.height;
        startOffsetMax = _UIElement.offsetMax;
        startOffsetMin = _UIElement.offsetMin;

        if (hideInStart == true)
        {
            HideLabel();
        }
        started = true;

    }

    public void UpdateScreenResolution()
    {
        panelSize.x = _UIElement.rect.width;
        panelSize.y = _UIElement.rect.height;
        canvasSize.x = _Canvas.rect.width;
        canvasSize.y = _Canvas.rect.height;
        startOffsetMax = _UIElement.offsetMax;
        startOffsetMin = _UIElement.offsetMin;
    }


    IEnumerator Move(Vector3 destinationPointMin, Vector3 destinationPointMax)
    {
        while (!started)
            yield return null;

        Vector2 actualOffsetMin = _UIElement.offsetMin;
        Vector2 actualOffsetMax = _UIElement.offsetMax;

        float t = startTime;
        float time;
        while (t <= 1)
        {
            t += Time.deltaTime + slideSpeed / 100;
            time = t * t * t * (t * (6f * t - 15f) + 10f);
            _UIElement.offsetMin = Vector2.Lerp(actualOffsetMin, destinationPointMin, time);
            _UIElement.offsetMax = Vector2.Lerp(actualOffsetMax, destinationPointMax, time);
            yield return new WaitForEndOfFrame();
        }
        if (!graphs.activeInHierarchy && hideInStart)
            graphs.SetActive(true);

        ready = true;
    }

    IEnumerator Move(Vector3 destinationPointMin, Vector3 destinationPointMax, float customSpeed)
    {
        while (!started)
            yield return null;

        Vector2 actualOffsetMin = _UIElement.offsetMin;
        Vector2 actualOffsetMax = _UIElement.offsetMax;

        float t = startTime;
        float time;
        while (t <= 1)
        {
            t += Time.deltaTime + customSpeed / 100;
            time = t * t * t * (t * (6f * t - 15f) + 10f);
            _UIElement.offsetMin = Vector2.Lerp(actualOffsetMin, destinationPointMin, time);
            _UIElement.offsetMax = Vector2.Lerp(actualOffsetMax, destinationPointMax, time);
            yield return new WaitForEndOfFrame();
        }
        if (!graphs.activeInHierarchy && hideInStart)
            graphs.SetActive(true);

        ready = true;
    }

    public void HideLabel()
    {
        switch (moveStyle)
        {
            case Movement.DOWN:
                moveBack = Movement.UP;
                MoveDown();
                break;
            case Movement.UP:
                moveBack = Movement.DOWN;
                MoveUp();
                break;
            case Movement.RIGHT:
                moveBack = Movement.LEFT;
                MoveRight();
                break;
            case Movement.LEFT:
                moveBack = Movement.RIGHT;
                MoveLeft();
                break;
            case Movement.NONE:
                gameObject.SetActive(false);
                break;
        }
    }

    public void ShowLabel()
    {
        if (moveStyle != Movement.NONE)
            StartCoroutine(Move(startOffsetMin, startOffsetMax));
        else
            gameObject.SetActive(true);
    }

    public void ShowLabel(float speed)
    {
        if (moveStyle != Movement.NONE)
            StartCoroutine(Move(startOffsetMin, startOffsetMax, speed));
        else
            gameObject.SetActive(true);
    }

    void MoveDown()
    {
        Vector3 newOffsetMin = -(Vector2_Abs(startOffsetMin) + panelSize);
        Vector3 newOffsetMax = -(Vector2_Abs(startOffsetMax) + panelSize);
        newOffsetMin.x = startOffsetMin.x;
        newOffsetMax.x = startOffsetMax.x;
        StartCoroutine(Move(newOffsetMin, newOffsetMax));
    }

    void MoveUp()
    {
        Vector3 newOffsetMin = (Vector2_Abs(startOffsetMin) + panelSize);
        Vector3 newOffsetMax = (Vector2_Abs(startOffsetMax) + panelSize);
        newOffsetMin.x = startOffsetMin.x;
        newOffsetMax.x = startOffsetMax.x;
        StartCoroutine(Move(newOffsetMin, newOffsetMax));
    }

    void MoveRight()
    {
        Vector3 newOffsetMin = (Vector2_Abs(startOffsetMin) + panelSize);
        Vector3 newOffsetMax = (Vector2_Abs(startOffsetMax) + panelSize);
        newOffsetMin.y = startOffsetMin.y;
        newOffsetMax.y = startOffsetMax.y;
        StartCoroutine(Move(newOffsetMin, newOffsetMax));
    }

    void MoveLeft()
    {
        Vector3 newOffsetMin = -(Vector2_Abs(startOffsetMin) + panelSize);
        Vector3 newOffsetMax = -(Vector2_Abs(startOffsetMax) + panelSize);
        newOffsetMin.y = startOffsetMin.y;
        newOffsetMax.y = startOffsetMax.y;
        StartCoroutine(Move(newOffsetMin, newOffsetMax));
    }

    Vector2 Vector2_Abs(Vector2 oldVector)
    {
        return new Vector2(Mathf.Abs(oldVector.x), Mathf.Abs(oldVector.y));
    }

    public void SetStartTime(float value)
    {
        startTime = value;
    }

    public void ResetStartTime()
    {
        startTime = 0;
    }
}
