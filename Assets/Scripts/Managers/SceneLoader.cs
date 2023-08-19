using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField]
    private Image _fadeImage;
    [SerializeField]
    private float _fadeSpeed;

    private Color _tempColor;
    private float _fadeAmount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        // Fader is transparent
        _tempColor = _fadeImage.color;
        _tempColor.a = 0;
        _fadeImage.color = _tempColor;

        _fadeImage.gameObject.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        _fadeImage.gameObject.SetActive(true);
        StartCoroutine(LoadSceneRoutine(scene));
    }

    private IEnumerator LoadSceneRoutine(string scene)
    {
        _tempColor = _fadeImage.color;

        while(_fadeImage.color.a < 1)
        {
            _fadeAmount = _tempColor.a + (_fadeSpeed * Time.deltaTime);
            _tempColor.a = _fadeAmount;

            if(_tempColor.a > 1)
            {
                _tempColor.a = 1;
            }
            _fadeImage.color = _tempColor;
            yield return null;
        }

        SceneManager.LoadScene(scene);

        while (_fadeImage.color.a > 0)
        {
            _fadeAmount = _tempColor.a - (_fadeSpeed * Time.deltaTime);
            _tempColor.a = _fadeAmount;

            if (_tempColor.a < 0)
            {
                _tempColor.a = 0;
            }
            _fadeImage.color = _tempColor;
            yield return null;
        }

        _fadeImage.gameObject.SetActive(false);
    }
}