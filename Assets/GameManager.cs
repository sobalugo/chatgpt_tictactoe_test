using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum Player { None, Cross, Circle }
    public Player currentPlayer = Player.Cross;
    public Player[,] board = new Player[3, 3];
    public Button[] buttons;
    public Text gameOverText;

    /// <summary>
    /// Initialize the game by setting up button listeners.
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int x = i / 3;
            int y = i % 3;
            buttons[i].onClick.AddListener(() => MakeMove(x, y));
        }

    }

    /// <summary>
    /// Make a move on the board and update the UI.
    /// Checks if there is a winner or a draw after the move by calling CheckForWinner().
    /// Also calls GameOver with winner or GameOver with draw.
    /// </summary>
    /// <param name="x">The row of the move.</param>
    /// <param name="y">The column of the move.</param>
    public void MakeMove(int x, int y)
    {
        if (board[x, y] == Player.None)
        {
            board[x, y] = currentPlayer;
            buttons[x * 3 + y].GetComponentInChildren<Text>().text = (currentPlayer == Player.Cross) ? "X" : "O";

            if (!CheckForWinner()) // Check for a winner before switching players
            {
                SwitchPlayer();
            }
            else
            {
                if (currentPlayer != Player.None)
                {
                    GameOver(currentPlayer); // Call GameOver with draw
                }
                else
                {
                    GameOver(Player.None); // Call GameOver when the player wins
                }
            }
        }
    }
        
    /// <summary>
    /// Check if there's a winner or a draw after each move.
    /// If there is a draw, currentPlayer = Player.None
    /// </summary>
    /// <returns>True if there's a winner or a draw, false otherwise.</returns>
    public bool CheckForWinner()
    {
        // Check for horizontal or vertical win states
        for (int i = 0; i < 3; i++)
        {
            if (CheckLine(board[i, 0], board[i, 1], board[i, 2]) ||
                CheckLine(board[0, i], board[1, i], board[2, i]))
            {
                return true;
            }
        }
        
        // Check for diagonal win states
        if (CheckLine(board[0, 0], board[1, 1], board[2, 2]) ||
            CheckLine(board[0, 2], board[1, 1], board[2, 0]))
        {
            return true;
        }

        // Check if it's a draw
        if (IsBoardFull())
        {   
            currentPlayer = Player.None;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the Tic-Tac-Toe board is full or not.
    /// </summary>
    /// <returns>True if the board is full, false otherwise.</returns>
    private bool IsBoardFull()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[x, y] == Player.None)
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    /// <summary>
    /// Check if a line (row, column, or diagonal) has the same player.
    /// </summary>
    /// <param name="a">First player in the line.</param>
    /// <param name="b">Second player in the line.</param>
    /// <param name="c">Third player in the line.</param>
    /// <returns>True if the line has the same player, false otherwise.</returns>
    private bool CheckLine(Player a, Player b, Player c)
    {
        return a != Player.None && a == b && a == c;
    }

    /// <summary>
    /// Handle the end of the game and display the game over message.
    /// </summary>
    /// <param name="winner">The winning player or Player.None for a draw.</param>
    private void GameOver(Player winner)
    {
        if (winner == Player.None)
        {
            gameOverText.text = "It's a draw!";
        }
        else
        {
            gameOverText.text = (winner == Player.Cross) ? "X wins!" : "O wins!";
        }

        StartCoroutine(RestartGame());
    }

    /// <summary>
    /// Coroutine to restart the game after a delay.
    /// </summary>
    /// <returns>IEnumerator for the coroutine.</returns>
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Switch the current player and trigger the computer's move if it's the computer's turn.
    /// </summary>
    private void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == Player.Cross) ? Player.Circle : Player.Cross;

        if (currentPlayer == Player.Circle) // Assuming the computer plays as Circle (O)
        {
            ComputerAI computerAI = FindObjectOfType<ComputerAI>();
            if (computerAI != null)
            {
                computerAI.MakeMove();
            }
        }
    }
}