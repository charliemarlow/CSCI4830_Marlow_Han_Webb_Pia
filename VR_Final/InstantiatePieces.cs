using System;

public class InstantiatePieces
{
    public void setUpRooks(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 0, side);
        instantiatePiece(prefab, 7, side);
    }

    public void setUpKnights(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 1, side);
        instantiatePiece(prefab, 6, side);
    }

    public void setUpBishops(GameObject prefab, int side)
    {
        instantiatePiece(prefab, 2, side);
        instantiatePiece(prefab, 5, side);
    }

    public void setUpRoyals(GameObject king, GameObject queen, int side)
    {
        instantiatePiece(queen, 3, side);
        instantiatePiece(king, 4, side);
    }

    public void instantiatePieces()
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

    public void instantiatePiece(GameObject prefab, int newX, int newZ)
    {

        Vector3 position = new Vector3(newX, 0, newZ);
        Quaternion rot = this.transform.rotation;
        GameObject piece = Instantiate(prefab, position, rot, this.transform);
        //piece.transform.localPosition = position;

        ChessPiece p = piece.gameObject.GetComponent<ChessPiece>();
        p.movePiece(newX, newZ);
        p.isLight = setColor(newZ);

        // set proper rotation for dark pieces
        if (!p.isLight)
        {
            Vector3 rotation = p.transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y + 180, rotation.z);
            p.transform.rotation = Quaternion.Euler(rotation);
            //p.transform.localRotation = new Vector3(90, 0, 0);
        }
        p.originalRot = p.transform.rotation;
        logicalBoard[newX, newZ] = p;

    }
}
