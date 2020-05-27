using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour{
    [SerializeField]
    GameObject[] clouds;
    [SerializeField]
    GameObject[] initialClouds;
    [SerializeField]
    float spawnSpeed;
    [SerializeField]
    float minCloudSpeed;
    [SerializeField]
    float maxCloudSpeed;
    [SerializeField]
    float sizeVariation;
    [SerializeField]
    float PositionVariation;
    float nextCloudTime;
    
    // Start is called before the first frame update
    void Start(){
        nextCloudTime = Time.time + sizeVariation;
        foreach (GameObject cloud in initialClouds){
            Rigidbody2D rb2D = cloud.GetComponent<Rigidbody2D>();
            SetRandomVelocity(rb2D);
        }
    }

    // Update is called once per frame
    void Update(){
        if (Time.time > nextCloudTime){
            GameObject randomCloud = clouds[Random.Range(0, clouds.Length)];
            GameObject cloud = Instantiate(randomCloud, this.transform.position, Quaternion.identity);
            cloud.transform.SetParent(this.transform);
            //Velocity
            Rigidbody2D rb2D = cloud.GetComponent<Rigidbody2D>();
            Vector2 randomVelocity = new Vector2(Random.Range(minCloudSpeed, maxCloudSpeed), 0);
            rb2D.velocity = randomVelocity;
            //Size
            RectTransform rct = cloud.GetComponent<RectTransform>();
            rct.sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
            cloud.transform.localScale = Vector3.one;
            rct.sizeDelta = rct.sizeDelta - rct.sizeDelta*Random.Range(0.0f, sizeVariation);;
            //Position
            float randomYPos = rct.position.y + Random.Range(-PositionVariation, PositionVariation);
            rct.position = new Vector2(rct.position.x, randomYPos);

            nextCloudTime = Time.time + spawnSpeed;
        }
    }

    void SetRandomVelocity(Rigidbody2D rb2D){
        Vector2 randomVelocity = new Vector2(Random.Range(minCloudSpeed, maxCloudSpeed), 0);
        rb2D.velocity = randomVelocity;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
