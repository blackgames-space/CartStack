using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField]
    Image _fade;

    [SerializeField]
    float _targetAlpha = 0.8f;

    [SerializeField]
    float _secsToFadeIn = 0.3f;
    [SerializeField]
    float _secsToFadeOut = 0.3f;

    [SerializeField]
    bool _isForTransition = false;

    private void Start()
    {
        if (_isForTransition == true)
        {
            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _targetAlpha);
            FadeOut();

            LevelManager.Instance.GameEndAction += FadeIn;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        var time = 0f;

        while (time < _secsToFadeIn)
        {
            time += Time.deltaTime;

            var alphaNew = (time / _secsToFadeIn) * _targetAlpha;

            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, alphaNew);

            yield return null;
        }

        _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, _targetAlpha);
    }

    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        var time = 0f;

        while (time < _secsToFadeOut)
        {
            time += Time.deltaTime;

            var alphaNew = (time / _secsToFadeIn) * _targetAlpha;

            _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 1 - alphaNew);

            yield return null;
        }

        _fade.color = new Color(_fade.color.r, _fade.color.g, _fade.color.b, 0f);
    }

    public void FullCycleFade()
    {
        StartCoroutine(FullCyclingFade());
    }

    IEnumerator FullCyclingFade()
    {
        StartCoroutine(FadingIn());
        yield return new WaitForSeconds(_secsToFadeIn);
        StartCoroutine(FadingOut());
    }

    public float GetSecsToFadeIn()
    {
        return _secsToFadeIn;
    }
}
