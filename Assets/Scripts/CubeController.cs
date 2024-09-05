using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controls player movement and rotation.
public class CubeController : MonoBehaviour
{
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
	private Material cubeMaterial;
	private Color defaultColor;
    private Rigidbody rb; // Reference to player's Rigidbody.
	private Transform transformer; // Reference to player's Rigidbody.
	private Renderer myRenderer;
	[SerializeField]
	private Button nextButton;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
		myRenderer = GetComponent<Renderer>();
		transformer = GetComponent<Transform>();
		cubeMaterial = myRenderer.material;
		defaultColor = cubeMaterial.color;
		Debug.Log("Hello hello");
		nextButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){
			cubeMaterial.SetColor("_Color", Color.red);
		}
		
		if(Input.GetKeyUp(KeyCode.H)){
			cubeMaterial.SetColor("_Color", defaultColor);
		}
		/*
		if(Input.GetMouseButtonDown(0)){
			transformer.localScale += new Vector3(0.1f,0.1f,0.1f);
		}
		
		if(Input.GetMouseButtonDown(1)){
			transformer.localScale -= new Vector3(0.1f,0.1f,0.1f);
		}*/
		if(Input.GetMouseButton(0)){
			transformer.localScale += new Vector3(0.001f,0.001f,0.001f);
		}
		
		if(Input.GetMouseButton(1)){
			transformer.localScale -= new Vector3(0.001f,0.001f,0.001f);
		}
    }


    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
		
        // Rotate player based on vertical input.
        float turnV = Input.GetAxis("Vertical") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(turnV, 0f, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Rotate player based on horizontal input.
        float turnH = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        turnRotation = Quaternion.Euler(0f, turnH, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
		
    }

	void TaskOnClick(){
		Debug.Log("You have changed the scene!");
		SceneManager.LoadScene("Scene2");
	}
}

//Update, FizedUpdate ve LateUpdate farkları
//Awake, Unavailable, Start, OnDisable