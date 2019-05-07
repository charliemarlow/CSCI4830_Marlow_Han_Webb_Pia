using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueensGambit : Tutorial
{
    private List<AudioClip> soundClips;
    AudioSource mysound;
    private int index = 0;
    // Start is called before the first frame update
    public override void extraStart()
    {
        setNonMoveableWhite();
        mysound = getSource();
        soundClips = getAudio();
        mysound.clip = soundClips[0];
        mysound.Play();
        board.tutorial = this;
        nextMove();
    }

    public override void dropPiece(ChessPiece piece)
    {
        nextMove();
        if(index > 3)
        {
            isDone = true;
        }
    }

    private void nextMove()
    {
        switch (index)
        {
            case 0:
                Destroy(board.logicalBoard[3, 1].gameObject);
                board.instantiatePiece(board.pawnLightPrefab, 3, 1);
                instantiateHighlight(3, 1);
                instantiateHighlight(3, 3);
                break;
            case 1:
                Destroy(board.logicalBoard[2, 1].gameObject);
                board.instantiatePiece(board.pawnLightPrefab, 2, 1);
                instantiateHighlight(2, 1);
                instantiateHighlight(2, 3);
                mysound.clip = soundClips[1];
                mysound.Play();
                break;
            case 2:
                Destroy(board.logicalBoard[3, 0].gameObject);
                board.instantiatePiece(board.queenLightPrefab, 3, 0);
                instantiateHighlight(3, 0);
                instantiateHighlight(0, 3);
                break;
            case 3:
                Destroy(board.logicalBoard[2, 0].gameObject);
                board.instantiatePiece(board.bishopLightPrefab, 2, 0);
                instantiateHighlight(2, 0);
                instantiateHighlight(5, 3);
                break;
        }
        index++;
    }
}
