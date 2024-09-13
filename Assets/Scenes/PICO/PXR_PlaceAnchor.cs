using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class PXR_PlaceAnchor : MonoBehaviour
{
    List<ARAnchor> m_Anchors = new();
    public GameObject m_Prefab;
    public GameObject m_AnchorPreview;

    private InputDevice rightController;
    private bool btnAClick = false;
    private bool aLock = false;
    private bool btnAState = false;

    // Start is called before the first frame update
    void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessKeyEvent();
        
        if (btnAClick)
        {
            CreateAnchor(m_AnchorPreview.transform);
        }
    }

    private void ProcessKeyEvent()
    {
        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out btnAState);
        if (btnAState && !aLock)
        {
            btnAClick = true;
            aLock = true;
        }
        else
        {
            btnAClick = false;
        }
        if (!btnAState)
        {
            btnAClick = false;
            aLock = false;
        }
    }

    private void CreateAnchor(Transform transform)
    {
        ARAnchor anchor;
        var anchorPrefab = Instantiate(m_Prefab, transform.position,transform.rotation);
        anchor = ComponentUtils.GetOrAddIf<ARAnchor>(anchorPrefab, true);
        m_Anchors.Add(anchor);
    }

    public void RemoveAllAnchors()
    {
        foreach (var anchor in m_Anchors)
        {
            Destroy(anchor.gameObject);
        }
        m_Anchors.Clear();
    }
}
