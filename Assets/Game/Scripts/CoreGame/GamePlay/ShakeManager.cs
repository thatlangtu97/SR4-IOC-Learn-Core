using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class ShakeManager : MonoBehaviour
{
    public static ShakeManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    IEnumerator delay( Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    } 
    IEnumerator ShakeEnumerator(Transform transformShake , float duration, float magnitude)
    {
        Vector3 orignalPosition = Vector3.zero;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transformShake.localPosition.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transformShake.localPosition = orignalPosition;
    }

    public void Shake(Transform transformShake, float duration, float magnitude)
    {
        StartCoroutine(ShakeEnumerator(transformShake, duration, magnitude));
    }
}
