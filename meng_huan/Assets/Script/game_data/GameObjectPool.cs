using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PoolBase
{
    public string PoolName = ""; //缓存池的名字
    public string PrefabPath = ""; //预制体的名字
    public DateTime BeingTime = DateTime.Now; //缓存池生成时间
    public List<GameObject> showObjects = new List<GameObject>(); //显示列表
    public List<GameObject> hideObjects = new List<GameObject>(); //隐藏列表

    public PoolBase(string poolName, string prefabPath, int count, DateTime time) {
        CleanAll();
        PoolName = poolName;
        PrefabPath = prefabPath;
        BeingTime = time;
        //开始创建
        for (int i = 0; i < count; i++)
        {
            GameObject createObj = CreatePrefab(PrefabPath);
            createObj.SetActive(false);
            hideObjects.Add(createObj);
        }
    }

    //从缓存中取出物体
    public GameObject Spawn()
    {
        GameObject o = null;
        if (hideObjects.Count > 0)
        {
            o = hideObjects[0];
            showObjects.Add(o);
            hideObjects.RemoveAt(0);
        }
        else
        {
            o = CreatePrefab(PrefabPath);
            showObjects.Add(o);
        }
        //取出来的时候激活
        o.SetActive(true);
        //断开了父子关系
        o.transform.parent = null;
        return o;
    }

    //创建
    public GameObject CreatePrefab(string o)
    { 
        GameObject objPrefab = (GameObject)Resources.Load(o);
        GameObject obj = UnityEngine.Object.Instantiate(objPrefab);
        obj.name = PoolName;
        return obj;
    }

    //进入缓存（在改进入缓存的调用不要Destroy哦，一般用卸载来进行统一的Destroy）
    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        showObjects.Remove(obj);
        hideObjects.Add(obj);
    }

    public void CleanAll()
    {
        for (int i = 0; i < showObjects.Count; i++)
        {
            UnityEngine.Object.Destroy(showObjects[i]);
        }
        for (int i = 0; i < hideObjects.Count; i++)
        {
            UnityEngine.Object.Destroy(hideObjects[i]);
        }
        showObjects.Clear();
        hideObjects.Clear();
    }
}

public class ObjectPool
{
    //最大未使用并要清理的时间间隔
    public int CheckUnloadTime = 1000 * 60 * 10; //10分钟未使用，直接清除
    //创建一个空字典，里面都是PoolBase
    public Dictionary<string, PoolBase> poolsDict = new Dictionary<string, PoolBase>();
    //单例
    private static ObjectPool _instance;
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPool();
            }
            return _instance;
        }
    }


    //如果没有这个类型的池子就创建一个
    public void createOneTypePool(string poolName, string prefabPath, int count)
    {
        if (!poolsDict.ContainsKey(poolName))
        {
            poolsDict.Add(poolName, new PoolBase(poolName, prefabPath, count, DateTime.Now));
        } else
        {
            Debug.Log("---已存在此缓存池---");
            return;
        }
    }

    //获得缓存池节点
    public GameObject spawnObject(string poolName)
    {
        PoolBase pool = getPoolByPoolName(poolName);
        return pool.Spawn();
    }

    //回收缓存池节点
    public void recycleObject(GameObject obj)
    {
        string name = obj.name;
        PoolBase pool = getPoolByPoolName(name);
        pool.Despawn(obj);
    }
    

    //清空缓存池
    public void clearObjectPool(string poolName)
    {
        PoolBase pool = getPoolByPoolName(poolName);
        pool.CleanAll();
        poolsDict.Remove(poolName);
    }

    //得到池子
    private PoolBase getPoolByPoolName (string poolName)
    {
        PoolBase pool;
        poolsDict.TryGetValue(poolName, out pool);
        return pool;
    }

    //主动调用， 检测时间大于10分钟未被使用的缓存池
    public void CheckPool()
    { //自动清理调用
        double curTime = getTimeMilliseconds(DateTime.Now);
        foreach (KeyValuePair<string, PoolBase> pool in poolsDict)
        {
            //if (curTime - pool.Value.BeingTime > CheckUnloadTime)
            //{
            //    //UnLoadPool(pool.Key);
            //}
        }
    }

    //时间戳
    public double getTimeMilliseconds(DateTime time)
    {
        return time.Subtract(DateTime.Parse("1970-1-1")).TotalMilliseconds;
    }
}


