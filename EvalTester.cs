using ChessChallenge.API;
using System;
using System.Security;

public class EvalTester {
    
    public static void Main()
    {
        Console.WriteLine("Hej")
    }

}
/*
private int Evaluate(Board board)
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
        int whiteNbrCaptures = 0;
        int blackNbrCaptures =  0;
        if (board.IsWhiteToMove)
        {
            whiteNbrMoves = board.GetLegalMoves().Length;
            whiteNbrCaptures = board.GetLegalMoves(true).Length;
            bool skipped = board.TrySkipTurn();
            blackNbrMoves = board.GetLegalMoves().Length;
            blackNbrCaptures = board.GetLegalMoves(true).Length;
            if (skipped)
            {
                board.UndoSkipTurn();
            }
        } 
        else
        {
            blackNbrMoves = board.GetLegalMoves().Length;
            bool skipped = board.TrySkipTurn();
            whiteNbrMoves = board.GetLegalMoves().Length;
            if (skipped)
            {
                board.UndoSkipTurn();
            }
        }

        PieceList[] allPieces = board.GetAllPieceLists();
        int whiteAttackingValue = 0;
        int blackAttackingValue = 0;
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
                }
                else
                {
                    blackAttackingValue += (BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whitePawns) 
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteKnights)
                    + 3 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteBishops)
                    + 5 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteRooks)
                    + 9 * BitboardHelper.GetNumberOfSetBits(BitboardHelper.GetPieceAttacks(piece.PieceType, piece.Square, board, false) & whiteQueens));
                }
            }
        }
        return whiteMaterial - blackMaterial + whiteNbrMoves - blackNbrMoves + whiteNbrCaptures - blackNbrCaptures;
    }
    */