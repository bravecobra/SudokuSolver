﻿using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
    public enum SudokuRegion
    {
        Row,
        Column,
        Block
    }

    public class Region
    {
        public readonly SPoint[] Points;
        public readonly Cell[] Cells;

        public Region(Board board, SudokuRegion region, int index)
        {
            switch (region)
            {
                case SudokuRegion.Block:
                    Points = new SPoint[9];
                    int ix = (index % 3) * 3, iy = (index / 3) * 3;
                    int c = 0;
                    for (int x = ix; x < ix + 3; x++)
                    {
                        for (int y = iy; y < iy + 3; y++)
                        {
                            Points[c++] = new SPoint(x, y);
                        }
                    }
                    break;
                case SudokuRegion.Row:
                    Points = new SPoint[9];
                    for (int i = 0; i < 9; i++)
                        Points[i] = new SPoint(i, index);
                    break;
                case SudokuRegion.Column:
                    Points = new SPoint[9];
                    for (int i = 0; i < 9; i++)
                        Points[i] = new SPoint(index, i);
                    break;
            }
            Cells = Points.Select(p => board[p]).ToArray();
        }

        public int[] GetRegion() => Cells.Select(c => c.Value).ToArray();
        public HashSet<int>[] GetCandidates() => Cells.Select(c => c.Candidates).ToArray();

        public Cell[] GetCellsWithCandidate(int value) => Cells.Where(c => c.Candidates.Contains(value)).ToArray();
        public SPoint[] GetPointsWithCandidate(int value) => GetCellsWithCandidate(value).Select(c => c.Point).ToArray();
    }
}
