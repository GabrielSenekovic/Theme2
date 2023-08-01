using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader instance;
    string currentLevelKey;
    AsyncOperationHandle<SceneInstance> handle;

    public static SceneLoader Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public async void Load(string key)
    {
        DontDestroyOnLoad(this);
        if(handle.IsValid())
        {
            await Addressables.UnloadSceneAsync(handle).Task;
        }
        handle = Addressables.LoadSceneAsync(key); //Scenes also have to be unloaded later
        currentLevelKey = key;
    }
    public async void Reload()
    {
        DontDestroyOnLoad(this);
        if (handle.IsValid())
        {
            await Addressables.UnloadSceneAsync(handle).Task;
        }
        handle = Addressables.LoadSceneAsync(currentLevelKey);
    }
}
