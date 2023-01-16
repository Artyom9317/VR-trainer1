using System;
using System.Collections.Generic;
using UnityEngine;

namespace compaunder
{
    public class CompaunderObjectMover : MonoBehaviour
    {
        [SerializeField] private CompaunderController compaunderController;
        [SerializeField] private List<CompaunderMovableObject> movableObjects;
        [SerializeField] private List<GameObject> augers;
        [SerializeField] private float augerSpeedMultiplier;

        private void FixedUpdate()
        {
            if (compaunderController.toggleState)
            {
                Move();
            }
        }

        private void Move()
        {
            foreach (var obj in movableObjects)
            {
                obj.gameObject.transform.Rotate(
                    new Vector3(obj.rotationAxisState.x, obj.rotationAxisState.y, obj.rotationAxisState.z),
                    obj.rotateSpeed * Time.deltaTime, Space.Self);
            }
            foreach (var auger in augers)
            {
                auger.transform.Rotate(new Vector3(0, 0, 1),
                    compaunderController.getCurrentAugerSpeed() * augerSpeedMultiplier * Time.deltaTime, Space.Self);
            }
        }
    }
}