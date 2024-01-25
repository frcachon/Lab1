using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Vector3 lastPosition = Vector3.zero;
    private float instantVelocity = 0.0f;
    private GameController gc;

    public float speed;
    public GameObject gameController; // es la clase del otro script

    void Start() {
        gc = gameController.GetComponent<GameController>();
        // lo que va entre < > es el nombre del otro script
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            gc.SetCountText();
        }
    }

    void FixedUpdate() {
        float horAxis = Input.GetAxis ("Horizontal");
        float verAxis = Input.GetAxis ("Vertical");
        Vector3 movement = new Vector3(horAxis, 0.0f, verAxis);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
        
        instantVelocity = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
        gc.getPlayerData(lastPosition, instantVelocity);
    }

    void Update() {

    }
}
