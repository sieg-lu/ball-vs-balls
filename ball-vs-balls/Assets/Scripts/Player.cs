using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    // ----------- private variables

    private int mMyId;
    protected GameObject mSceneManager;

    protected ePlayerType isController = ePlayerType._playerTypeInvalid;

    // ----------- public variables

    public int startGridX;
    public int startGridZ;

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

    public virtual void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        mMyId = inId;
        mSceneManager = inSceneManager;
    }

    public virtual void Update2()
    {

    }

    // Do NOT use these functions, use Initialize2() and Update2(), as they are called in
    // scene manager, in this way we can unite the initialize and update functions
    void Start()
    {
        // Only used for asserts
        Assert.IsTrue(mSceneManager != null);
        Assert.IsTrue(mSceneManager.GetComponent<GameSceneManager>() != null);
    }
	
	void Update()
    {
    
    }
}
