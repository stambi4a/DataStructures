namespace HorseRide
{
    using System;
    using System.Collections.Generic;

    public class HorseMoveSetter
    {
        public HorseMoveSetter(int value)
        {
            this.InputMatrix();
            this.InputInitialCell(value);
            this.CurrentCell = this.InitialCell;
        }
        public Matrix Matrix { get; set; }

        public Cell<int> CurrentCell { get; set; }

        public Cell<int> InitialCell { get; set; }


        private void InputMatrix()
        {
            int rows = int.Parse(Console.ReadLine());
            int columns = int.Parse(Console.ReadLine());
            this.Matrix = new Matrix(rows, columns);
        }

        private void InputInitialCell(int value)
        {
            int cellRow = int.Parse(Console.ReadLine());
            int cellColumn = int.Parse(Console.ReadLine());
            this.InitialCell = this.Matrix[cellRow, cellColumn];
            this.InitialCell.Value = value;
        }

        public void TraverseMatrixBfsHorseLikeMove()
        {
            Queue<Cell<int>> horseMoveCells = new Queue<Cell<int>>();
            horseMoveCells.Enqueue(this.CurrentCell);
            while (horseMoveCells.Count > 0)
            {
                this.CurrentCell = horseMoveCells.Dequeue();
                this.TryDirectionDownLeft();
                this.TryDirectionDownRight();
                this.TryDirectionRightDown();
                this.TryDirectionLeftDown();
                this.TryDirectionLeftUp();
                this.TryDirectionUpRight();
                this.TryDirectionUpLeft();
                this.TryDirectionRightUp();
                if (this.CurrentCell.Children.Count > 0)
                {
                    foreach (var child in this.CurrentCell.Children)
                    {
                        horseMoveCells.Enqueue(child);
                        child.Value = this.CurrentCell.Value + 1;
                    }
                }  
            }
                   
        }

        private void TryDirection(int deltaX, int deltaY)
        {
            int nextCellRow = this.CurrentCell.Row + deltaX;
            int nextCellColumn = this.CurrentCell.Column + deltaY;
            if (nextCellRow >= 0 
                && nextCellRow < this.Matrix.Height 
                && nextCellColumn >= 0
                && nextCellColumn < this.Matrix.Width 
                && this.Matrix[nextCellRow, nextCellColumn].Value == 0)
            {
                this.CurrentCell.Children.Add(this.Matrix[nextCellRow, nextCellColumn]);
            }
        
}

        private void TryDirectionUpRight()
        {
            this.TryDirection(-2, 1);
        }

        private void TryDirectionRightUp()
        {
            this.TryDirection(-1, 2);
        }

        private void TryDirectionRightDown()
        {
            this.TryDirection(1, 2);
        }

        private void TryDirectionDownRight()
        {
            this.TryDirection(2, 1);
        }

        private void TryDirectionDownLeft()
        {
            this.TryDirection(2, -1);
        }

        private void TryDirectionLeftDown()
        {
            this.TryDirection(1, -2);
        }

        private void TryDirectionLeftUp()
        {
            this.TryDirection(-1, -2);
        }

        private void TryDirectionUpLeft()
        {
            this.TryDirection(-2, -1);
        }

        public void PrintMiddleColumn()
        {
            int middleColumn = this.Matrix.Width / 2;
            List<int> middleColumnValues = new List<int>(this.Matrix.Height);
            for (int i = 0; i < this.Matrix.Height; i++)
            {
                middleColumnValues.Add(this.Matrix[i, middleColumn].Value);
            }

            Console.WriteLine($"{string.Join("\n", middleColumnValues)}");
        }
    }
}