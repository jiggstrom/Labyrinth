﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActionController : MonoBehaviour {
    public Transform m_hands; // where we handle tool
    public Tool m_tool { get; set; } // tool that can hit some objects

    public float m_speed = 400.0f; // speed of rotating tool in hands
    public float m_targetAngle = 45.0f;// angle of rotating
    public float m_shiftY = -20.0f;
    //public Animator CharacterAnimator;

    float m_startAngleX;
    private float animationTimeLeft;
    float m_angle = 0.0f;// actual rotated angle relative to start angle
    float m_multiplier = -1.0f;// determines side to rotate
    public bool m_didHitStart { get; protected set; }// determines whether hit started
    void Start()
    {
        m_didHitStart = false;
        m_startAngleX = m_hands.localRotation.x;
    }
    public void Hit()
    {
        m_didHitStart = true;// start hit
    }

    void Update()
    {
        if (!m_didHitStart)
            return;

        // angle increments according to speed
        m_angle = Mathf.Clamp(m_angle + m_multiplier * m_speed * Time.deltaTime, 0.0f, m_targetAngle);
        m_hands.localRotation = Quaternion.Euler(m_startAngleX + m_angle, m_shiftY, m_hands.localRotation.x);
        if(m_angle == m_targetAngle)
        {
            Debug.Log($"Hit with {m_tool?.name}");
            if (m_tool) // if we have tool -> call that 'Action' function
                m_tool.Action();

            m_multiplier *= -1.0f;
        } else if (m_angle == 0.0f)
        {
            m_multiplier *= -1.0f;
            m_didHitStart = false; // if we get there -> hit execution finished
        }
    }
    public void HitSomeSuface()
    {
        if (m_multiplier < 0) return;
        Debug.Log($"Hit with {m_tool?.name}");
        if (m_tool)
        {
            m_tool.Action();
        }
        m_multiplier *= -1.0f;
    }
}
