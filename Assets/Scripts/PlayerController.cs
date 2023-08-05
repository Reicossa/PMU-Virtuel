using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;

    private int changeLaneDist = 5;
    private int lane = 1;
    private int zRange = 22;
    private float speed = 15.0f;
    public float despawnDelay = 5f;

    public GameObject Bomb;
    public GameObject Barriere;
    public Transform bombSpawnPoint;
    public ParticleSystem exposionParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check si le player n'est pas sur la ligne la plus en haut et le déplace sur la ligne supérieure
        if (Input.GetKeyDown(KeyCode.S) && lane != 4)
        {
            transform.position = new Vector3((transform.position.x - changeLaneDist), transform.position.y, transform.position.z);
            lane += 1;
        }

        //Check si le player n'est pas sur la ligne la plus en bas et le déplace sur la ligne supérieure
        if (Input.GetKeyDown(KeyCode.W) && lane!=1)
        {
            transform.position = new Vector3((transform.position.x + changeLaneDist), transform.position.y, transform.position.z);
            lane -= 1;
        }

        // Mouvement of the player along lanes
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput);

        // Block the player if he goes to much to the right
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }

        // Block the player if he goes to much to the left
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }

        // Drop bomb when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBombAtPlayerPosition();
        }

        // Drop barrier when space is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SpawnBarrierAtPlayerPosition();
        }
    }
    private void SpawnBombAtPlayerPosition()
    {
        Vector3 playerPosition = transform.position;
        GameObject spawnedPrefab = Instantiate(Bomb, playerPosition, Quaternion.identity);
        Destroy(spawnedPrefab, despawnDelay);
        exposionParticle.Play();
    }

    private void SpawnBarrierAtPlayerPosition()
    {
        Vector3 playerPosition = transform.position;
        GameObject spawnedPrefab = Instantiate(Barriere, playerPosition, Quaternion.identity);
        Destroy(spawnedPrefab, despawnDelay);
        exposionParticle.Play();
    }
}