using ChessChallenge.API;
using System;
using System.Security;

public class MyBot : IChessBot
{
    private int MaxDepth = 4;

    public Move Think(Board board, Timer timer)
    {
        bool playsWhite = board.IsWhiteToMove;
        Move bestMove = Move.NullMove;
        int bestScore = int.MinValue;
        foreach (Move move in board.GetLegalMoves())
        {
            board.MakeMove(move);
            int moveScore = MinPlayer(board, playsWhite, bestScore, int.MaxValue, 1);
            if (moveScore > bestScore)
            {
                bestScore = moveScore;
                bestMove = move;
            }
            board.UndoMove(move);
        }
        return bestMove;
    }

    private int MinPlayer(Board board, bool playsWhite, int alpha, int beta, int depth)
    {
        if (board.IsInCheckmate())
        {
            return 1000;
        }
        else if (board.IsDraw())
        {
            return 0;
        }
        else if (depth == MaxDepth)
        {
            return Evaluate(board) * (playsWhite ? 1 : -1);
        }
        int bestScore = int.MaxValue;
        foreach (Move move in board.GetLegalMoves())
        {
            board.MakeMove(move);
            bestScore = Math.Min(bestScore, MaxPlayer(board, playsWhite, alpha, beta, depth + 1));
            beta = Math.Min(beta, bestScore);
            if (bestScore <= alpha)
            {
                board.UndoMove(move);
                break;
            }
            board.UndoMove(move);
        }
        return bestScore;
    }

    private int MaxPlayer(Board board, bool playsWhite, int alpha, int beta, int depth)
    {
        if (board.IsInCheckmate())
        {
            return -1000;
        }
        else if (board.IsDraw())
        {
            return 0;
        }
        else if (depth == MaxDepth)
        {
            return Evaluate(board) * (playsWhite ? 1 : -1);
        }
        int bestScore = int.MinValue;
        foreach (Move move in board.GetLegalMoves())
        {
            board.MakeMove(move);
            bestScore = Math.Max(bestScore, MinPlayer(board, playsWhite, alpha, beta, depth + 1));
            alpha = Math.Max(alpha, bestScore);
            if (bestScore >= beta)
            {
                board.UndoMove(move);
                break;
            }
            board.UndoMove(move);
        }
        return bestScore;
    }

    private static Move[] OrderMoves(Move[] moves)
    {
        
        return moves;
    }

    private static int Evaluate(Board board)
    {
        ulong whitePawns = board.GetPieceBitboard(PieceType.Pawn, true);
        ulong whiteKnights = board.GetPieceBitboard(PieceType.Knight, true);
        ulong whiteBishops = board.GetPieceBitboard(PieceType.Bishop, true);
        ulong whiteRooks = board.GetPieceBitboard(PieceType.Rook, true);
        ulong whiteQueens = board.GetPieceBitboard(PieceType.Queen, true);
        ulong blackPawns = board.GetPieceBitboard(PieceType.Pawn, false);
        ulong blackKnights = board.GetPieceBitboard(PieceType.Knight, false);
        ulong blackBishops = board.GetPieceBitboard(PieceType.Bishop, false);
        ulong blackRooks = board.GetPieceBitboard(PieceType.Rook, false);
        ulong blackQueens = board.GetPieceBitboard(PieceType.Queen, false);

        int whiteMaterial = BitboardHelper.GetNumberOfSetBits(whitePawns)
        + 3 * BitboardHelper.GetNumberOfSetBits(whiteKnights)
        + 3 * BitboardHelper.GetNumberOfSetBits(whiteBishops)
        + 5 * BitboardHelper.GetNumberOfSetBits(whiteRooks)
        + 9 * BitboardHelper.GetNumberOfSetBits(whiteQueens);

        int blackMaterial = BitboardHelper.GetNumberOfSetBits(blackPawns)
        + 3 * BitboardHelper.GetNumberOfSetBits(blackKnights)
        + 3 * BitboardHelper.GetNumberOfSetBits(blackBishops)
        + 5 * BitboardHelper.GetNumberOfSetBits(blackRooks)
        + 9 * BitboardHelper.GetNumberOfSetBits(blackQueens);
        
        int whiteNbrMoves = 0;
        int blackNbrMoves = 0;
        int whiteInCheck = 0;
        int blackInCheck = 0;
        if (board.IsWhiteToMove)
        {
            whiteNbrMoves = board.GetLegalMoves().Length;
            whiteInCheck = board.IsInCheck() ? 1 : 0;
            bool skipped = board.TrySkipTurn();
            blackNbrMoves = board.GetLegalMoves().Length;
            blackInCheck = board.IsInCheck() ? 1 : 0;
            if (skipped)
            {
                board.UndoSkipTurn();
            }
        } 
        else
        {
            blackNbrMoves = board.GetLegalMoves().Length;
            blackInCheck = board.IsInCheck() ? 1 : 0;
            bool skipped = board.TrySkipTurn();
            whiteNbrMoves = board.GetLegalMoves().Length;
            whiteInCheck = board.IsInCheck() ? 1 : 0;
            if (skipped)
            {
                board.UndoSkipTurn();
            }
        }

        PieceList[] allPieces = board.GetAllPieceLists();
        int whiteAttackingValue = 0;
        int blackAttackingValue = 0;
        int whiteProtection = 0;
        int blackProtection = 0;
        
        foreach (PieceList list in allPieces)
        {
            foreach (Piece piece in list)
            {
                if (piece.IsWhite)
                {
                    whiteAttackingValue += (BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & blackPawns)
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & blackKnights)
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & blackBishops)
                    + 5 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & blackRooks)
                    + 9 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & blackQueens));
                    whiteProtection += BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, true) & board.WhitePiecesBitboard);
                    
                }
                else
                {
                    blackAttackingValue += (BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whitePawns) 
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteKnights)
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteBishops)
                    + 5 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteRooks)
                    + 9 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteQueens));
                    blackProtection += BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & board.BlackPiecesBitboard);
                }
            }
        }

        return 5 * (whiteMaterial - blackMaterial)
        + whiteNbrMoves - blackNbrMoves
        - whiteInCheck + blackInCheck
        + whiteAttackingValue - blackAttackingValue 
        + whiteProtection - blackProtection;
    }
}