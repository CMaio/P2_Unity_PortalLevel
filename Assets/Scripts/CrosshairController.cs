using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private GameObject Crosshair;

    private void Awake()
    {
        NeitherPortalActived();   
    }

    public void BluePortalActived()
    {
        Crosshair.GetComponent<Image>().sprite = Resources.Load<Sprite>("CrosshairTexturesAndUI/portalCrossairB");
    }
    public void OrangePortalActived()
    {
        Crosshair.GetComponent<Image>().sprite = Resources.Load<Sprite>("CrosshairTexturesAndUI/portalCrossairO");
    }
    public void BothPortalsActived()
    {
        Crosshair.GetComponent<Image>().sprite = Resources.Load<Sprite>("CrosshairTexturesAndUI/portalCrossairBO");
    }
    public void NeitherPortalActived()
    {
        Crosshair.GetComponent<Image>().sprite = Resources.Load<Sprite>("CrosshairTexturesAndUI/portalCrossair");
    }
}
