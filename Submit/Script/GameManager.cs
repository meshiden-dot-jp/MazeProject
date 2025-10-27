using UnityEngine; 

namespace MazeProject
{
    public class GameManager : MonoBehaviour
    {
        public int MazeWidth = 11;
        public int MazeHeight = 11;

        public Transform MazeRoot;
        public GameObject WallPrefab;
        public GameObject FloorPrefab;
        public float WallSize = 1f;

        public MainUI MainUI;

        public Player PlayerPrefab;
        public GameObject GoalPrefab;

        public CameraManager CameraManager;
        private readonly MazeUtility mazeUtility = new();
        public Player Player { get; private set; }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CreateMaze();
            Player = Instantiate(
                PlayerPrefab,
                new Vector3(mazeUtility.StartX * WallSize, -0.5f, mazeUtility.StartY * WallSize),
                Quaternion.identity
                );
            MainUI.Player = Player;
            CameraManager.Follow = Player.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CreateMaze()
        {
            if (!mazeUtility.GenerateMaze(MazeWidth, MazeHeight))
            {
                Debug.LogError("迷路生成に失敗しました。");
                return;
            }
            if (WallPrefab == null)
            {
                Debug.LogError("WallPrefabが空です。");
                return;
            }
            for (var i = 0; i < MazeWidth; i++)
            {
                for (var j = 0; j < MazeHeight; j++)
                {
                    if (mazeUtility.Maze[i][j] == MazeUtility.ElementType.Wall)
                    {
                        Instantiate(
                        WallPrefab,
                        new Vector3(i * WallSize, 0, j * WallSize),
                        Quaternion.identity,
                        MazeRoot
                        );
                    }

                }
            }

            if (FloorPrefab == null)
            {
                Debug.LogError("FloorPrefabが空です。");
            }

            var floor = Instantiate(
                FloorPrefab,
                new Vector3((MazeWidth - 1) * WallSize * 0.5f, -0.5f, (MazeHeight - 1) * WallSize * 0.5f),
                Quaternion.identity,
                MazeRoot
                );
            floor.transform.localScale = new Vector3(
                MazeWidth * WallSize / 10f,
                0,
                MazeHeight * WallSize / 10f
                );
            floor.GetComponent<Renderer>().material.mainTextureScale = new Vector2(
                MazeWidth * WallSize,
                MazeHeight * WallSize
                );
            Instantiate(
                GoalPrefab,
                new Vector3(mazeUtility.GoalX * WallSize, 0, mazeUtility.GoalY * WallSize),
                Quaternion.identity,
                MazeRoot
                );
        }
    }
}