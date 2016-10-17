namespace HorseRide
{
    internal class RideHorseProgram
    {
        private const int InitialCellValue = 1;
        private static void Main(string[] args)
        {
            HorseMoveSetter moveSetter = new HorseMoveSetter(InitialCellValue);
            moveSetter.TraverseMatrixBfsHorseLikeMove();
            moveSetter.PrintMiddleColumn();
        }
    }
}
