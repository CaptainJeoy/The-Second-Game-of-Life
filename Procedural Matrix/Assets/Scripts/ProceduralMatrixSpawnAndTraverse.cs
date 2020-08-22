using UnityEngine;
using UnityEngine.UI;

public class ProceduralMatrixSpawnAndTraverse : MonoBehaviour
{
    public enum TraverseStyle
    {
        Straight,
        Diagonal
    }

    public BounceTween tweenObj;

    public TraverseStyle loadStyle = TraverseStyle.Diagonal;

    public Slider loadSpeedSlider;

    public int Dimension = 7;

    public float defaultDelaySpan = 0.06f;

    private BounceTween[,] squares;

    float hDelay, hSDelay, invHDelay, vDelay, invVDelay, dLDelay, invDLDelay, dRDelay, invDRDelay;

    int hI = 0, hJ = 0;
    int hSI = 0, hSJ = 0, hSCounter = 0;
    int invHI = 0, invHJ = 0, invHCounter = 0;

    int vI = 0, vJ = 0;
    int invVI = 0, invVJ = 0, invVCounter = 0;

    int dLI = 0, dLJ = 0, dLCounter = 0, dLCounterMod = 0, dLSecCounter = -1;
    int invDLI = 0, invDLJ = 0, invDLCounter = 0, invDLCounterMod = 0, invDLSecCounter;

    int dRI = 0, dRJ = 0, dRCounter = 0, dRCounterMod = 0, dRSecCounter;
    int invDRI = 0, invDRJ = 0, invDRCounter = 0, invDRCounterMod = 0, invDRSecCounter = -1;

    bool dIsFirstPartDone = false, invDIsFirstPartDone = false, dRIsFirstPartDone = false,
        invDRIsFirstPartDone = false;

    private void Awake()
    {
        loadSpeedSlider.value = defaultDelaySpan;

        invDLSecCounter = Dimension;
        dRSecCounter = Dimension;

        SpawnMatrix(Dimension);
    }

