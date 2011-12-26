using UnityEngine;
using System.Collections;


/*
 *		Author: 	Craig Lomax
 *		Date: 		06.09.2011
 *		URL:		clomax.me.uk
 *		email:		crl9@aber.ac.uk
 *
 */

public class Creature : MonoBehaviour {

#pragma warning disable 0414
	private GameObject mth;
	private GameObject gtl;
	private int id;
	private float sensitivityFwd = 1.0F;
	private float sensitivityHdg = 2.5F;
	private double energy;
	private float hdg = 0F;
	private Transform _t;
	private Logger lg;
	private int line_of_sight;
#pragma warning restore 0414
	
	void Start () {
		this._t = transform;
		this.name = "Creature";
		this.hdg = transform.localEulerAngles.y;
		this.lg = Logger.getInstance();
		this.line_of_sight = 2000;
		
		this.mth = (GameObject)Resources.Load("Prefabs/Creature/Mouth");
		GameObject mouth = (GameObject)Instantiate(mth);
		mouth.transform.parent = transform;
		mouth.transform.localPosition = new Vector3(0,0,0.5F);
		mouth.transform.localEulerAngles = new Vector3(0,0,0);
		mouth.AddComponent("Mouth");
		
		this.gtl = (GameObject)Resources.Load("Prefabs/Creature/Genital");
		GameObject genital = (GameObject)Instantiate(gtl);
		genital.transform.parent = transform;
		genital.transform.localPosition = new Vector3(0,0,-0.5F);
		genital.transform.localEulerAngles = new Vector3(0,180,0);
		genital.AddComponent("Genitalia");
	}
	
	public Creature () {
		this.id = GetInstanceID();
	}
	
	void Update () {
		this.changeHeading(Input.GetAxis("Horizontal") * this.sensitivityHdg);
		this.moveForward(Input.GetAxis("Vertical") * this.sensitivityFwd);
	}
	
	

	
	/*
	 * Return the current energy value for the creature
	 */
	public double getEnergy () {
		return this.energy;
	}
	
	/*
	 * Add to the creature the energy of what it ate
	 */
	public void eat (double n) {
		this.energy += n;
	}
	
	public void subtractEnergy (double n) {
		this.energy -= n;	
	}
	
	/*
	 * Remove the creature from existence and return
	 * the creature's energy.
	 */
	public double kill () {
		Destroy(gameObject);
		return (this.getEnergy());
	}
	
	public int getID () {
		return this.id;
	}
	
	public int getLOS() {
		return this.line_of_sight;
	}
	
	
	
	
	void moveForward (float n) {
		Vector3 fwd = _t.forward;
		fwd.y = 0;
		fwd.Normalize();
		this._t.position += n * fwd;
	}
	
	void changeHeading (float n) {
		this.hdg += n;
		this.wrapAngle(hdg);
		this._t.localEulerAngles = new Vector3(0,hdg,0);
	}
	
	void wrapAngle (float angle) {
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
	}

}
