using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> chunksPipe;
    public GameObject chunkPrefab;
    public int maxNumberChunk = 3;

    [SerializeField]
    private float distanceParcouru = 0f;
    private float pipeLength = 32f;
    private Vector3 lastPositionPlayer;

    private void Awake()
    {
        lastPositionPlayer = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceParcouru += Mathf.Abs(lastPositionPlayer.z - player.transform.position.z);
        lastPositionPlayer = player.transform.position;

        if(distanceParcouru >= pipeLength)
        {
            if(chunksPipe.Count >= maxNumberChunk)
            {
                GameObject chunk = chunksPipe[0];
                chunksPipe.RemoveAt(0);
                Destroy(chunk);
            }



            GameObject newChunk = Instantiate(chunkPrefab, player.transform.position, Quaternion.identity);

            if(chunksPipe.Count > 0)
            {
                AttachPipe(chunksPipe[chunksPipe.Count - 1].GetComponent<Pipe>(), newChunk.GetComponent<Pipe>());
            }
            chunksPipe.Add(newChunk);
            distanceParcouru = 0;
        }
    }

    //Attache le pipe2 au pipe1
    void AttachPipe(Pipe pipe1, Pipe pipe2)
    {
        pipe2.transform.position = pipe1.endPoint.position;
    }
}
