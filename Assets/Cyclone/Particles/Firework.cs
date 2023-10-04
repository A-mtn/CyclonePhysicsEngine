using Cyclone.Core;

namespace Cyclone.Particles
{/*
public class Firework : Particle
    {
        public uint type;
        public double age;

        public bool Update(double duration)
        {
            // Update our physical state
            Integrate(duration);

            // We work backwards from our age to zero.
            age -= duration;
            return (age < 0) || (position.y < 0);
        }
    }

    public struct FireworkRule
    {
        public uint type;
        public double minAge;
        public double maxAge;
        public Vector3d minVelocity;
        public Vector3d maxVelocity;
        public double damping;

        public struct Payload
        {
            public uint type;
            public uint count;

            public void Set(uint type, uint count)
            {
                this.type = type;
                this.count = count;
            }
        }

        public uint payloadCount;
        public Payload[] payloads;

        public void Init(uint payloadCount)
        {
            this.payloadCount = payloadCount;
            payloads = new Payload[payloadCount];
        }

        public void SetParameters(uint type, double minAge, double maxAge,
            Vector3d minVelocity, Vector3d maxVelocity, double damping)
        {
            this.type = type;
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.minVelocity = minVelocity;
            this.maxVelocity = maxVelocity;
            this.damping = damping;
        }

        public void Create(Firework firework, Firework parent = null)
        {
            firework.type = type;
            firework.age = Random.Crandom.RandomReal(minAge, maxAge);

            Vector3d vel = Vector3d.Zero;
            if (parent != null)
            {
                // The position and velocity are based on the parent.
                firework.Position(parent.Position());
                vel += parent.Velocity();
            }
            else
            {
                Vector3d start = Vector3d.Zero;
                int x = Random.Crandom.RandomInt(3) - 1;
                start.X = 5.0f * x;
                firework.SetPosition(start);
            }

            vel += Random.Crandom.RandomVector(minVelocity, maxVelocity);
            firework.SetVelocity(vel);

            // We use a mass of one in all cases (no point having fireworks
            // with different masses, since they are only under the influence
            // of gravity).
            firework.SetMass(1);

            firework.SetDamping(damping);

            firework.SetAcceleration(Vector3.Gravity);

            firework.ClearAccumulator();
        }
    }*/
}