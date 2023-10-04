using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float m_smoothness = 0.1f;
    [SerializeField] private Vector3 m_classicalOffset;
    [SerializeField] private Vector3 m_quantumOffset;
    private Vector3 m_offset;

    private Transform m_target;

    private void Start()
    {
        m_offset = m_classicalOffset;
    }

    private void LateUpdate()
    {
        if (m_target)
        {
            Vector3 desiredPosition = m_target.position + m_offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_smoothness);
            transform.position = smoothedPosition;
            
            transform.LookAt(m_target);
        }
        
    }

    public void SetTarget(Transform transform, bool isQuantum)
    {
        m_target = transform;
        m_offset = isQuantum ? m_quantumOffset : m_classicalOffset;
    }
}
