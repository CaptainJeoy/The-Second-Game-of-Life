using UnityEngine;
using UnityEngine.UI;

public class GenerateAndLoadTiles : MonoBehaviour
{
    public enum LoadStyle
    {
        Straight,
        Diagonal
    }

    public BounceTween tweenObj;

    public LoadStyle loadStyle = LoadStyle.Diagonal;

    public Slider loadSpeedSlider;

    public int Size = 5;

    public float defaultDelaySpan = 0.06f;

    private BounceTween[,] squares;

    float delay;

    int i = 0, j = 0, counter = 0, counterMod = 0, secCounter = -1;

    bool isFirstPartDone = false;

    private void Awake()
    {
        squares = new BounceTween[Size, Size];
        loadSpeedSlider.value = defaultDelaySpan;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                BounceTween bt = Instantiate(tweenObj);
                bt.transform.parent = this.transform;
                bt.transform.position = new Vector3(j - (Size / 2), i - (Size / 2), 0f);

                squares[i, j] = bt;
            }
        }
    }

    private void Update()
    {
        switch (loadStyle)
        {
            case LoadStyle.Straight:
                Straight();
                break;
            case LoadStyle.Diagonal:
                DiagonalLoad();
                break;
        }
    }

    void Straight()
    {
        while (counter < (Size * Size) && Time.time > delay)
        {
            i = counter / Size;
            j = counter % Size;

            squares[i, j].BounceSquare();

            delay = Time.time + loadSpeedSlider.value;
            counter++;

            if (counter == (Size * Size))
                counter = 0;
        }
    }

    void DiagonalLoad()
    {
        while (counter < Size + Size && Time.time > delay)
        {
            counterMod = counter % Size;

            if (!isFirstPartDone)
            {
                i = counterMod;
                j = 0;

                while (i >= 0)
                {
                    squares[i, j].BounceSquare();

                    i--;
                    j++;

                    delay = Time.time + loadSpeedSlider.value;
                }

                if (j >= Size - 1)
                {
                    isFirstPartDone = true;
                    i = 0;
                    j = 0;
                    counterMod = 0;
                }
            }
            else
            {
                i = Size - 1;
                j = secCounter;

                while (j <= Size - 1)
                {
                    squares[i, j].BounceSquare();

                    i--;
                    j++;

                    delay = Time.time + loadSpeedSlider.value;
                }
            }

            counter++;

            if (isFirstPartDone)
                secCounter++;

            if (counter == Size + Size)
            {
                secCounter = -1;
                counter = 0;
                isFirstPartDone = false;
            }
        }
    }

    public void Switch2Diagonal()
    {
        if (loadStyle == LoadStyle.Diagonal)
            return;

        secCounter = -1;
        counter = 0;
        isFirstPartDone = false;

        loadStyle = LoadStyle.Diagonal;
    }

    public void Switch2Straight()
    {
        if (loadStyle == LoadStyle.Straight)
            return;

        secCounter = -1;
        counter = 0;
        isFirstPartDone = false;

        loadStyle = LoadStyle.Straight;
    }
}
