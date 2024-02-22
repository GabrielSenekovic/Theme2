using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader instance;
    AsyncOperationHandle<SceneInstance> handle;

    [SerializeField] string currentLevelKey;

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
    public void Reload()
    {
        StartCoroutine(ReloadSceneAsync());
    }
    static IEnumerator ReloadSceneAsync()
    {
        yield return new WaitForSeconds(1.0f);
        Instance?.OnReload();
    }
    public async void OnReload()
    {
        DontDestroyOnLoad(this);
        if (handle.IsValid())
        {
            await Addressables.UnloadSceneAsync(handle).Task;
        }
        handle = Addressables.LoadSceneAsync(currentLevelKey);
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
}
