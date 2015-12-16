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

    // ----------- helper functions
    private void SetupScene()
    {
        Assert.IsTrue(width > 0 && width < kMaxWidth);
        Assert.IsTrue(height > 0 && height < kMaxHeight);

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
                Vector3 position = new Vector3(current_x, mNormalBrickSize.y / 2.0f + 0.05f, current_z);
                GameObject current_brick = (GameObject)Instantiate(normalBrick, position, Quaternion.identity);

                current_brick.name = "brick" + (i * height + j + 1).ToString();
                current_brick.transform.parent = mBricksRoot.transform;
            }
        }
    }
    
    // ----------- callback functions
    void Start()
    {
        mNormalBrickSize = normalBrick.transform.localScale;
        Debug.Log("normal brick's size: " + mNormalBrickSize.ToString());

        SetupScene();
    }
	
	void Update()
    {
	
	}
}
