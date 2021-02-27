using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObject;
    public float i;
    public float startTime;
    private string currentTime;
    private int Count;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        startTime = 0; // Replace by the value you want
    }

    // Update is called once per frame

    void Update()
    {
       
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent != null)
        {
            gameObject.active = true;

            if (Time.time > i)
            {
                i = Time.time + 1;

                GameObject coin = (GameObject)Instantiate(GameManager.Instance.CoinPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);
                Count++;



                if (Count % 15 == 0)
                {
                    GameObject Heart = (GameObject)Instantiate(GameManager.Instance.HeartPrefab, new Vector3(transform.position.x, transform.position.y + 2), Quaternion.identity);

                }
            }

        }
       
        else 
                gameObject.active = false;
    } 

}
