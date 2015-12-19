using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class PlayerController : Player
{
    // ----------- private variables

    // ----------- public variables

    // ----------- helper functions

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

    // ----------- main functions

    public override void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        base.Initialize2(inId, inSceneManager);
        isController = ePlayerType._playerTypeController;

        Assert.IsTrue(mSceneManager != null);
        Assert.IsTrue(Camera.main != null);
    }

    public override void Update2()
    {
        FixCamera();
        base.Update2();
    }

    void Start()
    {
        
	}
	
	void Update()
    {
	
	}
}
