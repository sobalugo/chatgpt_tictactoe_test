// using System.Collections.Generic;
// using UnityEngine;

// public class ComputerAI : MonoBehaviour
// {
//     public GameManager gameManager;

//     public void MakeMove()
//     {
//         List<Vector2Int> availableMoves = GetAvailableMoves();
//         if (availableMoves.Count > 0)
//         {
//             int randomIndex = Random.Range(0, availableMoves.Count);
//             Vector2Int move = availableMoves[randomIndex];
//             gameManager.MakeMove(move.x, move.y);
//         }
//     }

//     private List<Vector2Int> GetAvailableMoves()
//     {
//         List<Vector2Int> moves = new List<Vector2Int>();
//         for (int x = 0; x < 3; x++)
//         {
//             for (int y = 0; y < 3; y++)
//             {
//                 if (gameManager.board[x, y] == GameManager.Player.None)
//                 {
//                     moves.Add(new Vector2Int(x, y));
//                 }
//             }
//         }
//         return moves;
//     }
// }