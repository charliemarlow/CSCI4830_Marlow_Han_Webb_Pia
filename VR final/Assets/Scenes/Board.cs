using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject graphicChessBoard;
    public bool debugMode;

    private const int boardDimension = 8;

    public GameObject pawnDarkPrefab;
    public GameObject pawnLightPrefab;
    public GameObject rookDarkPrefab;
    public GameObject rookLightPrefab;
    public GameObject knightDarkPrefab;
    public GameObject knightLightPrefab;
    public GameObject bishopDarkPrefab;
    public GameObject bishopLightPrefab;
    public GameObject kingDarkPrefab;
    public GameObject kingLightPrefab;
    public GameObject queenDarkPrefab;
    public GameObject queenLightPrefab;


    private void instantiatePiece(GameObject prefab, int newX, int newZ)
    {
        Vector3 position = new Vector3(newX, 0, newZ);
        Quaternion rot = this.transform.rotation;
        GameObject piece = Instantiate(prefab, position, rot,this.transform);
        //piece.transform.localPosition = position;

        ChessPiece p = piece.gameObject.GetComponent<ChessPiece>();
        p.movePiece(newX, newZ);
        p.isLight = setColor(newZ);

    }

    bool setColor(int z)
    {
        return z < 2 ? true : false;
    }

    private void setUpRooks(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 0, side);
        instantiatePiece(prefab, 7, side);
    }

    private void setUpKnights(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 1, side);
        instantiatePiece(prefab, 6, side);
    }

    private void setUpBishops(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 2, side);
        instantiatePiece(prefab, 5, side);
    }

    private void setUpRoyals(GameObject king, GameObject queen, int side)
    {
        instantiatePiece(queen, 3, side);
        instantiatePiece(king, 4, side);
    }

    private void instantiatePieces()
    {
        int lightSide = 0;
        int darkSide = 7;

        // set up pawns
        for (int i = 0; i < boardDimension; i++)
        {
            instantiatePiece(pawnLightPrefab, i, lightSide + 1);
            instantiatePiece(pawnDarkPrefab, darkSide - i, darkSide - 1);
        }

        // set up light pieces
        setUpRooks(rookLightPrefab, lightSide);
        setUpKnights(knightLightPrefab, lightSide);
        setUpBishops(bishopLightPrefab, lightSide);
        setUpRoyals(kingLightPrefab, queenLightPrefab, lightSide);

        // set up dark pieces
        setUpRooks(rookDarkPrefab, darkSide);
        setUpKnights(knightDarkPrefab, darkSide);
        setUpBishops(bishopDarkPrefab, darkSide);
        setUpRoyals(kingDarkPrefab, queenDarkPrefab, darkSide);
    }

    // Start is called before the first frame update
    void Start()
    {
        instantiatePieces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
