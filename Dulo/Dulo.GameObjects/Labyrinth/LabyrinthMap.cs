using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.GameObjects.Labyrinth
{
    public enum CellState
    {
        UnvisitedCell,
        Wall,
        VisitedCell
    };

    public class LabyrinthMap
    {
        CellState[,] map = new CellState[0, 0];

        public int CountColumns { get; private set; }

        public int CountRows { get; private set; }

        public CellState this[int i, int j]
        {
            get
            {
                return map[i, j];
            }

            set
            {
                map[i, j] = value;
            }
        }

        public LabyrinthMap(int CountColumns, int CountRows)
        {
            this.CountColumns = CountColumns;
            this.CountRows = CountRows;
            map = new CellState[CountRows, CountColumns];

            Refresh();
        }

        public void SetCellsLikeUnvisited()
        {
            for (int i = 0; i < CountRows; i++)
                for (int j = 0; j < CountColumns; j++)
                    if (map[i, j] == CellState.VisitedCell)
                        map[i, j] = CellState.UnvisitedCell;
        }

        public void Refresh()
        {
            for (int i = 0; i < CountRows; i++)
                for (int j = 0; j < CountColumns; j++)
                    map[i, j] = CellState.UnvisitedCell;

            for (int i = 1; i < CountRows; i += 2)
                for (int j = 0; j < CountColumns; j++)
                    map[i, j] = CellState.Wall;

            for (int i = 0; i < CountRows; i++)
                for (int j = 1; j < CountColumns; j += 2)
                    map[i, j] = CellState.Wall;
        }

        public Indexes[] GetUnvisitedCells(int i0, int j0, int step = 2)
        {
            Indexes[] Result = new Indexes[0];

            Indexes[] cells = new Indexes[4] { new Indexes(-step, 0), new Indexes(0, step), new Indexes(step, 0), new Indexes(0, -step) };

            for (int i = 0; i < cells.Length; i++)
                if (i0 + cells[i].i >= 0 && j0 + cells[i].j >= 0 && i0 + cells[i].i < CountRows && j0 + cells[i].j < CountColumns)
                    if (map[i0 + cells[i].i, j0 + cells[i].j] == CellState.UnvisitedCell)
                    {
                        Array.Resize(ref Result, Result.Length + 1);
                        Result[Result.Length - 1] = new Indexes(cells[i].i, cells[i].j);
                    }

            return Result;
        }
    }
}
