using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
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
	public CreatureList CreatureList;
	public GameObject CharacterList;
	public Transform MainCube;
	public CameraController MainCamera;
	public Transform PreviewCube;
	public Button SelectButton;
	public Canvas CharacterSelectCanvas;
	public Canvas ActionButtonCanvas;
	public Button OpenCharacterSelectButton;
	private Dictionary<string, GameObject> creatureInstances = new Dictionary<string, GameObject>();
	private int UIViewLayer;
	private string SelectedCreature;
	private GameObject CurrentModel;
	void Awake()
	{
		// Set the target frame rate to 60fps
		Application.targetFrameRate = 60;
		Prepare();
	}

	void Start()
	{
	}

	private async void Prepare()
	{
		UIViewLayer = LayerMask.NameToLayer("UIView");
		var ToggleGroup = CharacterList.GetComponent<ToggleGroup>();
		var buttonPrefab = await Addressables.LoadAssetAsync<GameObject>("Assets/PFEnemies/Prefabs/Toggle.prefab").Task;
		foreach (CreatureSetting creature in CreatureList.creatures)
		{
			GameObject toggle = Instantiate(buttonPrefab, CharacterList.transform, false);
			toggle.transform.Find("Label").GetComponent<Text>().text = creature.displayName;
			toggle.GetComponent<Toggle>().group = ToggleGroup;
			toggle.GetComponent<Toggle>().onValueChanged.AddListener((isOn) => OnToggleValueChanged(creature.displayName, isOn));

			var enemyPrefab = await Addressables.LoadAssetAsync<GameObject>(creature.prefabAddress).Task;
			var instance = Instantiate(enemyPrefab, PreviewCube, false);
			instance.SetActive(false);
			SetLayerRecursively(instance, UIViewLayer);
			creatureInstances[creature.displayName] = instance;
		}
		SelectButton.onClick.AddListener(OnSelectButtonClicked);
		// アニメーション用ボタンたち
		AttackButton.onClick.AddListener(OnAttackButtonClicked);
		SkillButton.onClick.AddListener(OnSkillButtonClicked);
		DamagedButton.onClick.AddListener(OnDamagedButtonClicked);
		DeadButton.onClick.AddListener(OnDeadButtonClicked);
		HealButton.onClick.AddListener(OnHealButtonClicked);
		BuffButton.onClick.AddListener(OnBuffButtonClicked);
		DebuffButton.onClick.AddListener(OnDebuffButtonClicked);
		SpecialAttackButton.onClick.AddListener(OnSpecialAttackButtonClicked);
		AbnormalStateButton.onClick.AddListener(OnAbnormalStateButtonClicked);
		SummonButton.onClick.AddListener(OnSummonButtonClicked);
		CriticalAttackButton.onClick.AddListener(OnCriticalAttackButtonClicked);
		DodgeButton.onClick.AddListener(OnDodgeButtonClicked);
	}
	private async void OnSelectButtonClicked()
	{
		await InstantiatePrefab();
		MainCamera.ResetCamera();
		CharacterSelectCanvas.gameObject.SetActive(false);
		ActionButtonCanvas.gameObject.SetActive(true);
	}

	private async Task InstantiatePrefab()
	{
		if (CurrentModel != null)
		{
			Destroy(CurrentModel);
		}
		CreatureSetting creature = CreatureList.creatures.Find(creature => creature.displayName == SelectedCreature);
		var enemyPrefab = await Addressables.LoadAssetAsync<GameObject>(creature.prefabAddress).Task;
		CurrentModel = Instantiate(enemyPrefab, MainCube, false);
	}
	private void OnToggleValueChanged(string creatureName, bool isOn)
	{
		Debug.Log($"Toggle for {creatureName} is now {(isOn ? "On" : "Off")}");
		SelectedCreature = creatureName;
		if (creatureInstances.TryGetValue(creatureName, out GameObject creatureInstance))
		{
			creatureInstance.SetActive(isOn);
		}
	}

	private void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (obj == null)
		{
			return;
		}

		obj.layer = newLayer;

		foreach (Transform child in obj.transform)
		{
			if (child == null)
			{
				continue;
			}
			SetLayerRecursively(child.gameObject, newLayer);
		}
	}

	private void OnAttackButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Attack");
	}

	private void OnSkillButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Skill");
	}

	private void OnDamagedButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Damaged");
	}

	private void OnDeadButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Dead");
	}

	private void OnHealButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Heal");
	}

	private void OnBuffButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Buff");
	}

	private void OnDebuffButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Debuff");
	}

	private void OnSpecialAttackButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("SpecialAttack");
	}

	private void OnAbnormalStateButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("AbnormalState");
	}

	private void OnSummonButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Summon");
	}

	private void OnCriticalAttackButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("CriticalAttack");
	}

	private void OnDodgeButtonClicked()
	{
		CurrentModel.GetComponent<Animator>().SetTrigger("Dodge");
	}
}