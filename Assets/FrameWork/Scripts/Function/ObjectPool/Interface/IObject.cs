using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName.Function
{
    public abstract class IObject : MonoBehaviour
    {
        protected ObjectPool _parentPool;
        /*
         ObjectPool ���� �ҷ��ö�
         Enter : ����
         Exit : ����
         Init : �ҷ��ö����� �ʱ�ȭ
         Disabled : ��Ȱ��ȭ
         */

        public virtual void ConnectPool(ObjectPool pool)
        {
            _parentPool = pool;

        }
        public virtual void PoolObject()
        {
            _parentPool.PoolObject(this);
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnInit();
        public abstract void OnDisabled();
    }
}