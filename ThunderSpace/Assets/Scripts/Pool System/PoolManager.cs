using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Pool[] playerProjectilePools;

    [SerializeField] Pool[] enemyProjectilePools;


    static Dictionary<GameObject, Pool> dictionary;
    void Start(){
        dictionary = new Dictionary<GameObject, Pool>();
        Initialize(playerProjectilePools);
        Initialize(enemyProjectilePools);
    }

    void OnDestroy() {
        CheckPoolSize(playerProjectilePools);
        CheckPoolSize(enemyProjectilePools);
    }

    void CheckPoolSize(Pool[] pools){
        foreach(var pool in pools){
            if(pool.RuntimeSize > pool.Size){
                Debug.LogWarning(string.Format("Pool runtime size is bigger than init size!"));
            }
        }
    }

    void Initialize(Pool[] pools){
        foreach(var pool in pools){

            if(dictionary.ContainsKey(pool.Prefab)){
                Debug.LogError("Same prefab" + pool.Prefab.name);
                continue;
            }
            dictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("Pool: " + pool.Prefab.name).transform;

            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }

    /// <summary>
    /// <para>return prepared gameobject in pool</para>
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    
    public static GameObject Release(GameObject prefab){

        if(!dictionary.ContainsKey(prefab)){
            Debug.LogError("Didn't find prefab" + prefab.name);
            return null;
        }
        return dictionary[prefab].preparedObject();
    }

    public static GameObject Release(GameObject prefab, Vector3 position){

        if(!dictionary.ContainsKey(prefab)){
            Debug.LogError("Didn't find prefab" + prefab.name);
            return null;
        }
        return dictionary[prefab].preparedObject(position);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation){

        if(!dictionary.ContainsKey(prefab)){
            Debug.LogError("Didn't find prefab" + prefab.name);
            return null;
        }
        return dictionary[prefab].preparedObject(position,rotation);
    }

    public static GameObject Release(GameObject prefab, Vector3 position,Quaternion rotation, Vector3 localScale){

        if(!dictionary.ContainsKey(prefab)){
            Debug.LogError("Didn't find prefab" + prefab.name);
            return null;
        }
        return dictionary[prefab].preparedObject(position,rotation,localScale);
    }

}
