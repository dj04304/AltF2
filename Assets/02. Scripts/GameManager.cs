using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    #region Singleton Setting 
    private static GameManager _instanace;
    public static GameManager Instance
    { 
        get
        {
            _instanace = GameObject.Find("@GameManager").GetComponent<GameManager>();

            if(_instanace == null)
            {
                _instanace = new GameManager();
                var go = new GameObject("@GameManager");
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(_instanace);
            }
            else
            {
                DontDestroyOnLoad(_instanace);
            }
            return _instanace;
        }
    }

    private readonly UIManager _ui = new UIManager();
    public static UIManager UI => Instance._ui;

    private GameManager()
    {

    }
    #endregion

    [field: SerializeField] public int DeathCount { get; private set; } = 0;

    private UIScore _scoreUI;
    public bool IsPlayerDied { get; private set; } = false;

    private Transform _respawnPosition;

    private Transform _startPosition;

    private void Awake()
    {
        _ui.Init();
    }

    private void Start()
    {
        // OnPlayerDied();
    }

    private void Update()
    {
        _scoreUI?.SetPlayTime(Time.time);

        UITestInput();
    }

    private void UITestInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _ui.ShowPoppUI(EPopup.UIPauseGame);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        _scoreUI = GameObject.Find("HUD").GetComponent<UIGameScene>().Score;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    private void OnPlayerDied()
    {
        IsPlayerDied = true;
        ++DeathCount;
        _ui.ShowPoppUI(EPopup.UIDeath);

        Invoke("RespawnPlayer", 1.0f);
    }

    private void RespawnPlayer()
    {
        _ui.ClosePopupUI();
        IsPlayerDied = false;

        // var respawnPosition = respawnPosition == null ? _startPosition : _respawnPosition;

        // Resources.Load<GameObject>("Player")
    }
}
