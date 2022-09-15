using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CompanyName.FrameWork
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private Camera mainCamera;

        private Ray ray;
        private RaycastHit hit;

        public override void Init()
        {
        }

        public override void Release()
        {
        }

        // 개선 필요 ver 0.1;
        public RaycastHit hitRayCast()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit;
            }

            return hit;
        }

    }
}


