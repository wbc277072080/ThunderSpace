using UnityEngine;

public class Viewport : Singleton<Viewport>
{
    float minX;
    float maxX;
    float minY;
    float maxY;

    float middleX;

    void Start() {
        Camera mainCamera = Camera.main;

        //chanage view point position to world point position
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0f,0f));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1f,1f));
        middleX = mainCamera.ViewportToWorldPoint(new Vector3(0.5f,0f,0f)).x;

        minX = bottomLeft.x;
        minY = bottomLeft.y;

        
        maxX = topRight.x;
        maxY = topRight.y;

    }

    

    public Vector3 PlayerMoveablePosition(Vector3 playerPosition, float paddingX, float paddingY){
        Vector3 position = Vector3.zero;

        position.x = Mathf.Clamp(playerPosition.x,minX+paddingX,maxX-paddingX);
        position.y = Mathf.Clamp(playerPosition.y,minY+paddingY,maxY-paddingY);
        
        return position;
    }

    //enemy born point
    public Vector3 RandomEnemySpawnPosition(float paddingX, float paddingY){
        Vector3 position = Vector3.zero;
        position.x = maxX + paddingX;
        position.y = Random.Range(minY+paddingY,maxY-paddingY);

        return position;
    }

    //limit enemy position in right half of screen
    public Vector3 RandomRightHalfPosition(float paddingX, float paddingY){
        Vector3 position = Vector3.zero;
        //if enemy can move in whole screen
        //position.x = Random.Range(minX+paddingX, maxX - paddingX);
        position.x = Random.Range(middleX, maxX - paddingX);
        position.y = Random.Range(minY+paddingY,maxY-paddingY);
        return position;

    }

    
    
}
