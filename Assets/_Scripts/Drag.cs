using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    private Vector3 mousePositionOffset;


    private TargetJoint2D m_TargetJoint;
    [Range(0.0f, 100.0f)] public float m_Damping = 1.0f;

    [Range(0.0f, 100.0f)] public float m_Frequency = 5.0f;
    
    public bool m_DrawDragLine = true;
	public Color m_Color = Color.cyan;
    
    public LayerMask layer;

    Camera m_Camera;

    void Awake()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());

        if (mouse.leftButton.wasPressedThisFrame )
        {
            var collider = Physics2D.OverlapPoint(mousePosition, layer);
            if (!collider)
                return;
            
			var body = collider.attachedRigidbody;
			if (!body)
				return;

            m_TargetJoint = collider.gameObject.AddComponent<TargetJoint2D>();
            m_TargetJoint.dampingRatio = m_Damping;
            m_TargetJoint.frequency = m_Frequency;

            m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(mousePosition);
            
        }

		if (m_TargetJoint)
		{
			m_TargetJoint.target = mousePosition;

			// debuggg
			if (m_DrawDragLine)
				Debug.DrawLine (m_TargetJoint.transform.TransformPoint (m_TargetJoint.anchor), mousePosition, m_Color);
		}
        
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            Destroy (m_TargetJoint);
			m_TargetJoint = null;
        }

    }
}