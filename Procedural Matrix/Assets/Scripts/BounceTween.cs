using UnityEngine;

public class BounceTween : MonoBehaviour
{
    public float scale = 10, delay = 0.2f;

    [SerializeField]
    private bool isColourStay = false;

    private float desival = 1f, currval = 1f, elasped = 0f;

    private SpriteRenderer sp;

    private Color defaultColour;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        defaultColour = sp.color;
    }

    private void Update()
    {
        currval = LerpFloat(currval, desival, Time.deltaTime * scale);
        transform.localScale = Vector3.one * currval;

        elasped -= Time.deltaTime;

        if (elasped <= 0f)
        {
            desival = 1;
        }

    }
    public void BounceSquare(bool isClearPaint)
    {
        desival = 0.5f;
        elasped = delay;

        if (isClearPaint)
        {
            sp.color = defaultColour;
        }
        else
        {
            sp.color = (3 > Random.Range(0, 10)) ? defaultColour : randomColour();
        }
    }

    float LerpFloat(float a, float b, float t)
    {
        return (a + ((b - a) * t));
    }

    Color randomColour()
    {
        //return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        return Color.black;
    }
}
