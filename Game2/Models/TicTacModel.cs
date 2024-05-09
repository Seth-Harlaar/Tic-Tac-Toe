using Microsoft.VisualBasic;

namespace Game2.Models
{
    public class TicTacModel
    {
        public bool playersTurn = true;
        public bool gameOver = false;
        public bool playerWon = false;
        public int gameId;
        public int[] winningArray = [-1,-1,-1];

        private int oCount = 0;
        private int xCount = 0;
        private int[] gameIdArray = [];

        public TicTacModel(int gameId)
        {
            this.gameId = gameId;
            getMoveCounts();
            setupGame();
        }


        // check for game over, win, and whos turn
        public void setupGame()
        {
            int winner = 0;
            
            // check cols
            for(int i = 0; i < 3; i++)
            {
                winner = checkArray([i, i+3, i+6]);
                if(winner != 1)
                {
                    gameOver = true;
                    playerWon = (winner == 2 ? true : false);
                    return;
                }
            }
            
            // check rows
            for(int i = 0; i < 7; i+=3)
            {
                winner = checkArray([i, i+1, i+2]);
                if (winner != 1)
                {
                    gameOver = true;
                    playerWon = (winner == 2 ? true : false);
                    return;
                }
            }

            // check diagonals
            winner = checkArray([0, 4, 8]);
            if (winner != 1)
            {
                gameOver = true;
                playerWon = (winner == 2 ? true : false);
                return;
            }



            winner = checkArray([2, 4, 6]);
            if (winner != 1)
            {
                gameOver = true;
                playerWon = (winner == 2 ? true : false);
                return;
            }




            // check for game over
            if (xCount + oCount == 9)
            {
                gameOver = true;
            }

            // whos turn
            if (xCount > oCount)
            {
                playersTurn = false;
            } else
            {
                playersTurn = true;
            }
        }


        private void getMoveCounts()
        {
            gameIdArray = gameId.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

            foreach (int num in gameIdArray)
            {
                if (num == 2)
                {
                    xCount++;
                }
                if (num == 3)
                {
                    oCount++;
                }
            }
        }

        // checks an array for a win
        // 1 - no win, 2 - player win, 3 - computer win
        private int checkArray(int[] row)
        {
            int firstSymbol = gameIdArray[row[0]];

            if(firstSymbol == 1)
            {
                return 1;
            }
            
            if(gameIdArray[row[1]] != firstSymbol || gameIdArray[row[2]] != firstSymbol)
            {
                return 1;
            } else
            {
                winningArray = row;
                return firstSymbol;
            }
        }

    }
}
