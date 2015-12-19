using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Ball;

public abstract class Player : MonoBehaviour
{
    // ----------- private variables

    private int mMyId;
    protected GameObject mSceneManager;

    protected ePlayerType isController = ePlayerType._playerTypeInvalid;
    protected GameObject mLight;
    protected Light mLightComponent;

    private Ball mBall;
    protected Vector3 mMove;
    protected bool mJump = false;

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

    protected void SpawnSpotLight()
    {
        mLight = new GameObject(name + "Light");
        mLight.transform.Rotate(new Vector3(1.0f, 0, 0), 90.0f);
        mLightComponent = mLight.AddComponent<Light>();
        mLightComponent.type = LightType.Spot;
        mLightComponent.spotAngle = 30.0f;
        mLightComponent.transform.position = gameObject.transform.position;
    }

    protected abstract void PostSpawnSpotLight();

    protected void FixSpotLightPosition()
    {
        mLight.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 2.0f,
            transform.position.z);
    }

    // ----------- main functions

    public virtual void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        mMyId = inId;
        mSceneManager = inSceneManager;

        mBall = GetComponent<Ball>();
        mBall.Initialize2();

        SpawnSpotLight();
    }

    public virtual void Update2()
    {

    }

    public virtual void SyncUpdate2()
    {
        FixSpotLightPosition();
        mBall.Move(mMove, mJump);
        mJump = false;
    }

    // Do NOT use these functions, use Initialize2() and Update2(), as they are called in
    // scene manager, in this way we can unite the initialize and update functions
    void Start()
    {
        // Only used for asserts
        Assert.IsTrue(mSceneManager != null);
        Assert.IsTrue(mSceneManager.GetComponent<GameSceneManager>() != null);
        Assert.IsTrue(GetComponent<Rigidbody>() != null);
    }
	
	void Update()
    {
    
    }
}
