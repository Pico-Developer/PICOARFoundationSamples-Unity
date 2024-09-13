using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PXR_BoneController : MonoBehaviour
{
    public enum BodyTrackerRole
    {
        Root = 0,  // 骨盆
        LeftUpLeg = 1,  // 左侧臀部
        RightUpLeg = 2,  // 右侧臀部
        Spine3 = 3,  // 脊柱
        LeftLeg = 4,  // 左腿膝盖
        RightLeg = 5,  // 右腿膝盖
        Spine6 = 6,  // 脊柱
        LeftFoot = 7,  // 左脚踝
        RightFoot = 8,  // 右脚踝
        Spine7 = 9,  // 脊柱
        LeftToes = 10,  // 左脚
        RightToes = 11,  // 右脚
        Neck1 = 12,  // 颈部
        LeftShoulder1 = 13,  // 左侧锁骨
        RightShoulder1 = 14,  // 右侧锁骨
        Neck4 = 15,  // 头部
        LeftArm = 16,  // 左肩膀
        RightArm = 17,  // 右肩膀
        LeftForearm = 18,  // 左手肘
        RightForearm = 19,  // 右手肘
        LeftHand = 20,  // 左手腕
        RightHand = 21,  // 右手腕
        LeftHandMid1 = 22,  // 左手
        RightHandMid1 = 23,  // 右手
    }

    const int k_NumSkeletonJoints = 24;

    [SerializeField]
    [Tooltip("The root bone of the skeleton.")]
    Transform m_SkeletonRoot;

    /// <summary>
    /// Get/Set the root bone of the skeleton.
    /// </summary>
    public Transform skeletonRoot
    {
        get
        {
            return m_SkeletonRoot;
        }
        set
        {
            m_SkeletonRoot = value;
        }
    }

    Transform[] m_BoneMapping = new Transform[k_NumSkeletonJoints];

    public void InitializeSkeletonJoints()
    {
        // Walk through all the child joints in the skeleton and
        // store the skeleton joints at the corresponding index in the m_BoneMapping array.
        // This assumes that the bones in the skeleton are named as per the
        // JointIndices enum above.
        Queue<Transform> nodes = new Queue<Transform>();
        nodes.Enqueue(m_SkeletonRoot);
        while (nodes.Count > 0)
        {
            Transform next = nodes.Dequeue();
            for (int i = 0; i < next.childCount; ++i)
            {
                nodes.Enqueue(next.GetChild(i));
            }

            ProcessJoint(next);
        }
    }

    public void ApplyBodyPose(ARHumanBody body)
    {
        var joints = body.joints;
        if (!joints.IsCreated)
        {
            return;
        }

        for (int i = 0; i < k_NumSkeletonJoints; ++i)
        {
            XRHumanBodyJoint joint = joints[i];
            var bone = m_BoneMapping[i];
            if (bone != null)
            {
                bone.transform.position = joint.localPose.position;
                bone.transform.rotation = joint.localPose.rotation;
            }
        }
    }

    void ProcessJoint(Transform joint)
    {
        int index = GetJointIndex(joint.name);
        if (index >= 0 && index < k_NumSkeletonJoints)
        {
            m_BoneMapping[index] = joint;
        }
        else
        {
            Debug.LogWarning($"{joint.name} was not found.");
        }
    }

    // Returns the integer value corresponding to the JointIndices enum value
    // passed in as a string.
    int GetJointIndex(string jointName)
    {
        BodyTrackerRole val;
        if (Enum.TryParse(jointName, out val))
        {
            return (int)val;
        }
        return -1;
    }
}