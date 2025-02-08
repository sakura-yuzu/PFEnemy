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
	public CreatureList CreatureList;
	public GameObject CharacterList;
	public GameObject cube;
	public Transform PreviewCube;
	public Button SelectButton;
	private Dictionary<string, GameObject> creatureInstances = new Dictionary<string, GameObject>();
	private int UIViewLayer;
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
	}
}