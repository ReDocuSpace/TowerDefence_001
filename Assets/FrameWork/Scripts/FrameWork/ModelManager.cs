using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum OBJ_POOL
{
    TestCircle001,
    TestCircle002,
    TestCircle003,
    TestCircle004,
    TestCircle005,
    Enemy001
}

public enum FX_POOL
{

}

public enum FX_E_POOL
{

}

namespace CompanyName.FrameWork
{
    using CompanyName.Function;

    public class ModelManager : MonoSingleton<ModelManager>
    {
        private Coroutine corloadResource = null;

        [HideInInspector] public bool loadComplete = false;
        [HideInInspector] public Dictionary<OBJ_POOL, ObjectPool> objPool = new Dictionary<OBJ_POOL, ObjectPool>();
        [HideInInspector] public Dictionary<FX_POOL, ObjectPool> fxPool = new Dictionary<FX_POOL, ObjectPool>();
        [HideInInspector] public Dictionary<FX_E_POOL, ObjectPool> fxEnvironmentPool = new Dictionary<FX_E_POOL, ObjectPool>();

        public override void Init()
        {
            if (corloadResource == null)
                corloadResource = StartCoroutine(cLoadResource());
        }

        public override void Release()
        {
            if (corloadResource != null) StopCoroutine(corloadResource);
            corloadResource = null;

            // Object 해제 이후 삭제
            foreach (ObjectPool obj in objPool.Values)
            {
                obj.DestroyPool();
            }

            foreach (ObjectPool obj in fxPool.Values)
            {
                obj.DestroyPool();
            }

            foreach (ObjectPool obj in fxEnvironmentPool.Values)
            {
                obj.DestroyPool();
            }

            objPool.Clear();
            fxPool.Clear();
            fxEnvironmentPool.Clear();

            Resources.UnloadUnusedAssets();
        }


        private IEnumerator cLoadResource()
        {
            ResourceRequest asset;
            GameObject obj;

            for (int i = 0; i < System.Enum.GetValues(typeof(OBJ_POOL)).Length; i++)
            {
                yield return asset = Resources.LoadAsync<GameObject>("Object/" + ((OBJ_POOL)i).ToString());
                obj = asset.asset as GameObject;

                GameObject poolObject = new GameObject();

                poolObject.name = obj.gameObject.name + "Pool";
                poolObject.transform.SetParent(transform);
                poolObject.transform.localScale = Vector3.one;
                poolObject.transform.localPosition = Vector3.zero;

                ObjectPool _pool = poolObject.AddComponent<ObjectPool>();
                _pool.InitPool(obj.GetComponent<IObject>(), transform);

                objPool.Add((OBJ_POOL)i, _pool);

            }

            for (int i = 0; i < System.Enum.GetValues(typeof(FX_POOL)).Length; i++)
            {
                yield return asset = Resources.LoadAsync<GameObject>("FXObject/" + ((FX_POOL)i).ToString());
                obj = asset.asset as GameObject;

                GameObject poolObject = new GameObject();

                poolObject.name = obj.gameObject.name + "Pool";
                poolObject.transform.SetParent(transform);
                poolObject.transform.localScale = Vector3.one;
                poolObject.transform.localPosition = Vector3.zero;

                ObjectPool _pool = poolObject.AddComponent<ObjectPool>();
                _pool.InitPool(obj.GetComponent<FXObject>(), transform);

                fxPool.Add((FX_POOL)i, _pool);
            }

            loadComplete = true;
        }
    }


}
