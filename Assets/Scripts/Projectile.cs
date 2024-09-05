using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface Projectile
{
    public void GetFired();
    public void HitObject(GameObject collidedObj);
}