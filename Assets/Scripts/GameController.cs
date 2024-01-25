using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    private int count;
    private int numPickups = 4;
    private float[] closestPickUpDistance;
    private LineRenderer lineRenderer;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI playerPositionText;

    public GameObject[] pickUpsArray;

    enum ExecutionModes {
        Normal,
        Distance,
        Vision
    };

    // Start is called before the first frame update
    void Start() {
        count = 0;
        winText.text = "";
        scoreText.text = "Score: 0";
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    public void SetCountText() {
        count++;
        scoreText.text = "Score: " + count.ToString();
        if(count >= numPickups) {
            winText.text = "You've won!";
            scoreText.text = "";
        }
    }

    public void getPlayerData(Vector3 playerPos, float playerVel) {

        float[][] distancesArray = new float[numPickups][];

        for (int i = 0; i < numPickups; i++) {
            distancesArray[i] = new float[2];
        }

        for (int i = 0; i < numPickups; i++) {
            
            distancesArray[i][0] = i;

            if (pickUpsArray[i].activeInHierarchy) {
                distancesArray[i][1] = Vector3.Distance(playerPos, pickUpsArray[i].transform.position);
            } else {
                distancesArray[i][1] = -1;
            }
        }

        closestPickUpDistance = distancesArray[0];

        for (int i = 0; i < numPickups; i++) {

            if (distancesArray[i][1] < closestPickUpDistance[1] && distancesArray[i][1] > 0 || closestPickUpDistance[1] == -1) {
                closestPickUpDistance = distancesArray[i];
            }

        }

        for (int i = 0; i < numPickups; i++) {
            if (i == closestPickUpDistance[0]) {
                pickUpsArray[i].GetComponent<Renderer>().material.color = Color.blue;
            } else {
                pickUpsArray[i].GetComponent<Renderer>().material.color = Color.white;
            }
        }

        playerPositionText.text = "Player's global position: \n" + 
                                    playerPos.ToString() + "\n" + 
                                    "Player's velocity: \n" + playerVel +
                                    "\n Distance to closest Pick Up: \n" +
                                    closestPickUpDistance[1];

        int closestIndex = System.Convert.ToInt32(closestPickUpDistance[0]);

        lineRenderer.SetPosition (0, playerPos); // 0 for the start point, position vector 'startPosition'
        lineRenderer.SetPosition (1, pickUpsArray[closestIndex].GetComponent<Transform>().position);  // 1 for the end point, position vector 'endPosition'
        lineRenderer.SetWidth (0.1f, 0.1f); // Width of 0.1f both at origin and end of the line
    }

    void Update() {

    }
}
