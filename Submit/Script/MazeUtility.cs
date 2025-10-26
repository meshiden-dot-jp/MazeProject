using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUtility
{
    private ElementType[][] maze;
    private readonly List<Point> dugPoints = new List<Point>();

    public ElementType[][] Maze => maze;

    public enum ElementType
    {
        Wall,
        Road,
        Start,
        Goal
    }
    
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    private struct  Point
    {
        public int X;
        public int Y;
    }

    public int StartX => start_x;
    public int StartY => start_y;
    public int GoalX => goal_x;
    public int GoalY => goal_y;
    
    private int start_x;
    private int start_y;
    private int goal_x;
    private int goal_y;

    public bool GenerateMaze(int width, int height)
    {
        // 迷路を穴掘り法で生成する
        // 1. すべての領域を壁にする
        // 2. 領域内から縦横それぞれ奇数のインデックスを選び、そこを道にする
        // 3. 道になった領域の上下左右の領域を選び、そこを道にする。道に出来るかどうかの条件は選択した方角の2つ先のマスが領域内で壁であること
        // 4. 3をどの方角も道に出来なくなるまで繰り返す
        // 5. 3~4で掘り進んだ道のうちから奇数のインデックスをランダムに選び、そこから更に掘り進める。もし掘り進めることが出来る場所がなくなった場合は処理を終了する

        if(width % 2 == 0 || height % 2 == 0)
        {
            Debug.LogError("迷路のサイズは奇数である必要があります");
            return false;
        }
        
        maze = new ElementType[width][];
        for (var i = 0; i < width; i++)
        {
            maze[i] = new ElementType[height];
            for (var j = 0; j < height; j++)
            {
                maze[i][j] = ElementType.Wall;
            }
        }

        do
        {
            start_x = Random.Range(1, width - 1);
        } while (start_x % 2 == 0);

        do
        {
            start_y = Random.Range(1, height - 1);
        } while (start_y % 2 == 0);
        
        Dig(0, start_x, start_y);
        
        maze[start_x][start_y] = ElementType.Start;
        maze[goal_x][goal_y] = ElementType.Goal;

        return true;
    }

    private void Dig(int depth, int x, int y)
    {
        maze[x][y] = ElementType.Road;
        
        while (true)
        {
            var diggerbleDirections = new List<Direction>();
            if (x - 2 > 0 && maze[x - 2][y] == ElementType.Wall)
            {
                diggerbleDirections.Add(Direction.Left);
            }

            if (x + 2 < maze.Length && maze[x + 2][y] == ElementType.Wall)
            {
                diggerbleDirections.Add(Direction.Right);
            }

            if (y - 2 > 0 && maze[x][y - 2] == ElementType.Wall)
            {
                diggerbleDirections.Add(Direction.Down);
            }

            if (y + 2 < maze[x].Length && maze[x][y + 2] == ElementType.Wall)
            {
                diggerbleDirections.Add(Direction.Up);
            }

            if (diggerbleDirections.Count == 0)
            {
                if (dugPoints.Count > 0)
                {
                    if (depth == 0)
                    {
                        goal_x = x;
                        goal_y = y;
                    }

                    var index = Random.Range(0, dugPoints.Count);
                    var point = dugPoints[index];
                    dugPoints.RemoveAt(index);
                    
                    Dig(depth + 1, point.X, point.Y);
                }
                else
                {
                    return;
                }
            }
            else
            {

                var direction = diggerbleDirections[Random.Range(0, diggerbleDirections.Count)];
                switch (direction)
                {
                    case Direction.Up:
                        maze[x][y + 1] = ElementType.Road;
                        maze[x][y + 2] = ElementType.Road;
                        y += 2;
                        dugPoints.Add(new Point { X = x, Y = y });
                        break;
                    case Direction.Down:
                        maze[x][y - 1] = ElementType.Road;
                        maze[x][y - 2] = ElementType.Road;
                        y -= 2;
                        dugPoints.Add(new Point { X = x, Y = y });
                        break;
                    case Direction.Left:
                        maze[x - 1][y] = ElementType.Road;
                        maze[x - 2][y] = ElementType.Road;
                        x -= 2;
                        dugPoints.Add(new Point { X = x, Y = y });
                        break;
                    case Direction.Right:
                        maze[x + 1][y] = ElementType.Road;
                        maze[x + 2][y] = ElementType.Road;
                        x += 2;
                        dugPoints.Add(new Point { X = x, Y = y });
                        break;
                }
            }
        }
    }

    public string GetMazeString()
    {
        var str = "";
        foreach (var m1 in Maze)
        {
            foreach (var m2 in m1)
            {
                switch (m2)
                {
                    case ElementType.Start:
                        str += "△";
                        break;
                    case ElementType.Goal:
                        str += "▽";
                        break;
                    case ElementType.Road:
                        str += "□";
                        break;
                    case ElementType.Wall:
                        str += "■";
                        break;
                }
            }
            str += System.Environment.NewLine;
        }

        return str;
    }
}
