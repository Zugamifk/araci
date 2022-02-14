using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<Area> m_EnemySpawns;
    [SerializeField]
    Enemy m_Enemy;
    [SerializeField]
    Pickup[] m_ExperienceGems;

    [SerializeField]
    Character m_Character;
    [SerializeField]
    float m_MoveSpeed;
    [SerializeField]
    float m_HurtTime;
    [SerializeField]
    int m_MaxHealth;
    [SerializeField]
    Item m_Item;
    [SerializeField]
    ProgressionData m_ProgressionData;

    [SerializeField]
    DropTable m_DropTable;

    float m_Timer;
    PlayerController m_Player;

    private void Awake()
    {
        m_Player = new PlayerController(m_ProgressionData, m_MaxHealth);
        m_Character.SetMoveSpeed(m_MoveSpeed);
        GetItem(m_Item);
    }

    void Start()
    {
        var ui = Services.Find<UI>();

        ui.SetPickLevelupCallback(OnLevelUp);

        ui.SetCurrentHealth(m_MaxHealth);
        ui.SetMaxHealth(m_MaxHealth);

        ui.SetCurrentExperience(0);
        ui.SetLevelData(m_Player.LevelData);

        StartCoroutine(SpawnEnemies());

        GainExperience(10);
    }

    private void Update()
    {
        m_Player.Update(Time.deltaTime);
        m_Timer += Time.deltaTime;
        if (m_Timer > m_HurtTime)
        {
            m_Timer -= m_HurtTime;

            foreach (var e in m_Character.TouchingEnemies)
            {
                m_Player.Damage(e.Damage);
            }
        }
    }

    public void SpawnExperienceGem(Vector3 position)
    {
        var i = Random.Range(0, m_ExperienceGems.Length);
        var gem = Instantiate(m_ExperienceGems[i]);
        gem.transform.position = position;
        gem.gameObject.SetActive(true);
    }

    void GetItem(Item item)
    {
        m_Player.PickUp(item);
        Services.Find<UI>().AddItem(item, m_Player.GetItemLevel(item));
    }

    public void GainExperience(int amount)
    {
        if(m_Player.GainExperience(amount))
        {
            Services.Find<UI>().ShowLevelupPanel(m_Player, m_DropTable.GetLevelupOptions(3, m_Player));
            Services.Find<UI>().SetLevelData(m_Player.LevelData);
            Time.timeScale = 0;
        }
    }

    void OnLevelUp(Item item)
    {
        GetItem(item);
        Time.timeScale = 1;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            var i = Random.Range(0, m_EnemySpawns.Count);
            var s = m_EnemySpawns[i].GetRandomPosition();
            var e = Instantiate(m_Enemy);
            e.transform.position = s;
            e.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }
}
