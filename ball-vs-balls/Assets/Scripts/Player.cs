using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    // ----------- private variables

    private int mMyId;
    private GameObject mSceneManager;

    protected ePlayerType isController = ePlayerType._playerTypeInvalid;

    // ----------- public variables

    public int startGridX;
    public int startGridZ;

    public GameObject sceneManager;

    public enum ePlayerType
    {
        _playerTypeInvalid= 0,
        _playerTypeController,
        _playerTypeAI,
    };

    // ----------- helper functions
    
    public bool IsController()
    {
        Assert.IsTrue(isController != ePlayerType._playerTypeInvalid);

        return isController == ePlayerType._playerTypeController;
    }

    // ----------- callback functions

    public virtual void Initialize2(int myId, GameObject sceneManager)
    {
        mMyId = myId;
        mSceneManager = sceneManager;
    }

    public void Update2()
    {

    }

    // Do NOT use these functions, use Initialize2() and Update2(), as they are called in
    // scene manager, in this way we can unite the initialize and update functions
    void Start()
    {
        // Only used for asserts
        Assert.IsTrue(sceneManager != null);
        Assert.IsTrue(sceneManager.GetComponent<GameSceneManager>() != null);
	}
	
	void Update()
    {
	
	}
}
