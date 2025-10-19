using UnityEngine;

public class MazeTest : MonoBehaviour
{
    public int sizeX = 11;
    public int sizeY = 11;
    public int testInt;
    public bool testBool;
    public Vector3 testVector3;

    private int testIntPrivate;
    protected int testIntProtected;
    [SerializeField]
    protected int testIntPrivate2;

    public StructTest StructTestElement;

    [System.Serializable]

    public struct StructTest
    {
        public float ValueF;
        public Quaternion ValueQuat;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("迷路の初期化");
        var maze = new MazeUtility();
        if(maze.GenerateMaze(sizeX, sizeY))
        {
            Debug.Log("初期化成功");
            Debug.Log(maze.GetMazeString());
            return;
        }
        else
        {
            Debug.LogError("初期化失敗");
        }
        maze.GenerateMaze(sizeX, sizeY);
        Debug.Log(maze.GetMazeString()); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
