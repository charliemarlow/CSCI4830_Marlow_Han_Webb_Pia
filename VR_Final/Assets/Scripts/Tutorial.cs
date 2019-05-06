using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{

    public Board board;
    // Start is called before the first frame update
    void Start()
    {
        // get board reference
        GameObject boardGo = GameObject.FindWithTag("board");
        board = boardGo.GetComponent<Board>();

        // clear board
        board.clearBoard();
        board.clearHighlights();

        // set isTutorial to true
        board.setIsTutorial(true);

        // set which tutorial it is
        board.tutorial = this;
        this.extraStart();
    }

    public virtual void extraStart()
    {

    }

    public virtual void pickupPiece(ChessPiece piece)
    {

    }

    public virtual void dropPiece(ChessPiece piece)
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public void instantiateHighlight(int x, int y)
    {
        GameObject highlightPrefab = board.highlightPrefab;
        GameObject o = Instantiate(highlightPrefab, board.transform);

        Vector3 newLoc = new Vector3(x, 0, y);
        o.transform.localPosition = newLoc;
        board.highlights[x, y] = o;
    }

    public void instantiatePiece(GameObject prefab, int newX, int newZ, bool active)
    {

        Vector3 position = new Vector3(newX, 0, newZ);
        Quaternion rot = board.transform.rotation;
        GameObject piece = Instantiate(prefab, position, rot, board.transform);
        //piece.transform.localPosition = position;

        ChessPiece p = piece.gameObject.GetComponent<ChessPiece>();
        p.movePiece(newX, newZ);
        p.isLight = true;
        p.originalRot = p.transform.rotation;
        board.logicalBoard[newX, newZ] = p;
        p.GetComponent<ChessPiece>().enabled = active;

    }
}
