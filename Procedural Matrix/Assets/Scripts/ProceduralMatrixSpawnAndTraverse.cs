using System;
using UnityEngine;

public class ProceduralMatrixSpawnAndTraverse : MonoBehaviour
{
    public enum TraverseMode
    {
        Traverse1, Traverse2, Traverse3, Traverse4, Traverse5, Traverse6, Traverse7, Traverse8, Traverse9, Traverse10,
        Traverse11, Traverse12, Traverse13, Traverse14, Traverse15, Traverse16, Traverse17, Traverse18, Traverse19, Traverse20,
        Traverse21, Traverse22, Traverse23, Traverse24, Traverse25, Traverse26, Traverse27, Traverse28, Traverse29, Traverse30
    }

    public BounceTween tweenObj;

    public TraverseMode traverseMode = TraverseMode.Traverse1;

    public int Dimension = 15;

    [Range(0.2f, 0f)]
    public float defaultDelaySpan = 0.094f;

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

    bool hbool = false, hIbool = false, hSbool = false, vbool = false, vIbool = false,
        dLbool = false, dLIbool = false, dRbool = false, dRIbool = false;

    int currChannel;

    private void Awake()
    {
        invDLSecCounter = Dimension;
        dRSecCounter = Dimension;

        currChannel = (int)traverseMode;

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
        ChangeChannel();

        /*
         * All Traversing functions are stackable and based on the demo video,
         * below are the stack arrangements used for the different traversing patterns you saw.
         * If you just care about the algorithms, just scroll past this "Update" function.
         */
        #region TraverseCombinations
        switch (traverseMode)
        {
            case TraverseMode.Traverse1:
                HorizontalStepTraverse();
                break;
            case TraverseMode.Traverse2:
                HorizontalTraverse();
                break;
            case TraverseMode.Traverse3:
                HorizontalTraverseInverse();
                break;
            case TraverseMode.Traverse4:
                HorizontalTraverse();
                HorizontalTraverseInverse();
                break;
            case TraverseMode.Traverse5:
                VerticalTraverse();
                break;
            case TraverseMode.Traverse6:
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse7:
                VerticalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse8:
                HorizontalTraverse();
                HorizontalTraverseInverse();
                VerticalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse9:
                DiagonalRightTraverseInverse();
                break;
            case TraverseMode.Traverse10:
                DiagonalLeftTraverseInverse();
                break;
            case TraverseMode.Traverse11:
                DiagonalRightTraverseInverse();
                DiagonalLeftTraverseInverse();
                break;
            case TraverseMode.Traverse12:
                DiagonalRightTraverse();
                break;
            case TraverseMode.Traverse13:
                DiagonalLeftTraverse();
                break;
            case TraverseMode.Traverse14:
                DiagonalRightTraverse();
                DiagonalLeftTraverse();
                break;
            case TraverseMode.Traverse15:
                DiagonalRightTraverse();
                DiagonalLeftTraverse();
                DiagonalRightTraverseInverse();
                DiagonalLeftTraverseInverse();
                break;
            case TraverseMode.Traverse16:
                HorizontalTraverse();
                HorizontalTraverseInverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse17:
                HorizontalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse18:
                DiagonalRightTraverse();
                DiagonalRightTraverseInverse();
                break;
            case TraverseMode.Traverse19:
                HorizontalTraverse();
                VerticalTraverse();
                break;
            case TraverseMode.Traverse20:
                DiagonalLeftTraverse();
                DiagonalLeftTraverseInverse();
                break;
            case TraverseMode.Traverse21:
                DiagonalRightTraverse();
                DiagonalLeftTraverse();
                DiagonalRightTraverseInverse();
                break;
            case TraverseMode.Traverse22:
                DiagonalRightTraverse();
                HorizontalTraverse();
                break;
            case TraverseMode.Traverse23:
                DiagonalRightTraverseInverse();
                DiagonalRightTraverse();
                HorizontalTraverse();
                break;
            case TraverseMode.Traverse24:
                DiagonalLeftTraverse();
                VerticalTraverse();
                break;
            case TraverseMode.Traverse25:
                DiagonalLeftTraverseInverse();
                DiagonalLeftTraverse();
                VerticalTraverse();
                break;
            case TraverseMode.Traverse26:
                DiagonalLeftTraverseInverse();
                DiagonalLeftTraverse();
                VerticalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse27:
                DiagonalRightTraverseInverse();
                DiagonalRightTraverse();
                VerticalTraverse();
                VerticalTraverseInverse();
                break;
            case TraverseMode.Traverse28:
                DiagonalRightTraverseInverse();
                DiagonalRightTraverse();
                HorizontalTraverse();
                HorizontalTraverseInverse();
                break;
            case TraverseMode.Traverse29:
                DiagonalLeftTraverseInverse();
                DiagonalLeftTraverse();
                HorizontalTraverse();
                HorizontalTraverseInverse();
                break;
            case TraverseMode.Traverse30:
                HorizontalTraverse();
                HorizontalTraverseInverse();
                VerticalTraverse();
                VerticalTraverseInverse();

                DiagonalRightTraverse();
                DiagonalLeftTraverse();
                DiagonalRightTraverseInverse();
                DiagonalLeftTraverseInverse();
                break;
        }
        #endregion
    }

