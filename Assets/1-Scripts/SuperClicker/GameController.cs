using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class GameController : MonoBehaviour
{
	#region Properties
	[field:SerializeField] public float ClickRatio { get; set; }
	#endregion

	#region Fields
	[SerializeField] private TextMeshProUGUI _rewardText;
	[SerializeField] private TextMeshProUGUI _clicksText;

	[SerializeField] private ParticleSystem _particlesRain;
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
		SlotButtonUI.OnSlotReward += GetReward;
    }
	private void OnDestroy()
	{
		SlotButtonUI.OnSlotReward -= GetReward;
	}

	#endregion

	#region Public Methods
	public void RainParticles()
	{
		_particlesRain.Emit(Mathf.Clamp((int)ClickRatio, 0, 13));
	}
	#endregion

	#region Private Methods
	private void GetReward(Reward reward)
	{
		ShowReward(reward);

		//Apply rewards
		if (reward.RewardType == RewardType.Plus)
		{
			ClickRatio += reward.Value;
			_clicksText.text = "x" + ClickRatio;
			return;
		}
		
		if (reward.RewardType == RewardType.Multi)
		{
			ClickRatio *= reward.Value;
			_clicksText.text = "x" + ClickRatio;
			return;
		}

		if (reward.RewardType == RewardType.Agent)
		{
			//...
			return;
		}
	}

	private void ShowReward(Reward reward)
	{
		//Initialziation
		if (!_rewardText.gameObject.activeSelf)
		{
			_rewardText.gameObject.SetActive(true);
			_rewardText.transform.localScale = Vector3.zero;
		}

		//Update text
		_rewardText.text = "REWARD\n " + reward.RewardType + reward.Value + " Clicks";

		// Crear una secuencia
		Sequence mySequence = DOTween.Sequence();

		// Añadir el primer efecto de escala
		mySequence.Append(_rewardText.transform.DOScale(1, 1));

		// Añadir el efecto de sacudida en la rotación
		mySequence.Append(_rewardText.transform.DOShakeRotation(1, new Vector3(0, 0, 30)));

		// Añadir el segundo efecto de escala
		mySequence.Append(_rewardText.transform.DOScale(0, 1));

		// Iniciar la secuencia
		mySequence.Play();

	}
	#endregion
}
