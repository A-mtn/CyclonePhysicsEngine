using System;
using System.Collections;
using Cyclone.Core;
using CycloneUnityTestScenes;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityTestScenes.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject m_quantumBox;
        private QuantumBox m_quantumBoxScript;
        [SerializeField] private GameObject m_classicalBox;
        private ClassicalBox m_classicalBoxScript;
        [SerializeField] private GameObject[] m_walls;
        
        private bool m_isQuantumState = false;
        private float m_triggerDistance = 3f;
        private bool m_hasClassicalWallHitTriggered = false;
        private bool m_hasQuantumWallHitTriggered = false;
        private Transform m_CurrentQuantumWallTransform;

        [SerializeField] private GameObject m_SliderObject;
        
        private int m_XValue;
        private int m_YValue;
        private int m_ZValue;

        public event Action<int, int, int> addForceToQuantum;
        public event Action<int, int, int> addForceToClassical;
        public event Action classicalObjectHittedWall;
        public event Action quantumObjectHittedWall;
        public event Action clearAllForces;

        [SerializeField] private GameObject m_classicalLaser;
        [SerializeField] private GameObject m_quantumLaser;

        [SerializeField] private CameraFollow m_cameraFollow;

        [SerializeField] private GameObject m_entangledBox;
        [SerializeField] private GameObject m_teleportButton;

        [SerializeField] private GameObject m_tutorialUI1;
        [SerializeField] private GameObject m_tutorialUI2;
        [SerializeField] private GameObject m_tutorialUI3;
        private bool m_tutorialUI1Triggered = false;
        private bool m_tutorialUI2Triggered = false;
        private bool m_tutorialUI3Triggered = false;
        private void Awake()
        {
            m_SliderObject.GetComponent<SliderScript>().fireButtonPressed += OnFireButtonPressed;
            m_quantumBoxScript = m_quantumBox.GetComponent<QuantumBox>();
            m_classicalBoxScript = m_classicalBox.GetComponent<ClassicalBox>();
        }
        
        private void Start()
        {
            //SwitchActive(m_isQuantumState);
            var target = m_isQuantumState == true ? m_quantumBox.transform : m_classicalBox.transform;
            m_cameraFollow.SetTarget(target, m_isQuantumState);
        }

        void SwitchActive(bool isQuantum)
        {
            Debug.Log("in quantum state: " + isQuantum);
            clearAllForces?.Invoke();
            if (isQuantum)
            {
                m_classicalLaser.SetActive(true);
                
                Invoke("SwitchToQuantum", .5f);
            }
            else
            {
                m_quantumLaser.SetActive(true);
                
                Invoke("SwitchToClassical", .5f);

            }
            
        }

        private void SwitchToQuantum()
        {
            m_classicalLaser.SetActive(false);
            m_classicalBox.SetActive(false);
            m_quantumBoxScript.SetBodyPosAndRot(m_classicalBoxScript.GetBodyPosition(), m_classicalBoxScript.GetBodyRotation());
            m_quantumBox.transform.position = m_classicalBox.transform.position;
            m_quantumBox.SetActive(true); 
            m_cameraFollow.SetTarget(m_quantumBox.transform, m_isQuantumState);
        }

        private void SwitchToClassical()
        {
            m_quantumLaser.SetActive(false);
            m_quantumBox.SetActive(false);
            m_classicalBoxScript.SetBodyPosAndRot(m_quantumBoxScript.GetBodyPosition(), m_quantumBoxScript.GetBodyRotation());
            m_classicalBox.transform.position = m_quantumBox.transform.position;
            m_classicalBox.SetActive(true);
            m_cameraFollow.SetTarget(m_classicalBox.transform, m_isQuantumState);
        }

        public void SwitchButtonPressed()
        {
            m_isQuantumState = !m_isQuantumState;
            SwitchActive(m_isQuantumState);
        }

        public void OnFireButtonPressed(int valueX, int valueY, int valueZ)
        {
            m_XValue = valueX;
            m_YValue = valueY;
            m_ZValue = valueZ;
            if (m_isQuantumState)
            {
                addForceToQuantum?.Invoke(m_XValue, m_YValue, m_ZValue);
            }
            else
            {
                addForceToClassical?.Invoke(m_XValue, m_YValue, m_ZValue);
            }
        }

        public void onTeleportButton()
        {
            m_entangledBox.SetActive(false);
            m_teleportButton.SetActive(false);
            var transform = m_entangledBox.transform;
            m_quantumBoxScript.SetBodyPosAndRot(new Vector3d(transform.position.x, transform.position.y,transform.position.z), new Vector3d(transform.rotation.x, transform.rotation.y, transform.rotation.z));
            m_quantumBox.transform.position = transform.position;
        }
        
        public void OnEntangleButtonPressed()
        {
            Debug.Log("entangled button pressed!" + m_hasQuantumWallHitTriggered + " " + m_isQuantumState);
            if (m_isQuantumState && m_CurrentQuantumWallTransform != null)
            {
                m_entangledBox.SetActive(true);
                m_teleportButton.SetActive(true);
                /*
                Debug.Log("currentwall transport" + m_CurrentQuantumWallTransform.position);
                m_quantumBoxScript.SetBodyPosAndRot(new Vector3d(m_CurrentQuantumWallTransform.position.x, m_CurrentQuantumWallTransform.position.y, m_CurrentQuantumWallTransform.position.z), m_quantumBoxScript.GetBodyRotation());
                m_quantumBox.transform.position = m_CurrentQuantumWallTransform.position;
                */
            }
        }
        
        private void Update()
        {
            foreach (GameObject wall in m_walls)
            { 
                if (!m_hasClassicalWallHitTriggered && m_classicalBox.activeSelf &&
                    Vector3d.Distance(m_classicalBoxScript.GetBodyPosition(), wall.GetComponent<WallObstacle>().GetBodyPosition()) <= m_triggerDistance)
                {
                    m_hasClassicalWallHitTriggered = true;
                    Debug.Log("Classical object hitted wall!");
                    classicalObjectHittedWall?.Invoke();
                    if (m_tutorialUI1Triggered == false)
                    {
                        m_tutorialUI1Triggered = true;
                        m_tutorialUI1.SetActive(true);
                    }
                }
                else
                {
                    m_hasClassicalWallHitTriggered = false;
                }
                
                if (!m_hasQuantumWallHitTriggered && m_quantumBox.activeSelf && wall.GetComponent<WallObstacle>().isQuantumWall &&
                    Vector3d.Distance(m_quantumBoxScript.GetBodyPosition(), wall.GetComponent<WallObstacle>().GetBodyPosition()) <= m_triggerDistance)
                {
                    m_hasQuantumWallHitTriggered = true;
                    m_CurrentQuantumWallTransform = wall.GetComponent<WallObstacle>().entanglementTransform;
                    Debug.Log("Quantum object hitted wall!");
                    quantumObjectHittedWall?.Invoke();
                    if (m_tutorialUI2Triggered == false)
                    {
                        m_tutorialUI2Triggered = true;
                        m_tutorialUI2.SetActive(true);
                    }
                }
                else
                {
                    m_hasQuantumWallHitTriggered = false;
                }
            }      

        }

        public void ResetQuantumForces()
        {
            quantumObjectHittedWall?.Invoke();
        }

        public void ResetClassicalForces()
        {
            classicalObjectHittedWall?.Invoke();
        }

        public void OpenTutorial3()
        {                    
            if (m_tutorialUI3Triggered == false)
            {
                m_tutorialUI3Triggered = true;
                m_tutorialUI3.SetActive(true);
            }
        }
    }
}