    void SpawnMatrix(int dimension)
    {
        squares = new BounceTween[dimension, dimension];

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                BounceTween bt = Instantiate(tweenObj);
                bt.transform.parent = this.transform;
                bt.transform.position = new Vector3(j - (dimension / 2), i - (dimension / 2), 0f);

                squares[i, j] = bt;
            }
        }
    }

    private void Update()
    {
        /*
         * All Traversing functions are stackable and based on the demo video,
         * below are the stack arrangements used for the different traversing patterns you saw.
         * If you're opening the project in unity, just UNCOMMENT any of the functions,
         * they are all numbered based on their appearance in the video.
         * If you just care about the algorithms, just scroll past this "Update" function.
         */

        //Traverse 1
        HorizontalStepTraverse();

        //Traverse 2
        /*
        HorizontalTraverse();
        */

        //Traverse 3
        /*
        HorizontalTraverseInverse();
        */

        //Traverse 4
        /*
        HorizontalTraverse();
        HorizontalTraverseInverse();
        */

        //Traverse 5
        /*
        VerticalTraverse();
        */

        //Traverse 6
        /*
        VerticalTraverseInverse();
        */

        //Traverse 7
        /*
        VerticalTraverse();
        VerticalTraverseInverse();
        */

        //Traverse 8
        /*
        HorizontalTraverse();
        HorizontalTraverseInverse();
        VerticalTraverse();
        VerticalTraverseInverse();
        */

        //Traverse 9
        /*
        DiagonalRightTraverseInverse();
        */

        //Traverse 10
        /*
        DiagonalLeftTraverseInverse();
        */

        //Traverse 11
        /*
        DiagonalRightTraverseInverse();
        DiagonalLeftTraverseInverse();
        */

        //Traverse 12
        /*
        DiagonalRightTraverse();
        */

        //Traverse 13
        /*
        DiagonalLeftTraverse();
        */

        //Traverse 14
        /*
        DiagonalRightTraverse();
        DiagonalLeftTraverse();
        */

        //Traverse 15
        /*
        DiagonalRightTraverse();
        DiagonalLeftTraverse();
        DiagonalRightTraverseInverse();
        DiagonalLeftTraverseInverse();
        */

        //Traverse 16
        /*
        HorizontalTraverse();
        HorizontalTraverseInverse();
        VerticalTraverseInverse();
        */

        //Traverse 17
        /*
        HorizontalTraverse();
        VerticalTraverseInverse();
        */

        //Traverse 18
        /*
        HorizontalTraverse();
        HorizontalTraverseInverse();
        VerticalTraverse();
        VerticalTraverseInverse();

        DiagonalRightTraverse();
        DiagonalLeftTraverse();
        DiagonalRightTraverseInverse();
        DiagonalLeftTraverseInverse();
       */

        /*
        switch (loadStyle)
        {
            case TraverseStyle.Straight:
                HorizontalTraverseInverse();
                HorizontalTraverse();
                VerticalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseStyle.Diagonal:
                DiagonalLeftTraverse();
                DiagonalLeftTraverseInverse();
                DiagonalRightTraverse();
                DiagonalRightTraverseInverse();
                break;
        }
        */
    }

    void HorizontalTraverse()
    {
        while (hI < Dimension && Time.time > hDelay)
        {
            hJ = 0;

            while (hJ < Dimension)
            {
                squares[hI, hJ].BounceSquare();

                hJ++;

                hDelay = Time.time + loadSpeedSlider.value;
            }

            hI++;

            if (hI == Dimension)
                hI = 0;
        }
    }

    void HorizontalTraverseInverse()
    {
        while (invHCounter < Dimension && Time.time > invHDelay)
        {
            invHI = (Dimension - 1) - (invHCounter % (Dimension));
            invHJ = 0;

            while (invHJ < Dimension)
            {
                squares[invHI, invHJ].BounceSquare();

                invHJ++;

                invHDelay = Time.time + loadSpeedSlider.value;
            }

            invHCounter++;

            if (invHCounter == Dimension)
               invHCounter = 0;
        }
    }

    void HorizontalStepTraverse()
    {
        while (hSCounter < (Dimension * Dimension) && Time.time > hSDelay)
        {
            hSI = hSCounter / Dimension;
            hSJ = hSCounter % Dimension;

            squares[hSI, hSJ].BounceSquare();

            hSDelay = Time.time + loadSpeedSlider.value;
            hSCounter++;

            if (hSCounter == (Dimension * Dimension))
                hSCounter = 0;
        }
    }

    void VerticalTraverse()
    {
        while (vI < Dimension && Time.time > vDelay)
        {
            vJ = 0;

            while (vJ < Dimension)
            {
                squares[vJ, vI].BounceSquare();

                vJ++;

                vDelay = Time.time + loadSpeedSlider.value;
            }

            vI++;

            if (vI == Dimension)
                vI = 0;
        }
    }

    void VerticalTraverseInverse()
    {
        while (invVCounter < Dimension && Time.time > invVDelay)
        {
            invVI = (Dimension - 1) - (invVCounter % (Dimension));
            invVJ = 0;

            while (invVJ < Dimension)
            {
                squares[invVJ, invVI].BounceSquare();

                invVJ++;

                invVDelay = Time.time + loadSpeedSlider.value;
            }

            invVCounter++;

            if (invVCounter == Dimension)
                invVCounter = 0;
        }
    }

    void DiagonalLeftTraverse()
    {
        while (dLCounter < Dimension + Dimension && Time.time > dLDelay)
        {
            dLCounterMod = dLCounter % Dimension;

            if (!dIsFirstPartDone)
            {
                dLI = dLCounterMod;
                dLJ = 0;

                while (dLI >= 0)
                {
                    squares[dLI, dLJ].BounceSquare();

                    dLI--;
                    dLJ++;

                    dLDelay = Time.time + loadSpeedSlider.value;
                }

                if (dLJ >= Dimension - 1)
                {
                    dIsFirstPartDone = true;
                    dLI = 0;
                    dLJ = 0;
                    dLCounterMod = 0;
                }
            }
            else
            {
                dLI = Dimension - 1;
                dLJ = dLSecCounter;

                while (dLJ <= Dimension - 1)
                {
                    squares[dLI, dLJ].BounceSquare();

                    dLI--;
                    dLJ++;

                    dLDelay = Time.time + loadSpeedSlider.value;
                }
            }

            dLCounter++;

            if (dIsFirstPartDone)
                dLSecCounter++;

            if (dLCounter == Dimension + Dimension)
            {
                dLSecCounter = -1;
                dLCounter = 0;
                dIsFirstPartDone = false;
            }
        }
    }

    void DiagonalLeftTraverseInverse()
    {
        while (invDLCounter < Dimension + Dimension && Time.time > invDLDelay)
        {
            invDLCounterMod = Dimension - (invDLCounter % Dimension);

            if (!invDIsFirstPartDone)
            {
                invDLI = invDLCounterMod;
                invDLJ = Dimension - 1;

                while (invDLI <= Dimension - 1)
                {
                    squares[invDLI, invDLJ].BounceSquare();

                    invDLI++;
                    invDLJ--;

                    invDLDelay = Time.time + loadSpeedSlider.value;
                }

                if (invDLJ <= 0)
                {
                    invDIsFirstPartDone = true;
                    invDLI = 0;
                    invDLJ = 0;
                    dLCounterMod = 0;
                }
            }
            else
            {
                invDLI = 0;
                invDLJ = invDLSecCounter;

                while (invDLJ >= 0)
                {
                    squares[invDLI, invDLJ].BounceSquare();

                    invDLI++;
                    invDLJ--;

                    invDLDelay = Time.time + loadSpeedSlider.value;
                }
            }

            invDLCounter++;

            if (invDIsFirstPartDone)
                invDLSecCounter--;

            if (invDLCounter == Dimension + Dimension)
            {
                invDLSecCounter = Dimension;
                invDLCounter = 0;
                invDIsFirstPartDone = false;
            }
        }
    }

    void DiagonalRightTraverse()
    {
        while (dRCounter < Dimension + Dimension && Time.time > dRDelay)
        {
            dRCounterMod = dRCounter % Dimension;

            if (!dRIsFirstPartDone)
            {
                dRI = dRCounterMod;
                dRJ = Dimension - 1;

                while (dRI >= 0)
                {
                    squares[dRI, dRJ].BounceSquare();

                    dRI--;
                    dRJ--;

                    dRDelay = Time.time + loadSpeedSlider.value;
                }

                if (dRJ <= 0)
                {
                    dRIsFirstPartDone = true;
                    dRI = 0;
                    dRJ = 0;
                    dRCounterMod = 0;
                }
            }
            else
            {
                dRI = Dimension - 1;
                dRJ = dRSecCounter;

                while (dRJ >= 0)
                {
                    squares[dRI, dRJ].BounceSquare();

                    dRI--;
                    dRJ--;

                    dRDelay = Time.time + loadSpeedSlider.value;
                }
            }

            dRCounter++;

            if (dRIsFirstPartDone)
                dRSecCounter--;

            if (dRCounter == Dimension + Dimension)
            {
                dRSecCounter = Dimension;
                dRCounter = 0;
                dRIsFirstPartDone = false;
            }
        }
    }

    void DiagonalRightTraverseInverse()
    {
        while (invDRCounter < Dimension + Dimension && Time.time > invDRDelay)
        {
            invDRCounterMod = (Dimension - 1) - (invDRCounter % (Dimension - 1));

            if (!invDRIsFirstPartDone)
            {
                invDRI = invDRCounterMod;
                invDRJ = 0;

                while (invDRI <= Dimension - 1)
                {
                    squares[invDRI, invDRJ].BounceSquare();

                    invDRI++;
                    invDRJ++;

                    invDRDelay = Time.time + loadSpeedSlider.value;
                }

                if (invDRJ >= Dimension - 1)
                {
                    invDRIsFirstPartDone = true;
                    invDRI = 0;
                    invDRJ = 0;
                    invDRCounterMod = 0;
                }
            }
            else
            {
                invDRI = 0;
                invDRJ = invDRSecCounter;

                while (invDRJ <= Dimension - 1)
                {
                    squares[invDRI, invDRJ].BounceSquare();

                    invDRI++;
                    invDRJ++;

                    invDRDelay = Time.time + loadSpeedSlider.value;
                }
            }

            invDRCounter++;

            if (invDRIsFirstPartDone)
                invDRSecCounter++;

            if (invDRCounter == Dimension + Dimension)
            {
                invDRSecCounter = -1;
                invDRCounter = 0;
                invDRIsFirstPartDone = false;
            }
        }
    }

    public void Switch2Diagonal()
    {
        if (loadStyle == TraverseStyle.Diagonal)
            return;

        invHCounter = 0;

        hI = 0;
        hJ = 0;

        hSI = 0;
        hSJ = 0;
        hSCounter = 0;

        invVCounter = 0;

        vI = 0;
        vJ = 0;

        dLSecCounter = -1;
        dLCounter = 0;
        dIsFirstPartDone = false;

        invDLSecCounter = Dimension;
        invDLCounter = 0;
        invDIsFirstPartDone = false;

        dRSecCounter = Dimension;
        dRCounter = 0;
        dRIsFirstPartDone = false;

        invDRSecCounter = -1;
        invDRCounter = 0;
        invDRIsFirstPartDone = false;

        loadStyle = TraverseStyle.Diagonal;
    }

    public void Switch2Straight()
    {
        if (loadStyle == TraverseStyle.Straight)
            return;

        invHCounter = 0;

        hI = 0;
        hJ = 0;

        hSI = 0;
        hSJ = 0;
        hSCounter = 0;

        invVCounter = 0;

        vI = 0;
        vJ = 0;

        dLSecCounter = -1;
        dLCounter = 0;
        dIsFirstPartDone = false;

        invDLSecCounter = Dimension;
        invDLCounter = 0;
        invDIsFirstPartDone = false;

        dRSecCounter = Dimension;
        dRCounter = 0;
        dRIsFirstPartDone = false;

        invDRSecCounter = -1;
        invDRCounter = 0;
        invDRIsFirstPartDone = false;

        loadStyle = TraverseStyle.Straight;
    }
}