    void ChangeChannel()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        { ResetAll();  SwitchEnum(1);}
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        { ResetAll();  SwitchEnum(-1);};
    }

    void SwitchEnum(int inputDir)
    {
        currChannel = (currChannel + inputDir) % Enum.GetValues(typeof(TraverseMode)).Length;

        if (currChannel < 0)
            currChannel = Enum.GetValues(typeof(TraverseMode)).Length - 1;
    
        traverseMode = (TraverseMode) currChannel;
    }

    void HorizontalTraverse()
    {
        while (hI < Dimension && Time.time > hDelay)
        {
            hJ = 0;

            while (hJ < Dimension)
            {
                if (hbool)
                {
                    squares[hI, hJ].BounceSquare(true);
                }
                else
                {
                    squares[hI, hJ].BounceSquare(false);
                }

                hJ++;

                hDelay = Time.time + defaultDelaySpan;
            }

            hI++;

            if (hI == Dimension)
            {
                hI = 0;
                hbool = !hbool;
            }
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
                if (hIbool)
                {
                    squares[invHI, invHJ].BounceSquare(true);
                }
                else
                {
                    squares[invHI, invHJ].BounceSquare(false);
                }
                
                invHJ++;

                invHDelay = Time.time + defaultDelaySpan;
            }

            invHCounter++;

            if (invHCounter == Dimension)
            {
                invHCounter = 0;
                hIbool = !hIbool;
            }  
        }
    }

    void HorizontalStepTraverse()
    {
        while (hSCounter < (Dimension * Dimension) && Time.time > hSDelay)
        {
            hSI = hSCounter / Dimension;
            hSJ = hSCounter % Dimension;

            if (hSbool)
            {
                squares[hSI, hSJ].BounceSquare(true);
            }
            else
            {
                squares[hSI, hSJ].BounceSquare(false);
            }
            
            hSDelay = Time.time + defaultDelaySpan;
            hSCounter++;

            if (hSCounter == (Dimension * Dimension))
            {
                hSCounter = 0;
                hSbool = !hSbool;
            }
        }
    }

    void VerticalTraverse()
    {
        while (vI < Dimension && Time.time > vDelay)
        {
            vJ = 0;

            while (vJ < Dimension)
            {
                if (vbool)
                {
                    squares[vJ, vI].BounceSquare(true);
                }
                else
                {
                    squares[vJ, vI].BounceSquare(false);
                }

                vJ++;

                vDelay = Time.time + defaultDelaySpan;
            }

            vI++;

            if (vI == Dimension)
            {
                vI = 0;
                vbool = !vbool;
            }  
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
                if (vIbool)
                {
                    squares[invVJ, invVI].BounceSquare(true);
                }
                else
                {
                    squares[invVJ, invVI].BounceSquare(false);
                }
                
                invVJ++;

                invVDelay = Time.time + defaultDelaySpan;
            }

            invVCounter++;

            if (invVCounter == Dimension)
            {
                invVCounter = 0;
                vIbool = !vIbool;
            }
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
                    if (dLbool)
                    {
                        squares[dLI, dLJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[dLI, dLJ].BounceSquare(false);
                    }
                    
                    dLI--;
                    dLJ++;

                    dLDelay = Time.time + defaultDelaySpan;
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
                    if (dLbool)
                    {
                        squares[dLI, dLJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[dLI, dLJ].BounceSquare(false);
                    }

                    dLI--;
                    dLJ++;

                    dLDelay = Time.time + defaultDelaySpan;
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
                dLbool = !dLbool;
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
                    if (dLIbool)
                    {
                        squares[invDLI, invDLJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[invDLI, invDLJ].BounceSquare(false);
                    }
                    
                    invDLI++;
                    invDLJ--;

                    invDLDelay = Time.time + defaultDelaySpan;
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
                    if (dLIbool)
                    {
                        squares[invDLI, invDLJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[invDLI, invDLJ].BounceSquare(false);
                    }

                    invDLI++;
                    invDLJ--;

                    invDLDelay = Time.time + defaultDelaySpan;
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
                dLIbool = !dLIbool;
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
                    if (dRbool)
                    {
                        squares[dRI, dRJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[dRI, dRJ].BounceSquare(false);
                    }

                    dRI--;
                    dRJ--;

                    dRDelay = Time.time + defaultDelaySpan;
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
                    if (dRbool)
                    {
                        squares[dRI, dRJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[dRI, dRJ].BounceSquare(false);
                    }

                    dRI--;
                    dRJ--;

                    dRDelay = Time.time + defaultDelaySpan;
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
                dRbool = !dRbool;
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
                    if (dRIbool)
                    {
                        squares[invDRI, invDRJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[invDRI, invDRJ].BounceSquare(false);
                    }
                   
                    invDRI++;
                    invDRJ++;

                    invDRDelay = Time.time + defaultDelaySpan;
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
                    if (dRIbool)
                    {
                        squares[invDRI, invDRJ].BounceSquare(true);
                    }
                    else
                    {
                        squares[invDRI, invDRJ].BounceSquare(false);
                    }

                    invDRI++;
                    invDRJ++;

                    invDRDelay = Time.time + defaultDelaySpan;
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
                dRIbool = !dRIbool;
            }
        }
    }

    private void ResetAll()
    {
        hI = 0;
        hbool = !hbool;

        invHCounter = 0;
        hIbool = !hIbool;

        hSCounter = 0;
        hSbool = !hSbool;

        vI = 0;
        vbool = !vbool;

        invVCounter = 0;
        vIbool = !vIbool;

        dLSecCounter = -1;
        dLCounter = 0;
        dIsFirstPartDone = false;
        dLbool = !dLbool;

        invDLSecCounter = Dimension;
        invDLCounter = 0;
        invDIsFirstPartDone = false;
        dLIbool = !dLIbool;

        dRSecCounter = Dimension;
        dRCounter = 0;
        dRIsFirstPartDone = false;
        dRbool = !dRbool;

        invDRSecCounter = -1;
        invDRCounter = 0;
        invDRIsFirstPartDone = false;
        dRIbool = !dRIbool;
    }
}
