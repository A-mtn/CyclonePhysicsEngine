using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	public Transform m_startPoint;
	public Transform m_endPoint;
	LineRenderer m_laserLine;
	// Use this for initialization
	void Start () {
		m_laserLine = GetComponentInChildren<LineRenderer> ();
		m_laserLine.SetWidth (.2f, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		m_laserLine.SetPosition (0, m_startPoint.position);
		m_laserLine.SetPosition (1, m_endPoint.position);

	}
}
