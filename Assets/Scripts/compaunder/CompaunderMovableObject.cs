using System;
using UnityEngine;
using util;

namespace compaunder
{
    [Serializable]
    public class CompaunderMovableObject
    {
        public GameObject gameObject;
        public AxisState rotationAxisState;
        public float rotateSpeed;
    }
}