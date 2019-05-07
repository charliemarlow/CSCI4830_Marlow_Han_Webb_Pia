using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class LaserFingers : MonoBehaviour
{

    public float maxLaserDistance;
    public Collider main1, main2, main3;
    public Collider selKing, selQueen, selBishop, selKnight, selRook;
    public Collider tut1, tut2, tut3, tut4;
    public Collider dif1, dif2, dif3;
    public Collider a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, enter;
    public Button Main1, Main2, Main3;
    public Button SelKing, SelQueen, SelBishop, SelKnight, SelRook;
    public Button Tut1, Tut2, Tut3, Tut4;
    public Button Dif1, Dif2, Dif3;
    public Button A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, ENTER;

    public ControllerInput left, right;
    public GameManager gm;
    public Laser laser;
    public GameObject currentSelection;

    public Board board;

    // Start is called before the first frame update
    void Start()
    {
        laser.gameObject.SetActive(true);
        GameObject boardGo = GameObject.FindWithTag("board");
        board = boardGo.GetComponent<Board>();
    }

    public GameObject selectRaycast()
    {
        if(currentSelection == null) return null;
        return currentSelection;
    }

    // Update is called once per frame
    void Update()
    {

        if(left.menuUp || right.menuUp){
            laser.gameObject.SetActive(true);
        }else{
            laser.gameObject.SetActive(false);
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance))
        {
            laser.length = hit.distance;    // shortens the laser
            if(left.getIsGrabbed() || right.getIsGrabbed()){
                // use index trigger to select an object
                currentSelection = hit.transform.gameObject;
                if(hit.transform.CompareTag("easy")){
                    board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = false;
                    board.isMoveTutorial = false;
                    board.currentDifficulty = 0;
                    board.instantiatePieces();
                    left.killMenu();
                    right.killMenu();

                }else if(hit.transform.CompareTag("medium")){
                    board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = false;
                    board.isMoveTutorial = false;
                    board.currentDifficulty =1;
                    board.instantiatePieces();
                    left.killMenu();
                    right.killMenu();
                }else if(hit.transform.CompareTag("hard")){
                    board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = false;
                    board.isMoveTutorial = false;
                    board.currentDifficulty = 2;
                    board.instantiatePieces();
                    left.killMenu();
                    right.killMenu();
                }
                else if(hit.transform.CompareTag("kingd")){
                     board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = true;
                    gm.startTutorial(2);
                    left.killMenu();
                    right.killMenu();
                    
                }else if(hit.transform.CompareTag("queeng")){
                    board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = true;
                    gm.startTutorial(3);
                    left.killMenu();
                    right.killMenu();
                }else if(hit.transform.CompareTag("movepieces")){
                     board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = true;
                    board.isMoveTutorial = true;
                    gm.startTutorial(1);
                    left.killMenu();
                    right.killMenu();
                }else if(hit.transform.CompareTag("setpieces")){
                     board.clearBoard();
                    board.clearHighlights();
                    board.isTutorial = true;
                    gm.startTutorial(0);
                    left.killMenu();
                    right.killMenu();
                    
                }
            }
        }else{
            laser.length = maxLaserDistance;
        }

        /* 
        // only use laser fingers when taking a survey
        if (gm.raycastMode)
        {
            laser.gameObject.SetActive(true);
        }
        else
        {
            laser.gameObject.SetActive(false);
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out hit, maxLaserDistance))
        {
            laser.length = hit.distance;    // shortens the laser

            // use index trigger to select an object
            currentSelection = hit.transform.gameObject;
            Debug.Log("current selection = " + currentSelection.name);
            // we can check here what it actually is
            //if (hit.collider == main1 || hit.collider== main2 || hit.collider== main3)
            //{
                if (right.getIsGrabbed() == true || left.getIsGrabbed() == true)
                {
                    //mainMenu buttons
                    if (hit.collider == main1){
                        Debug.Log("invoking main1");
                        Main1.onClick.Invoke();
                    }

                    if (hit.collider == main2){
                        Debug.Log("invoking main2");
                        Main2.onClick.Invoke();
                    }
                 
                    if (hit.collider == main3){
                        Main3.onClick.Invoke();
                        Debug.Log("hitting main 3");
                    }


                    //selectPiece menu buttons
                    if (hit.collider == selKing)
                        SelKing.onClick.Invoke();

                    if (hit.collider == selQueen)
                        SelQueen.onClick.Invoke();

                    if (hit.collider == selBishop)
                        SelBishop.onClick.Invoke();

                    if (hit.collider == selKnight)
                        SelKnight.onClick.Invoke();

                    if (hit.collider == selRook)
                        SelRook.onClick.Invoke();

                    //tutorial menu buttons
                    if (hit.collider == tut1)
                        Tut1.onClick.Invoke();

                    if (hit.collider == tut2)
                        Tut2.onClick.Invoke();

                    if (hit.collider == tut3)
                        Tut3.onClick.Invoke();

                    if (hit.collider == tut4)
                        Tut4.onClick.Invoke();


                    //dificulty menu buttons
                    if (hit.collider == dif1)
                        Dif1.onClick.Invoke();

                    if (hit.collider == dif2)
                        Dif2.onClick.Invoke();

                    if (hit.collider == dif3)
                        Dif3.onClick.Invoke();

                    if (hit.collider == dif3)
                        Dif3.onClick.Invoke();


                    //keyboard buttons
                    if (hit.collider == a)
                        A.onClick.Invoke();

                    if (hit.collider == b)
                        B.onClick.Invoke();

                    if (hit.collider == c)
                        C.onClick.Invoke();

                    if (hit.collider == d)
                        D.onClick.Invoke();

                    if (hit.collider == e)
                        E.onClick.Invoke();

                    if (hit.collider == f)
                        F.onClick.Invoke();

                    if (hit.collider == g)
                        G.onClick.Invoke();

                    if (hit.collider == h)
                        H.onClick.Invoke();

                    if (hit.collider == i)
                        I.onClick.Invoke();

                    if (hit.collider == j)
                        J.onClick.Invoke();

                    if (hit.collider == k)
                        K.onClick.Invoke();

                    if (hit.collider == l)
                        L.onClick.Invoke();

                    if (hit.collider == m)
                        M.onClick.Invoke();

                    if (hit.collider == n)
                        N.onClick.Invoke();

                    if (hit.collider == o)
                        O.onClick.Invoke();

                    if (hit.collider == p)
                        P.onClick.Invoke();

                    if (hit.collider == q)
                        Q.onClick.Invoke();

                    if (hit.collider == r)
                        R.onClick.Invoke();

                    if (hit.collider == s)
                        S.onClick.Invoke();

                    if (hit.collider == t)
                        T.onClick.Invoke();

                    if (hit.collider == u)
                        U.onClick.Invoke();

                    if (hit.collider == v)
                        V.onClick.Invoke();

                    if (hit.collider == w)
                        W.onClick.Invoke();

                    if (hit.collider == x)
                        X.onClick.Invoke();

                    if (hit.collider == y)
                        Y.onClick.Invoke();

                    if (hit.collider == z)
                        Z.onClick.Invoke();

                    if (hit.collider == enter)
                        ENTER.onClick.Invoke();

                }
            //}

        }
        else
        {
            laser.length = maxLaserDistance;
        }
        */
    }
}


