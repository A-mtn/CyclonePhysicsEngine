using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntangledBox : MonoBehaviour
{
    [SerializeField] private Transform m_startPosition;
    [SerializeField] private Transform m_endPosition;
    public float speed = 1f;

    private float t = 0f;
    private bool movingForward = true;

    void LateUpdate()
    {
        if (movingForward)
        {
            t += Time.deltaTime * speed;

           
            if (t >= 1f)
            {
                t = 1f; 
                movingForward = false;
            }
        }
        else
        {
            t -= Time.deltaTime * speed;

            
            if (t <= 0f)
            {
                t = 0f; 
                movingForward = true;
            }
        }

        transform.position = Vector3.Lerp(m_startPosition.position, m_endPosition.position, t);
    }

}
