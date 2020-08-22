using UnityEngine;

public class BounceTween : MonoBehaviour
{
    public float scale = 10, delay = 0.2f;

    private float desival = 1f, currval = 1f, elasped = 0f;

    private void Update()
    {
        currval = LerpFloat(currval, desival, Time.deltaTime * scale);
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

    float LerpFloat(float a, float b, float t)
    {
        return (a + ((b - a) * t));
    }
}
