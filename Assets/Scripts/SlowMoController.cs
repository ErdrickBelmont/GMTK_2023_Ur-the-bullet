using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlowMoController : MonoBehaviour
{
    private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, -1, 0, -1);
    public float transitionTime = 1;
    public float startMultiplier = 1;
    public float endMultiplier = 0.25f;
    private float currentTime = 0;
    [HideInInspector]
    public float currentMultiplier = 1;
    public void slowDown()
    {
        if (animCurve[0].value == startMultiplier) { return; }
        StopAllCoroutines();
        animCurve = AnimationCurve.EaseInOut(0, startMultiplier, transitionTime, endMultiplier);
        if (currentTime != 0) currentTime = Mathf.Abs(currentTime - transitionTime);
        StartCoroutine(changeMultiplier());
    }
    public void speedUp()
    {
        if (animCurve[0].value == endMultiplier) { return; }
        StopAllCoroutines();
        animCurve = AnimationCurve.EaseInOut(0, endMultiplier, transitionTime, startMultiplier);
        if (currentTime != 0) currentTime = Mathf.Abs(currentTime - transitionTime);
        StartCoroutine(changeMultiplier());
    }
    private IEnumerator changeMultiplier()
    {
        currentTime += Time.deltaTime;
        currentMultiplier = animCurve.Evaluate(currentTime);
        while (animCurve[1].value != currentMultiplier)
        {
            yield return null;
            currentTime += Time.deltaTime;
            currentMultiplier = animCurve.Evaluate(currentTime);
        }
        currentTime = 0;
    }
}
