using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoadManager : MonoBehaviour
{
    public GameObject LoadingCanvas;
    public Image LoadProgressBar;
    public float ScreenHideSpeed;

    public static SceneLoadManager Instance;


    //���� �ε� ��ũ�� ����, ���� �ִϸ��̼��� �߰��ȴٸ� ����� ����
    private LoadingState _loadingState = LoadingState.Wait;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (FindObjectsOfType<SceneLoadManager>().Length >= 2)
        {
            Destroy(gameObject);
        }
        Instance = FindObjectOfType<SceneLoadManager>();
        DontDestroyOnLoad(gameObject);

        _canvasGroup = LoadingCanvas.GetComponent<CanvasGroup>();
    }
    public void ChangeScene(string sceneName)
    {
        _loadingState = LoadingState.InProgress;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(FillProgressBar(asyncLoad));
    }

    IEnumerator FillProgressBar(AsyncOperation asyncLoad)
    {
        //�ε� ȭ�� �ʱ�ȭ
        LoadProgressBar.fillAmount = 0;

        LoadingCanvas.SetActive(true);
        _canvasGroup.alpha = 1.0f;

        //FillAmount
        while (LoadProgressBar.fillAmount <= 1)
        {
            LoadProgressBar.fillAmount = asyncLoad.progress;

            yield return new WaitForSecondsRealtime(0.3f);

            if (LoadProgressBar.fillAmount >= 1)
            {
                break;
            }
        }
        StartCoroutine(HideAlphaLoadingCanvas());
    }

    IEnumerator HideAlphaLoadingCanvas()
    {
        while (_canvasGroup.alpha != 0)
        {
            _canvasGroup.alpha -= 1 / ScreenHideSpeed * Time.deltaTime;
            yield return null;
        }
        LoadingCanvas.SetActive(false);
    }
}
