using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [Header("PortalShoot")]
    [SerializeField] private string portalEnableTag;
    [SerializeField] private GameObject previewPortal;

    [SerializeField] Camera mainCamera;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask portalLayerMask;

    [SerializeField] private GameObject bluePortal;
    [SerializeField] private GameObject redPortal;

    [SerializeField] private UnityEvent BluePortalEvent, OrangePortalEvent, BothPortalEvent;

    private bool isActivePreview = false;
    private int multiplayerScale = 1;
    private bool canScale = false;

    [Header("PortalScale")]
    [SerializeField] int maxScalePortal;
    [SerializeField] int minScalePortal;
    Vector3 previewBaseScale;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip createPortalClip;

    [Header("PortalPart")]
    [SerializeField] GameObject Blueframe;
    [SerializeField] GameObject BlueCamDisplay;
    [SerializeField] GameObject Redframe;
    [SerializeField] GameObject RedCamDisplay;
    Transform oldParent;

    private void Awake()
    {
        previewBaseScale = previewPortal.transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActivePreview = movePreviewPortal();
        }

        if((Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0) && !canScale)
        {
            multiplayerScale = (int) Input.mouseScrollDelta.y;

            Debug.Log(multiplayerScale + "  :SCALE");
            canScale = true;
            
        }
        previewPortal.SetActive(isActivePreview);

        if (Input.GetMouseButtonUp(0) && isActivePreview)
        {
            audioSource.clip = createPortalClip;
            audioSource.Play();

            moveBluePortal();
            isActivePreview = false;
            previewPortal.transform.localScale = previewBaseScale;


            redPortal.GetComponent<PortalCamera>().changeOffset((int)previewPortal.transform.localScale.x);

            if (redPortal.activeSelf) BothPortalEvent.Invoke();
            else BluePortalEvent.Invoke();
        }

        if (Input.GetMouseButtonUp(1) && isActivePreview)
        {
            audioSource.clip = createPortalClip;
            audioSource.Play();

            moveRedPortal();
            isActivePreview = false;
            previewPortal.transform.localScale = previewBaseScale;

            bluePortal.GetComponent<PortalCamera>().changeOffset((int)previewPortal.transform.localScale.x);
            
            if (bluePortal.activeSelf) BothPortalEvent.Invoke();
            else OrangePortalEvent.Invoke();
        }

    }
    private bool movePreviewPortal()
    {
        Ray r = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hitInfo;
        if (Physics.Raycast(r, out hitInfo, maxDistance, portalLayerMask))
        {
            previewPortal.transform.position = hitInfo.point;
            previewPortal.transform.forward = hitInfo.normal;
            if (canScale)
            {
                if((multiplayerScale > 0 && previewPortal.transform.localScale.x + multiplayerScale <= maxScalePortal) || (multiplayerScale < 0 && previewPortal.transform.localScale.x + multiplayerScale >= minScalePortal))
                {
                    previewPortal.transform.localScale = new Vector3(previewPortal.transform.localScale.x + multiplayerScale, previewPortal.transform.localScale.y + multiplayerScale, previewPortal.transform.localScale.z + multiplayerScale);  
                }
                canScale = false;
            }
            return previewPortal.GetComponent<PreviewPortal>().isValidPosition(mainCamera);
        }
        return false;
    }
    private void moveBluePortal()
    {
        bluePortal.SetActive(true);
        bluePortal.transform.position = previewPortal.transform.position;
        bluePortal.transform.forward = previewPortal.transform.forward;

        oldParent = Blueframe.transform.parent;
        Blueframe.transform.parent = null;
        Blueframe.transform.localScale = new Vector3(previewPortal.transform.localScale.x, Blueframe.transform.localScale.y, previewPortal.transform.localScale.z);
        Blueframe.transform.parent = oldParent;

        oldParent = BlueCamDisplay.transform.parent;
        BlueCamDisplay.transform.parent = null;
        BlueCamDisplay.transform.localScale = new Vector3(previewPortal.transform.localScale.x - 0.07f, BlueCamDisplay.transform.localScale.y, previewPortal.transform.localScale.z - 0.07f);
        BlueCamDisplay.transform.parent = oldParent;

        previewPortal.SetActive(false);
    }
    private void moveRedPortal()
    {
        redPortal.SetActive(true);
        OrangePortalEvent.Invoke();
        redPortal.transform.position = previewPortal.transform.position;
        redPortal.transform.forward = previewPortal.transform.forward;

        oldParent = Redframe.transform.parent;
        Redframe.transform.parent = null;
        Redframe.transform.localScale = new Vector3(previewPortal.transform.localScale.x, Redframe.transform.localScale.y, previewPortal.transform.localScale.z);
        Redframe.transform.parent = oldParent;

        oldParent = RedCamDisplay.transform.parent;
        RedCamDisplay.transform.parent = null;
        RedCamDisplay.transform.localScale = new Vector3(previewPortal.transform.localScale.x - 0.07f, RedCamDisplay.transform.localScale.y, previewPortal.transform.localScale.z - 0.07f);
        RedCamDisplay.transform.parent = oldParent;

        previewPortal.SetActive(false);
    }
}
