using System.Collections;
using UnityEngine;

public class UI : MonoBehaviour
{
    public CanvasGroup LoadingCanvas;
    public CanvasGroup HUDCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Show(CanvasGroup canvas)
    {
        canvas.gameObject.SetActive(true);
        canvas.alpha = 1f;
    }

    public void Hide()
    {
        Debug.Log($"{gameObject.name} is active : {gameObject.activeInHierarchy}");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while(LoadingCanvas.alpha > 0f)
        {
            LoadingCanvas.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
        }

        LoadingCanvas.gameObject.SetActive(false);
    }
}
