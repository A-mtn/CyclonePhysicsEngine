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
    public class WallObstacle : MonoBehaviour
    {
        public double mass = -1;

        public double damping = 0;
        public bool isQuantumWall = false;
        public Transform entanglementTransform;
        

        private RigidBody m_body;

        void Start()
        {
            var pos = transform.position.ToVector3d();
            var scale = transform.localScale.ToVector3d() * 0.5;
            var rot = transform.rotation.ToQuaternion();

            m_body = new RigidBody();
            m_body.Position = pos;
            m_body.Orientation = rot;
            m_body.LinearDamping = 0;
            m_body.AngularDamping = 0;
            m_body.Name = gameObject.name;
            m_body.SetMass(-1);
            m_body.SetAwake(false);
            m_body.SetCanSleep(false);
            m_body.SetIsWall(true);

            var shape = new CollisionBox(scale);
            shape.Body = m_body;

            RigidPhysicsEngine.Instance.Bodies.Add(m_body);
            RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
        }
        
        
        private void Update()
        {
            transform.position = m_body.Position.ToVector3();
            transform.rotation = m_body.Orientation.ToQuaternion();
        }

        public Vector3d GetBodyPosition()
        {
            return m_body.Position;
        }
        public Vector3d GetBodyRotation()
        {
            return m_body.Rotation;
        }
    }
}