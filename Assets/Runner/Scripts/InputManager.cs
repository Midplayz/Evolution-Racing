using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple Input Manager for a Runner game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the InputManager.
        /// </summary>
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;
        public bool sidewaysMovementEnabled = true;
        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            sidewaysMovementEnabled= true;
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
            sidewaysMovementEnabled = false;
        }

        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }

//#if UNITY_EDITOR
//            m_InputPosition = Mouse.current.position.ReadValue();

//            if (Mouse.current.leftButton.isPressed)
//            {
//                if (!m_HasInput)
//                {
//                    m_PreviousInputPosition = m_InputPosition;
//                }
//                m_HasInput = true;
//            }
//            else
//            {
//                m_HasInput = false;
//            }
//#endif
//#if UNITY_ANDROID||UNITY_IOS
            if (Touch.activeTouches.Count > 0)
            {
                m_InputPosition = Touch.activeTouches[0].screenPosition;

                if (!m_HasInput)
                {
                    m_PreviousInputPosition = m_InputPosition;
                }
                
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }
//#endif
            
            if (m_HasInput && sidewaysMovementEnabled)
            {
                float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
                PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);
            }
            else
            {
                PlayerController.Instance.CancelMovement();
            }

            m_PreviousInputPosition = m_InputPosition;
        }
    }
}

