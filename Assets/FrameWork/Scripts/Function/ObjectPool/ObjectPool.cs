using System.Collections.Generic;
using UnityEngine;

/*
    GetObject()     : Object ���� (Pool���� Object ��������)
    PoolObject      : Object ���� (Pool�� Object ����)
 */

namespace CompanyName.Function
{
    public class ObjectPool : MonoBehaviour
    {
        // Field ����������������������������������������������������������������������������������������������������������������������
        // Pool Queue�� ����
        private Queue<IObject> _pool = new Queue<IObject>();

        /* Pool ��� ����ȭ�� ���� Component ���
            origin : �ڽ��� ����ִ� IObject ����
        */
        [SerializeField] private IObject origin;

        private List<IObject> useObjectList = new List<IObject>();

        // Method ����������������������������������������������������������������������������������������������������������������������
        private IObject InstantiatePoolObject()
        {
            IObject newObj = GameObject.Instantiate<IObject>(origin);

            newObj.OnEnter();
            newObj.ConnectPool(this);

            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(transform);

            useObjectList.Add(newObj);

            return newObj;
        }

        public void InitPool(IObject prefab, Transform parent = null, int count = 1)
        {
            origin = prefab;

            for (int i = 0; i < count; i++)
            {
                _pool.Enqueue(InstantiatePoolObject());
            }
        }

        public void DestroyPool()
        {
            while (_pool.Count != 0)
            {
                IObject obj = _pool.Dequeue();
                obj.OnDisabled();
                obj.OnExit();
                Destroy(obj);
            }
            Destroy(this);
        }

        public IObject GetObject(Transform startTrans, Transform lookAtTrans = null, Transform parent = null)
        {
            IObject obj = null;

            if (_pool.Count == 0)
            {
                obj = InstantiatePoolObject();
            }
            else
            {
                obj = _pool.Dequeue();
            }

            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parent);

            obj.transform.position = startTrans.position;

            if (lookAtTrans != null)
            {
                obj.transform.LookAt(lookAtTrans);
            }


            obj.OnInit();

            return obj;
        }

        public IObject GetObject(Transform parent = null, bool sameOrigin = false)
        {
            IObject obj = null;

            if (_pool.Count == 0)
            {
                obj = InstantiatePoolObject();
            }
            else
            {
                obj = _pool.Dequeue();
            }

            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parent);

            if (sameOrigin)
            {
                obj.transform.localPosition = origin.transform.localPosition;
                obj.transform.localScale = origin.transform.localScale;
            }
            else
            {
                obj.transform.localPosition = Vector3.zero;
            }

            obj.OnInit();

            return obj;
        }

        public void PoolObject(IObject obj)
        {
            if (origin.gameObject.name + "(Clone)" == obj.gameObject.name)
            {
                obj.OnDisabled();
                obj.transform.SetParent(transform);
                obj.gameObject.SetActive(false);

                _pool.Enqueue(obj);
            }
            else
            {
                Debug.Log("Pooling error : " + obj.name);
            }
        }

        public void PoolObject()
        {
            foreach (IObject obj in useObjectList)
            {
                if (obj.gameObject.activeSelf)
                {
                    obj.OnDisabled();
                    obj.transform.SetParent(transform);
                    obj.gameObject.SetActive(false);

                    _pool.Enqueue(obj);
                }
            }
        }
    }


}

