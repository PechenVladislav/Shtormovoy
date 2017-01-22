using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Floor : MonoBehaviour
    {
        public const string Tag = "Floor";

        public ParticleSystem breakParticles;

        public ParticleSystem missParticles;

        public void breakPlatform()
        {
            breakParticles.Play();
            Destroy(gameObject, 1f);
        }

        public void missPlatform()
        {
            missParticles.Play();
        }
    }
}
