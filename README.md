I created this chess bot as a small hobby project at home, out of interest and to familiarise myself with C#. It was inpired by and based on a [challenge](https://github.com/SebLague/Chess-Challenge) created by Sebastian Lague, who has developed the entire frameork, including the logic to implement the rules of chess, helpers to get the state of the board and a GUI to play or watch the games. I have only written the code in MyBot.cs and the credit for everything else goes to Sebastian Lague. The rules of the challange included limitations like maximum memory usage, no accessing the internet or external files, no multithreding and a limit on program size measured in "tokens" (variables, functions, etc.). My bot is based on the Minimax search algorithm combined with my own intuition of what constitutes an advantageous chess position, but some ideas are not yet implemented.

Key features:
- Minimax search to look into the future and find the best move for each player.
- Alpha-beta pruning to decrease number of visited nodes and optimise search efficiency.
- Heuristic evaluation of board state when maximum search depth is reached.
