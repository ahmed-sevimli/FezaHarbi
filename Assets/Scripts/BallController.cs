using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BallController : MonoBehaviour
{
    private Rigidbody rb; // Reference to player's Rigidbody.
    private int score;
    //[SerializeField]
    //private Material[] material_array;
    private List<Material> materials = new List<Material>();
    public Text txt;
    public Random rnd = new Random();
    void Awake()
    {   
        int i;
        for(i=0; i<5; i++){
            materials.Add(Resources.Load("ball_mat"+(i+1).ToString(), typeof(Material)) as Material);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(2000f, 0f, 0f), ForceMode.Force);
        score = 0;
        //Debug.Log("Current Score: " + transformers[0].GetComponent<Renderer>().material.color);
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        score++;
        Debug.Log("Current Score: " + score);
        Debug.Log("Material Name: " + score);
        txt.GetComponent<UnityEngine.UI.Text>().text = score.ToString();
        gameObject.GetComponent<Renderer>().material = materials[rnd.Next(materials.Count)];

        if (collision.gameObject.name == "RWall")
        {
            rb.AddForce(new Vector3(-4000f, 0f, 0f), ForceMode.Force);
        }
        else if(collision.gameObject.name == "LWall")
        {
            rb.AddForce(new Vector3(4000f, 0f, 0f), ForceMode.Force);
        }
    }
}
