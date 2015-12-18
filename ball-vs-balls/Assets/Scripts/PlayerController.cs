using UnityEngine;
using System.Collections;

public class PlayerController : Player
{
    public override void Initialize2(int myId, GameObject sceneManager)
    {
        isController = ePlayerType._playerTypeController;
        base.Initialize2(myId, sceneManager);
    }

    void Start()
    {
        
	}
	
	void Update()
    {
	
	}
}
