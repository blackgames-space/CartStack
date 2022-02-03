using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNHide : MonoBehaviour
{
    [SerializeField]
    float _secsToAppear = 0.5f;
    [SerializeField]
    float _secsToDisappear = 0.5f;

    [SerializeField]
    bool _hidingOut = false;

    public void Show()
    {
        StartCoroutine(Showing());
    }

    IEnumerator Showing()
    {
        var time = 0f;

        while (time < _secsToAppear)
        {
            time += Time.deltaTime;

            var progress = Mathf.Clamp01(time / _secsToAppear);

            transform.localScale = new Vector3(progress, progress, progress);

            yield return null;
        }

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Hide()
    {
        if(_hidingOut) StartCoroutine(HidingOut());
        else StartCoroutine(HidingIn());
    }

    IEnumerator HidingOut()
    {
        var time = 0f;

        while (time < _secsToDisappear)
        {
            time += Time.deltaTime;

            var progress = Mathf.Clamp01(time / _secsToDisappear);

            transform.localScale = new Vector3(1 + progress * 2, 1 + progress * 2, 1 + progress * 2);

            yield return null;
        }

        transform.localScale = new Vector3(3f, 3f, 3f);
    }

    IEnumerator HidingIn()
    {
        var time = 0f;

        while (time < _secsToDisappear)
        {
            time += Time.deltaTime;

            var progress = Mathf.Clamp01(time / _secsToDisappear);

            transform.localScale = new Vector3(1 - progress, 1 - progress, 1 - progress);

            yield return null;
        }

        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public float GetSecsToAppear()
    {
        return _secsToAppear;
    }

    public void SetSecsToDisappear(float time)
    {
        _secsToDisappear = time;
    }
}
