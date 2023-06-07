using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    bool cursor;
    public bool isGameOver = false;
    private bool isAllive = true;

    private Image fadeImage;
    [SerializeField]
    private GameObject GameOverUI_Pref;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (!Instance.Equals(this))
            {
                Destroy(gameObject);
            }
            return;
        }
    }
    private void Start()
    {
        CursorOff();
    }
    private void Update()
    {
        if(isGameOver && isAllive)
        {
            GameOver();
        }
        if(Input.GetKey(KeyCode.F11))
        {
            Time.timeScale = 10f;
        }
        else if(Input.GetKeyUp(KeyCode.F11))
        {
            Time.timeScale = 1f;
        }
    }

    private void GameOver()
    {
        isAllive = false;
        GameObject gameOverUI = Instantiate(GameOverUI_Pref);
        fadeImage = gameOverUI.GetComponentInChildren<Image>();
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        for (float alpha = 0f; alpha <= 0.8f; alpha += Time.deltaTime)
        {
            Color newColor = fadeImage.color;
            newColor.a = alpha;
            fadeImage.color = newColor;
            yield return null;
        }
        yield return new WaitForSeconds(2f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (cursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void CursorOn()
    {
        cursor = true;
    }
    public void CursorOff()
    {
        cursor = false;
    }
}
