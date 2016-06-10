using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class testscript : MonoBehaviour {
    private enum weapons
    {
        none,
        katanaEquiped,
        macheteEquiped,
        longSwordEquiped,
        crossBowEquiped,
    }
    public static bool[] isSkinOwned1 = new bool[7];
    // Use this for initialization
    void Start () {
        
     Debug.Log(isSkinOwned1);
    // transform.localScale = Vector3.one * scaleSize;
    // Text test1 =  gameObject.GetComponent<Text>;
}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(Time.deltaTime);
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("1");

    }

 


    weapons myWeapon = weapons.none;


    void SetWeaponkatana( )
    {
        myWeapon = weapons.katanaEquiped;
    }



   

}
