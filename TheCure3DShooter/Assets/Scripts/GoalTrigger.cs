// Robin B
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {

    void OnTriggerEnter( Collider coll ) {

        if( coll.gameObject.tag == Tags.player ) {

            GameManager.instance.ChangeGameState( GameManager.GameState.Victory );
        }
    }
}
