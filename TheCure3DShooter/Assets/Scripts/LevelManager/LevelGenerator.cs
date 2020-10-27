using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : LevelManager {

    public void GenerateTunnel() {

        Vector3 lastPosition;
        lastPosition = Vector3.zero;

        for( int i = 1; i < tunnelLength; i++ ) {

            Vector3 newPosition = new Vector3(

                Random.Range(lastPosition.x - xyOffset, lastPosition.x + xyOffset),
                Random.Range(lastPosition.y - xyOffset, lastPosition.y + xyOffset),
                i * zOffset
            );


            tunnelSegments[ i ] = newPosition;

            lastPosition = newPosition;
        }
    }
}
