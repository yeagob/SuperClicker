using System;
using UnityEngine;

public class HandVRController : MonoBehaviour
{
	#region Fields
	private LineRenderer _line;
	//Personaje a teletransportar
	[SerializeField] private Transform _player;
	[SerializeField] private GameObject _teleportMarc;
	//Boton necesario para el teleport. 
	[SerializeField] private OVRInput.Button _buttonPressed;

	#endregion

	#region Properties
	#endregion

	#region Unity Callbacks	
	private void Awake()
	{
		_line = GetComponent<LineRenderer>();

	}
	private void Update()
	{

		TeleportRay();
		
	}

	private void TeleportRay()
	{
		_line.SetPosition(0, transform.position);
		RaycastHit hit;
		//Debug.DrawRay(transform.position + transform.right * 0.2f, transform.right);
		if (Physics.Raycast(transform.position + transform.right * 0.2f, transform.right, out hit))
		{
			if (hit.collider.tag == "Floor")
			{
				_line.SetPosition(1, hit.point);
				_teleportMarc.SetActive(true);
				_teleportMarc.transform.position = hit.point + Vector3.up*0.01f;

				if (OVRInput.GetUp(_buttonPressed) == true)
					_player.position = hit.point + Vector3.up;
			}
			else
			{
				_teleportMarc.SetActive(false);

				_line.SetPosition(1, transform.position);
			}

		}
	}
	#endregion

	#region Private Methods
	#endregion

	#region Public Methods
	#endregion

}
