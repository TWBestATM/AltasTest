using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class altasLoadTest : MonoBehaviour
{
    [SerializeField]
    GameObject loadingTip = null;
    public void loadSceen()
    {
        Addressables.LoadSceneAsync("SampleScene", LoadSceneMode.Single, true);
        loadingTip.SetActive(true);

    }

    public void backMain()
    {
        AltasLoadManager.instance.releaseAltas("test");
        SceneManager.LoadScene("main");
        loadingTip.SetActive(true);
    }
}
