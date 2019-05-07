using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{

    public Board board;
    public static AudioSource sSource;
    public static List<AudioClip> voiceover;
    public bool isDone;
    // Start is called before the first frame update

    public static AudioSource getSource()
    {
        return sSource;
        Debug.Log("get Audio Source");
    }

    public void setSource(AudioSource src)
    {
        sSource = src;
        Debug.Log("set Audio Source");
    }

    public static List<AudioClip> getAudio()
    {
        return voiceover;
    }

    public void setAudio(List<AudioClip> list)
    {
        voiceover = new List<AudioClip>();
        foreach(AudioClip clip in list){
            Debug.Log(clip.name);
            Debug.Log(voiceover);
            voiceover.Add(clip);
        }
    }

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

    public ChessPiece instantiatePiece(GameObject prefab, int newX, int newZ, bool active)
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
        return p;

    }
    public ChessPiece instantiateDarkPiece(GameObject prefab, int newX, int newZ, bool active)
    {

        Vector3 position = new Vector3(newX, 0, newZ);
        Quaternion rot = board.transform.rotation;
        GameObject piece = Instantiate(prefab, position, rot, board.transform);
        //piece.transform.localPosition = position;

        ChessPiece p = piece.gameObject.GetComponent<ChessPiece>();
        p.movePiece(newX, newZ);
        p.isLight = false;
        p.originalRot = p.transform.rotation;
        board.logicalBoard[newX, newZ] = p;
        p.GetComponent<ChessPiece>().enabled = active;
        return p;

    }

    public void setNonMoveableWhite(){
        for (int i = 0; i < 8; i++)
        {
            instantiatePiece(board.pawnLightPrefab, i, 1, false);
        }

        instantiatePiece(board.rookLightPrefab, 0, 0, false);
        instantiatePiece(board.rookLightPrefab, 7, 0, false);

        instantiatePiece(board.knightLightPrefab, 1, 0, false);
        instantiatePiece(board.knightLightPrefab, 6, 0, false);

        instantiatePiece(board.bishopLightPrefab, 2, 0, false);
        instantiatePiece(board.bishopLightPrefab, 5, 0, false);

        instantiatePiece(board.queenLightPrefab, 3, 0, false);
        instantiatePiece(board.kingLightPrefab, 4, 0, false);
    }
}
