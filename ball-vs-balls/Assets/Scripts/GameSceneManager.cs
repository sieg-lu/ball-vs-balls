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

    public GameObject[] players;

    // ----------- helper functions

    private bool IsPlayerGrid(int gridX, int gridZ, out int playerId)
    {
        bool result = false;
        playerId = -1;

        for (int playerIndex= 0; !result && playerIndex < players.Length; playerIndex++)
        {
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

        Assert.IsTrue(normalBrick != null);
//        Assert.IsTrue(wallBrick != null);

        float start_x = (mNormalBrickSize.x + kIntervalBetweenBlocks) * (-width / 2.0f);
        float start_z = (mNormalBrickSize.z + kIntervalBetweenBlocks) * (-height / 2.0f);
        float end_x = (mNormalBrickSize.x + kIntervalBetweenBlocks) * (width / 2.0f);
        float end_z = (mNormalBrickSize.z + kIntervalBetweenBlocks) * (height / 2.0f);

        Assert.IsTrue(mBricksRoot == null);
        mBricksRoot = new GameObject();
        mBricksRoot.transform.position = this.transform.position;

        int i = 0;
        int j = 0;
        float current_x = start_x;
        float current_z = start_z;

        for (i = 0, current_x = start_x;
             i < width;
             i++, current_x += (mNormalBrickSize.x + kIntervalBetweenBlocks))
        {
            for (j = 0, current_z = start_z;
                 j < height;
                 j++, current_z += (mNormalBrickSize.z + kIntervalBetweenBlocks))
            {
                int playerId = -1;
                bool isPlayerGrid = IsPlayerGrid(i, j, out playerId);
                
                Vector3 position = new Vector3(current_x, mNormalBrickSize.y / 2.0f + 0.01f, current_z);
                if (isPlayerGrid)
                {
                    Assert.IsTrue(playerId != -1);

                    GameObject currentPlayer = (GameObject)Instantiate(players[playerId], position, Quaternion.identity);
                    Player playerComponent = players[playerId].GetComponent<Player>();

                    currentPlayer.name = playerComponent.IsController() ? "Control" : "Enemy " + playerId;
                }
                else
                {
                    GameObject currentBrick = (GameObject)Instantiate(normalBrick, position, Quaternion.identity);

                    currentBrick.name = "brick" + (i * height + j + 1).ToString();
                    currentBrick.transform.parent = mBricksRoot.transform;
                }
            }
        }
    }

    void Initialize2()
    {
        mNormalBrickSize = normalBrick.transform.localScale;
        Debug.Log("normal brick's size: " + mNormalBrickSize.ToString());
    }
    
    // ----------- main functions
    void Start()
    {
        // Initialize section
        Initialize2();

        for (int playerIndex= 0; playerIndex < players.Length; playerIndex++)
        {
            players[playerIndex].GetComponent<Player>().Initialize2(playerIndex, gameObject);
        }

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
}
