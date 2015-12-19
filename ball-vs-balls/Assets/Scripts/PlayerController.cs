using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : Player
{
    // ----------- private variables
    
    // ----------- public variables

    // ----------- helper functions
    
    public void PlayerMove()
    {
        // Get the axis and jump input.
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        Transform cam = Camera.main.transform;

        mJump = CrossPlatformInputManager.GetButton("Jump");

        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        mMove = (v * camForward + h * cam.right).normalized;
    }

    private void FixCamera()
    {
        GameSceneManager sceneManagerScript = mSceneManager.GetComponent<GameSceneManager>();
        float distanceAwayFromController = sceneManagerScript.distanceAwayFromController;
        float angleInDegreeBetweenCameraAndHorizon = sceneManagerScript.angleInDegreeBetweenCameraAndHorizon;
        Camera camera = Camera.main;

//        Debug.Log(sceneManagerScript.players[0].transform.position);

        camera.gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y + distanceAwayFromController * Mathf.Sin(Mathf.Deg2Rad * angleInDegreeBetweenCameraAndHorizon),
            gameObject.transform.position.z - distanceAwayFromController * Mathf.Cos(Mathf.Deg2Rad * angleInDegreeBetweenCameraAndHorizon));
        camera.gameObject.transform.LookAt(gameObject.transform);
    }

    protected override void PostSpawnSpotLight()
    {
        mLightComponent.color = Color.white;
    }

    // ----------- main functions

    public override void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        base.Initialize2(inId, inSceneManager);
        isController = ePlayerType._playerTypeController;

        Assert.IsTrue(mSceneManager != null);
        Assert.IsTrue(Camera.main != null);
        
        PostSpawnSpotLight();
    }

    public override void Update2()
    {
        base.Update2();
        PlayerMove();
    }

    public override void SyncUpdate2()
    {
        base.SyncUpdate2();
        FixCamera();
    }

    // Do NOT use these functions, use Initialize2() and Update2(), as they are called in
    // scene manager, in this way we can unite the initialize and update functions
    void Start()
    {
        // Only used for asserts
    }

    void Update()
    {
	
	}
}
