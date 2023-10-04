using System;
using CycloneUnityTestScenes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityTestScenes.Scripts
{
    public class SuperPositionBoxes : MonoBehaviour
    {
        [SerializeField] private GameObject m_gameObjectA;
        [SerializeField] private GameObject m_gameObjectB;

        [SerializeField] private GameObject[] objectsToCheck;

        [SerializeField] private GameObject m_GameController;
        private GameController gameControllerScript;
        
        private GameObject m_choosenGameObject;
        
        
        private bool m_hasATriggered = false;
        private bool m_hasBTriggered = false;

        private void Awake()
        {
            gameControllerScript = m_GameController.GetComponent<GameController>();
        }

        private void Start()
        {
            var randNum = Random.value;
            if (randNum <= 0.5)
            {
                m_choosenGameObject = m_gameObjectA;
            }
            else
            {
                m_choosenGameObject = m_gameObjectB;
            }
        }

        private void Update()
        {
            foreach (var gameObject in objectsToCheck)
            {
                if (!m_hasATriggered &&
                    Vector3.Distance(gameObject.transform.position, m_gameObjectA.transform.position) <= 25)
                {
                    gameControllerScript.OpenTutorial3();
                }
                
                if (!m_hasATriggered &&Vector3.Distance(gameObject.transform.position, m_gameObjectA.transform.position) <= 4)
                {
                    m_hasATriggered = true;
                    if (gameObject.activeSelf && gameObject.name == "QuantumBox" && m_choosenGameObject == m_gameObjectA)
                    {
                        m_gameObjectA.SetActive(false);
                    }
                    if (gameObject.activeSelf && gameObject.name == "QuantumBox" && m_choosenGameObject == m_gameObjectB)
                    {
                        m_gameObjectB.SetActive(false);
                    }

                    if (gameObject.activeSelf && gameObject.name == "ClassicalBox")
                    {
                        //gameControllerScript.ResetClassicalForces();
                    }
                    gameObject.GetComponent<QuantumBox>().GoBack();
                    this.enabled = false;
                }
                else
                {
                    m_hasATriggered = false;
                }
                
                if (!m_hasBTriggered &&Vector3.Distance(gameObject.transform.position, m_gameObjectB.transform.position) <= 4)
                {
                    
                    m_hasBTriggered = true;
                    if (gameObject.activeSelf && gameObject.name == "QuantumBox" && m_choosenGameObject == m_gameObjectB)
                    {
                        m_gameObjectB.SetActive(false);
                    }
                    if (gameObject.activeSelf && gameObject.name == "QuantumBox" && m_choosenGameObject == m_gameObjectA)
                    {
                        m_gameObjectA.SetActive(false);
                        //gameControllerScript.ResetQuantumForces();
                    }

                    if (gameObject.activeSelf && gameObject.name == "ClassicalBox")
                    {
                        //gameControllerScript.ResetClassicalForces();
                    }
                    gameObject.GetComponent<QuantumBox>().GoBack();
                    this.enabled = false;
                }
                else
                {
                    m_hasBTriggered = false;
                }
            }
        }
    }
}