using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneManager : MonoBehaviour
{
    public GameObject LoadingCanvas;

    public Image LoadProgressBar;

    public static LoadingSceneManager Instance;
    
    //���� �ε� ��ũ�� ����, ���� �ִϸ��̼��� �߰��ȴٸ� ����� ����
    private LoadingState _loadingState = LoadingState.Wait;

    private void Awake()
    {
        if (FindObjectsOfType<LoadingSceneManager>().Length >= 2)
        {
            Destroy(gameObject);
        }

        Instance = FindObjectOfType<LoadingSceneManager>();
        DontDestroyOnLoad(gameObject);
    }
    public void ChangeScene(string sceneName)
    {
        _loadingState = LoadingState.InProgress;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FillProgressBar(asyncLoad));
    }

    IEnumerator FillProgressBar(AsyncOperation asyncLoad)
    {
        //�ε� ������ �ʱ�ȭ
        LoadProgressBar.fillAmount = 0;
        LoadingCanvas.SetActive(true);

        //FillAmount
        while (LoadProgressBar.fillAmount <= 1)
        {
            LoadProgressBar.fillAmount = asyncLoad.progress;

            yield return new WaitForSecondsRealtime(0.1f);

            if (LoadProgressBar.fillAmount >= 1)
            {
                break;
            }
        }
        LoadingCanvas.SetActive(false);
    }
}
