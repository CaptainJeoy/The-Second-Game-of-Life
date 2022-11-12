using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RevealImagesOnMouseHover : MonoBehaviour
{
	[SerializeField] private RectTransform parentPanel;

    [SerializeField] private Image[] images;

	[SerializeField] private float delaySpan = 1f, scale = 2f;

    [SerializeField] private TextMeshProUGUI text;

	private float delayIn, delayOut;

    bool isFirstTime = false;

    string firstTimeKey = "Iwint";

    private void Start()
    {
        firstTimeKey += GetInstanceID().ToString();

        delayIn = delayOut = delaySpan;

        if (!PlayerPrefs.HasKey(firstTimeKey))
        {
            isFirstTime = true;

            Invoke("timeUp", 2f);

            PlayerPrefs.SetFloat(firstTimeKey, 0f);
        }

        if (!isFirstTime)
        {
            foreach (Image item in images)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 0f);
            }

            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
    }

    private void timeUp()
    {
        isFirstTime = false;
    }

    private Rect GetScreenCoord(RectTransform uiTrans)
	{
		Vector2 screenRectSize = uiTrans.rect.size * uiTrans.lossyScale;

		Vector2 screenPivotPos = screenRectSize * uiTrans.pivot;

		Vector2 screenRectPos = (Vector2)uiTrans.position - screenPivotPos;

		return new Rect(screenRectPos, screenRectSize);
	}

    private void Update()
    {
        if (isFirstTime)
            return;

        if (GetScreenCoord(parentPanel).Contains(Input.mousePosition))
        {
            if (images[images.Length - 1].color.a >= 1f)
            {
                delayIn = delaySpan;
                return;
            }
          
            delayIn -= (Time.unscaledDeltaTime * scale);

            foreach (Image item in images)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, delaySpan - delayIn);
            }

            text.color = new Color(text.color.r, text.color.g, text.color.b, delaySpan - delayIn);
        }
        else
        {
            if (images[images.Length - 1].color.a <= 0f)
            {
                delayOut = delaySpan;
                return;
            }
            
            delayOut -= (Time.unscaledDeltaTime * scale);

            foreach (Image item in images)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, delayOut);
            }

            text.color = new Color(text.color.r, text.color.g, text.color.b, delayOut);
        }
    }
}
