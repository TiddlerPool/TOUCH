using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityChan;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TouchController : MonoBehaviour
{
    public Transform touchTarget;
    public Vector3 aimPos;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private Camera mainCamera;

    [Header("Controllers")]
    [SerializeField] private UIManageMain uIManageMain;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private ThirdPersonController thirdPersonController;

    private void Awake()
    {

        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (starterAssetsInputs.touch) {
            MousePosition();
            SitDown();
        }
        else {
            
            StandUp();
            touchTarget.localPosition = Vector3.forward;
            GetComponentInChildren<SpringManager>().enabled = true;
            GetComponentInChildren<Rig>().weight = 0f;
        }
    }

    private Vector3 MousePosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100,11)) {
            Vector3 v3a = new Vector3(hit.point.x, 0f, hit.point.z);
            Vector3 v3b = new Vector3(transform.position.x, 0f, transform.position.z);
            aimPos = v3a - v3b;
            return aimPos;
        }
        else {
            //Debug.Log("Ray did not hit anything");
            aimPos = Vector3.zero;
            return aimPos;
        }
    }

    private void SitDown()
    {
        thirdPersonController.SitDown = true;
        aimVirtualCamera.gameObject.SetActive(true);
        transform.rotation = Quaternion.LookRotation(aimPos,Vector3.up);
        if(UICanvasCheck())
            Cursor.lockState = CursorLockMode.Confined;
    }

    private void StandUp()
    {
        thirdPersonController.SitDown = false;
        aimVirtualCamera.gameObject.SetActive(false);
        if (UICanvasCheck())
            Cursor.lockState = CursorLockMode.Locked;
    }

    private bool UICanvasCheck() {
        if (uIManageMain.pause.activeSelf) {
            return false;
        }
        return true;
    }
}
