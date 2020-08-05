using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class AltasLoadManager : MonoBehaviour
{
    public static AltasLoadManager instance;
    Dictionary<string, AsyncOperationHandle<SpriteAtlas>> atlasLoadedDic = new Dictionary<string, AsyncOperationHandle<SpriteAtlas>>();
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;

        }
        instance = this;
        SpriteAtlasManager.atlasRequested+= RequestAtlas;

        DontDestroyOnLoad(gameObject);
    }

    void RequestAtlas(string tag, System.Action<UnityEngine.U2D.SpriteAtlas> callback)
    {
        Debug.Log("load" + tag);
        AsyncOperationHandle<SpriteAtlas> async = Addressables.LoadAssetAsync<SpriteAtlas>(tag);
        async.Completed += (ob) => { callback(ob.Result); };
        if (!atlasLoadedDic.ContainsKey(tag))
        {
            atlasLoadedDic.Add(tag, async);
        }
    }
    public void releaseAltas(string tag)
    {
        if (atlasLoadedDic.ContainsKey(tag))
        {
            Addressables.Release(atlasLoadedDic[tag]);
            Debug.Log("release" + tag);
            atlasLoadedDic.Remove(tag);
        }


    }
    protected void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            SpriteAtlasManager.atlasRequested -= RequestAtlas;
            Debug.Log("destory");
        }
    }
}
