using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dulo.GameObjects.Labyrinth
{
    public class LabyrinthGenerator
    {
        public LabyrinthMap Map { get; private set; }

        public LabyrinthGenerator(int CountColl, int countRow)
        {
            Map = new LabyrinthMap(CountColl, countRow);/// TODO: Леха!!
            DestroyWall(0, 0, new Random(), new Stack());
        }

        private void DestroyWall(int i0, int j0, Random rnd, Stack stack)
        {

            Map[i0, j0] = CellState.VisitedCell;

            Indexes[] ind = Map.GetUnvisitedCells(i0, j0);

            if (ind.Length == 0)
            {
                Indexes prev = (Indexes)stack.GetAndRemoveFirstItem();

                if (stack.Length > 0)
                    DestroyWall(prev.i, prev.j, rnd, stack);

                return;
            }

            stack.Add(new Indexes(i0, j0));

            Indexes d = GetRandomItemFromArray(ref ind, rnd);

            Map[i0 + d.i / 2, j0 + d.j / 2] = CellState.VisitedCell;

            DestroyWall(i0 + d.i, j0 + d.j, rnd, stack);
        }

        private Indexes GetRandomItemFromArray(ref Indexes[] array, Random rnd)
        {
            int i0 = rnd.Next(array.Length);
            Indexes Result = array[i0];

            for (int i = i0; i < array.Length - 1; i++)
                array[i] = array[i + 1];

            Array.Resize(ref array, array.Length - 1);

            return Result;
        }
    }
}
