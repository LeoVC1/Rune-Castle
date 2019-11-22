using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameManager gameManager;
    public InputManager inputManager;

    public float CameraMoveSpeed = 120.0f;

    public GameObject Mage;

    public Vector3 offSet;
    public Vector3 offSetBase;
    public float clampAngle = 80.0f;

    public float verticalInputSensitivity = 150.0f;
    public float horizontalInputSensitivityX = 150.0f;

    private float rotY;
    private float rotX;

    private GameObject _target;

    public CameraCollision camCollision;
    public GameObject NewTarget;

    public CanvasGroup principal;
    public CanvasGroup secundario;
    public TextMeshProUGUI enemyDescriptionText;

    bool _nonGameplay = false;
    public float delay = 4.5f;
    float delayTimer = 0;

    // Use this for initialization
    void Start()
    {
        ActivateUI(true);
        camCollision = GetComponentInChildren<CameraCollision>();
        offSetBase = offSet;
        ReturnGameplay();

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.isCameraFreezed && Time.timeScale == 1)
            return;

        float mouseX = 0;

        if(!_nonGameplay)
        {
            mouseX = Input.GetAxis("Mouse X");
        }

        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.P))
            ChangeTarget(NewTarget);

        if (_nonGameplay)
        {
            if(delayTimer < delay)
            {
                float distance = Vector3.Distance(camCollision.transform.position, _target.transform.position);
                if (distance < 18)
                {
                    ActivateUI(false);
                    mouseX = 0.5f;
                    rotY += mouseX * horizontalInputSensitivityX * Time.unscaledDeltaTime;
                    rotX = -clampAngle + (clampAngle * 1.5f) / 2;
                    delayTimer += Time.unscaledDeltaTime;
                }
            }
            else
            {
                ActivateUI(true);
                delayTimer = 0;
                _nonGameplay = false;
                ReturnGameplay();
            }
        }

        rotY += mouseX * horizontalInputSensitivityX * Time.deltaTime;
        rotX += -mouseY * verticalInputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = _target.transform;

        float step = CameraMoveSpeed * Time.unscaledDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position + offSet, step);
    }

    public void ChangeTarget(GameObject newTarget)
    {
        offSet = newTarget.GetComponent<Enemy>().cameraOffset;
        camCollision.maxDistance = 10;
        Time.timeScale = 0;
        CameraMoveSpeed = 150f;
        _target = newTarget;
        _nonGameplay = true;
    }

    public void ReturnGameplay()
    {
        camCollision.maxDistance = 17;
        offSet = offSetBase;
        Time.timeScale = 1;
        CameraMoveSpeed = 100f;
        _target = Mage;
    }

    public void ActivateUI(bool uiPrincipal)
    {
        if(uiPrincipal)
        {
            principal.alpha = 1;
            secundario.alpha = 0;
        }
        else
        {
            principal.alpha = 0;
            secundario.alpha = 1;
        }
    }

    public void ChangeDescription(string enemyDescription)
    {
        enemyDescriptionText.text = enemyDescription;
    }
}
