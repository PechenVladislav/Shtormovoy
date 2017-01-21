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

        public ParticleSystem particles;

        public void breakPlatform()
        {
            particles.Play();
            Destroy(gameObject, 1f);
        }
    }
}
