using Cyclone.Core;

namespace Cyclone.Rigid.Forces
{
    public class RigidBodyForce : RigidForce
    {
        private Vector3d m_force;
        private RigidBody m_body;

        public RigidBodyForce(RigidBody body, Vector3d force)
        {
            m_body = body;
            m_force = force;
        }

        public override void UpdateForce(double dt)
        {
            UpdateForce();
        }

        private void UpdateForce()
        {
            if (m_body.HasInfiniteMass) return;

            m_body.AddForce(m_force * m_body.GetMass());
        }
        
        
    }
}