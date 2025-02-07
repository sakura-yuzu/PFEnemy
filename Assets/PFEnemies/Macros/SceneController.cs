using UnityEngine;
using UnityEngine.UI;

class SceneController : MonoBehaviour
{
	public Button AttackButton;
	public Button SkillButton;
	public Button DamagedButton;
	public Button DeadButton;
	public Button HealButton;
	public Button BuffButton;
	public Button DebuffButton;
	public Button SpecialAttackButton;
	public Button AbnormalStateButton;
	public Button SummonButton;
	public Button CriticalAttackButton;
	public Button DodgeButton;
	void Awake()
	{
		// Set the target frame rate to 60fps
		Application.targetFrameRate = 60;
	}

	void Start()
	{
	}
}