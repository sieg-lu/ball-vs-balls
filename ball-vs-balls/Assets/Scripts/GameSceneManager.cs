using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class GameSceneManager : MonoBehaviour
{
    // ----------- private variables
    private const int kMaxWidth = 100;
    private const int kMaxHeight = 100;
    private const float kIntervalBetweenBlocks = 0.05f;

    private GameObject mBricksRoot = null;
    private Vector3 mNormalBrickSize;

    // ----------- public variables
    public GameObject normalBrick;
    public GameObject wallBrick;

    public int width = 50;
    public int height = 50;

    public float distanceAwayFromController = 3.14f;
    public float angleInDegreeBetweenCameraAndHorizon = 70.0f;

    // Used prefabs, but make them into real GameObject at runtime (SetupScene())
    public GameObject[] players;

    // ----------- helper functions

    private bool IsPlayerGrid(
        int gridX,
        int gridZ,
        out int playerId)
    {
        bool result = false;
        playerId = -1;

        for (int playerIndex= 0; !result && playerIndex < players.Length; playerIndex++)
        {
            // Hacky, get the Player component before it's setup
            Player playerComponent = players[playerIndex].GetComponent<Player>();
            if (gridX == playerComponent.startGridX && gridZ == playerComponent.startGridZ)
            {
                playerId = playerIndex;
                result = true;
            }
        }

        return result;
    }

    private void SetupScene()
    {
        Assert.IsTrue(width > 0 && width < kMaxWidth);
        Assert.IsTrue(height > 0 && height < kMaxHeight);

        float startX = (mNormalBrickSize.x + kIntervalBetweenBlocks) * (-width / 2.0f);
        float startZ = (mNormalBrickSize.z + kIntervalBetweenBlocks) * (-height / 2.0f);
        float endX = (mNormalBrickSize.x + kIntervalBetweenBlocks) * (width / 2.0f);
        float endZ = (mNormalBrickSize.z + kIntervalBetweenBlocks) * (height / 2.0f);

        Assert.IsTrue(mBricksRoot == null);
        mBricksRoot = new GameObject();
        mBricksRoot.transform.position = this.transform.position;

        int i = 0;
        int j = 0;
        float currentX = startX;
        float currentZ = startZ;

        for (i = 0, currentX = startX;
             i < width;
             i++, currentX += (mNormalBrickSize.x + kIntervalBetweenBlocks))
        {
            for (j = 0, currentZ = startZ;
                 j < height;
                 j++, currentZ += (mNormalBrickSize.z + kIntervalBetweenBlocks))
            {
                int playerId = -1;
                bool isPlayerGrid = IsPlayerGrid(i, j, out playerId);
                
                Vector3 position = new Vector3(currentX, mNormalBrickSize.y / 2.0f + 0.01f, currentZ);
                if (isPlayerGrid)
                {
                    Assert.IsTrue(playerId != -1);

                    players[playerId] = (GameObject)Instantiate(players[playerId], position, Quaternion.identity);
                    Player playerComponent = players[playerId].GetComponent<Player>();

                    playerComponent.Initialize2(playerId, gameObject);
                    players[playerId].name = playerComponent.IsController() ? "Control" : "Enemy " + playerId;
                }
                else
                {
//                     GameObject currentBrick = (GameObject)Instantiate(normalBrick, position, Quaternion.identity);
// 
//                     currentBrick.name = "brick" + (i * height + j + 1).ToString();
//                     currentBrick.transform.parent = mBricksRoot.transform;
                }
            }
        }
    }

    void Initialize2()
    {
        Assert.IsTrue(normalBrick != null);
//        Assert.IsTrue(wallBrick != null);

        mNormalBrickSize = normalBrick.transform.localScale;
        Debug.Log("normal brick's size: " + mNormalBrickSize.ToString());
    }
    
    // ----------- main functions
    void Start()
    {
        // Initialize section
        Initialize2();
        
        // Gameplay section
        SetupScene();
    }

    void Update()
    {
        for (int playerIndex = 0; playerIndex < players.Length; playerIndex++)
        {
            players[playerIndex].GetComponent<Player>().Update2();
        }
    }

    void FixedUpdate()
    {
        for (int playerIndex = 0; playerIndex < players.Length; playerIndex++)
        {
            players[playerIndex].GetComponent<Player>().SyncUpdate2();
        }
    }
}
