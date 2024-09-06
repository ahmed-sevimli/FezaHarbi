using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
    IEnumerator Example() {
        yield return new WaitForFixedUpdate();
    }
}
