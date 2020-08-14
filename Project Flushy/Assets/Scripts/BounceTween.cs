using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTween : MonoBehaviour
{
    public float scale = 10, delay = 0.2f;

    float desival = 1f, currval = 1f, elasped = 0f;
    private void Update()
    {
        currval = Mathf.Lerp(currval, desival, Time.deltaTime * scale);
        transform.localScale = Vector3.one * currval;

        elasped -= Time.deltaTime;

        if (elasped <= 0f)
            desival = 1;
    }
    public void BounceSquare()
    {
        desival = 0.5f;
        elasped = delay;
    }
}
