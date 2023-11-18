using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class PointsElementUI : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private TextMeshProUGUI _clicksText;
	[SerializeField] private float _duration = 3;
	private GameController _game;
	
	#endregion

	#region Unity Callbacks
	void Awake()
	{
		_game = FindObjectOfType<GameController>();
	}
	// Start is called before the first frame update
	void Start()
    {
		_clicksText.transform.Translate(Vector3.back);
		//Set Clicks text
		_clicksText.text = "+" + _game.ClickRatio.ToString();
		
		//Movement
		transform.DOMoveY(transform.position.y + UnityEngine.Random.Range(100, 500), _duration);

		//Color Fade Out
		_clicksText.DOColor(new Color(0, 0, 0, 0), _duration);

		// Par�metros del movimiento sinusoidal
		Destroy(gameObject, _duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}