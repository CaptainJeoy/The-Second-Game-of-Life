using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class ControlSystem : MonoBehaviour
{
    [SerializeField] private GameObject PP_panel, pause, play;

    [SerializeField] private ProceduralMatrixSpawnAndTraverse procedural;

    [SerializeField] private RectTransform upRect, downRect;

    [SerializeField] private AudioMixer mixer;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Slider slider;

    [SerializeField] private float mouseDelaySpan = 5f;

    float delay;
    int counter = 0;
    const string volKey = "NftVolume";

    Vector3 prevMousePos, currMousePos;

    private void Start()
    {
        if (PlayerPrefs.HasKey(volKey))
        {
            slider.value = PlayerPrefs.GetFloat(volKey);
        }

        AdjustVolume(slider.value);
        audioSource.Play();
        delay = mouseDelaySpan;
    }

    private void Update()
    {
        MouseCursor();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (counter % 2 == 0)
            {
                audioSource.Pause();
                procedural.enabled = false;

                PP_panel.SetActive(true);
                pause.SetActive(true);

                play.SetActive(false);
            }
            else
            {
                StopCoroutine(Play());
                StartCoroutine(Play());
            }

            counter++;
        }
    }

    private IEnumerator Play()
    {
        audioSource.Play();
        procedural.enabled = true;

        pause.SetActive(false);
        play.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        PP_panel.SetActive(false);
    }

    private void MouseCursor()
    {
        if (GetScreenCoord(upRect).Contains(Input.mousePosition) || GetScreenCoord(downRect).Contains(Input.mousePosition))
            return;

        currMousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Escape) || (currMousePos - prevMousePos).sqrMagnitude > 0.01f)
        {
            Cursor.visible = true;
            delay = mouseDelaySpan;
        }

        delay -= Time.unscaledDeltaTime;

        if (Input.GetMouseButtonDown(0) || (delay <= 0f && (currMousePos - prevMousePos).sqrMagnitude <= 0.01f))
        {
            Cursor.visible = false;
        }

        prevMousePos = Input.mousePosition;
    }

    private Rect GetScreenCoord(RectTransform uiTrans)
    {
        Vector2 screenRectSize = uiTrans.rect.size * uiTrans.lossyScale;

        Vector2 screenPivotPos = screenRectSize * uiTrans.pivot;

        Vector2 screenRectPos = (Vector2)uiTrans.position - screenPivotPos;

        return new Rect(screenRectPos, screenRectSize);
    }

    public void AdjustVolume(float sliderVal)
    {
        mixer.SetFloat("vol", Mathf.Log10(sliderVal) * 20f);

        PlayerPrefs.SetFloat(volKey, sliderVal);
    }
}
