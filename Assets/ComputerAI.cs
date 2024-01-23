using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    public GameManager gameManager;

    /// <summary>
    /// Makes a move for the computer player in the Tic Tac Toe game.
    /// </summary>
    public void MakeMove()
    {
        Vector2Int winningMove = new Vector2Int(-1, -1);
        if (CheckPotentialWin(GameManager.Player.Cross, GameManager.Player.Circle, ref winningMove))
        {
            gameManager.MakeMove(winningMove.x, winningMove.y);
        }
        else
        {
            List<Vector2Int> availableMoves = GetAvailableMoves();
            if (availableMoves.Count > 0)
            {
                Vector2Int randomMove = availableMoves[Random.Range(0, availableMoves.Count)];
                gameManager.MakeMove(randomMove.x, randomMove.y);
            }
        }
    }

    /// <summary>
    /// Gets a list of available moves for the computer player in the Tic Tac Toe game.
    /// </summary>
    /// <returns>A list of Vector2Int coordinates representing the available moves on the board.</returns>
    private List<Vector2Int> GetAvailableMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (gameManager.board[x, y] == GameManager.Player.None)
                {
                    moves.Add(new Vector2Int(x, y));
                }
            }
        }
        return moves;
    }

    /// <summary>
    /// Check if there's a potential win for the given player.
    /// </summary>
    /// <param name="player">The player to check for a potential win.</param>
    /// <param name="opponent">The opponent of the player.</param>
    /// <param name="winningMove">A reference to a Vector2Int that will store the winning move if found.</param>
    /// <returns>True if there's a potential win, false otherwise.</returns>
    private bool CheckPotentialWin(GameManager.Player player, GameManager.Player opponent, ref Vector2Int winningMove)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (gameManager.board[x, y] == GameManager.Player.None)
                {
                    gameManager.board[x, y] = player;
                    if (gameManager.CheckForWinner())
                    {
                        winningMove = new Vector2Int(x, y);
                        gameManager.board[x, y] = GameManager.Player.None;
                        return true;
                    }
                    gameManager.board[x, y] = GameManager.Player.None;
                }
            }
        }
        return false;
    }
}