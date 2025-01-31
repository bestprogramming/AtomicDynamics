﻿#if NO_COLLISION_DR
using static AtomicDynamics.Physics;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Simulation(Big dr)
    {
        public List<Particle> Particles = [];

        private Big maxV = Zero;
        private Big dt = PlankTime;

        private Interaction[]? interactions;
        public Interaction[] Interactions
        {
            get
            {
                interactions ??= Particle.GetInteractions(Particles).ToArray();
                return interactions;
            }
        }

        public void SetFs()
        {
            foreach (var i in Interactions)
            {
                var f = Elementary.GetF(i.Elementary1, i.Elementary2);
                i.Elementary1.F += f;
                i.Elementary2.F -= f;
            }
        }

        public void SetVs()
        {
            SetFs();

            foreach (var p in Particles)
            {
                p.SetV(dt);
                if (p.V.Length > maxV) maxV = p.V.Length;
            }
        }

        public void Next()
        {
            foreach (var p in Particles)
            {
                p.Next(dt);
            }

            SetVs();

            if (maxV != Zero) dt = dr / maxV;
            maxV = Zero;
        }
    }
}

#endif
