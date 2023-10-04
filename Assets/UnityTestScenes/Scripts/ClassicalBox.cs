using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;
using Cyclone.Rigid.Forces;
using UnityEngine.UI;
using UnityTestScenes.Scripts;

namespace CycloneUnityTestScenes
{
    public class ClassicalBox : MonoBehaviour
    {
        public double mass = 1;

        public double damping = 0.9;

        public double gravity = -9.81;
        
        private RigidBody m_body;
        
        [SerializeField] private GameObject m_GameController;

        private float x, y, z;

        private void Awake()
        {
            m_GameController.GetComponent<GameController>().addForceToClassical += AddForce;
            m_GameController.GetComponent<GameController>().classicalObjectHittedWall += ClearAllForces;
            m_GameController.GetComponent<GameController>().clearAllForces += ClearAllForces;
        }
        
        void Start()
        {
            var pos = transform.position.ToVector3d();
            var scale = transform.localScale.ToVector3d() * 0.5;
            var rot = transform.rotation.ToQuaternion();

            m_body = new RigidBody();
            m_body.Position = pos;
            m_body.Orientation = rot;
            m_body.LinearDamping = damping;
            m_body.AngularDamping = damping;
            m_body.Name = gameObject.name;
            //m_body.Velocity = new Vector3d(1, 1, 0);
            m_body.SetMass(mass);
            m_body.SetAwake(false);
            m_body.SetCanSleep(false);

            var shape = new CollisionBox(scale);
            shape.Body = m_body;

            RigidPhysicsEngine.Instance.Bodies.Add(m_body);
            RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
        }
        
        private void Update()
        {
            transform.position = m_body.Position.ToVector3();
            transform.rotation = m_body.Orientation.ToQuaternion();

            //m_body.Velocity += new Vector3d(x, y, z);
        }
        
        public void AddForce(int valueX, int valueY, int valueZ)
        {
            RigidPhysicsEngine.Instance.Forces.Add(new RigidBodyForce(m_body, 
            new Vector3d(valueX, valueY, valueZ)));
        }
        
        public void ClearAllForces()
        {
            RigidPhysicsEngine.Instance.Forces.Clear();
            m_body.LastFrameAcceleration = Vector3d.Zero;
            m_body.Velocity = Vector3d.Zero;
        }

        public Vector3d GetBodyPosition()
        {
            return m_body.Position;
        }
        public Vector3d GetBodyRotation()
        {
            return m_body.Rotation;
        }
        
        public void SetBodyPosAndRot(Vector3d pos, Vector3d rot)
        {
            m_body.Position = pos;
            m_body.Rotation = rot;
        }
        
        public void GoBack()
        {
            m_body.Velocity = new Vector3d(0, 0, -3f);
            ClearAllForces();
        }
    }
}