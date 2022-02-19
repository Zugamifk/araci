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
    PlayerData m_PlayerData;

    [SerializeField]
    DropTable m_DropTable;

    public PlayerData PlayerData => m_PlayerData;

    float m_Timer;

    private void Awake()
    {
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
        ui.SetLevelData(Services.Find<PlayerController>().LevelData);

        StartCoroutine(SpawnEnemies());

        GainExperience(10);
    }

    private void Update()
    {
        var dt = Time.deltaTime;
        var player = Services.Find<PlayerController>();
        player.Update(dt);

        var tc = Services.Find<TimeController>();
        tc.Update(dt);
        Services.Find<UI>().SetTime(tc.Seconds);

        m_Timer += dt;
        if (m_Timer > m_HurtTime)
        {
            m_Timer -= m_HurtTime;

            foreach (var e in m_Character.TouchingEnemies)
            {
                player.Damage(e.Damage);
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
        var player = Services.Find<PlayerController>();
        player.PickUp(item);
        Services.Find<UI>().AddItem(item, player.GetItemLevel(item));
    }

    public void GainExperience(int amount)
    {
        var player = Services.Find<PlayerController>();
        if (player.GainExperience(amount))
        {
            Services.Find<UI>().ShowLevelupPanel(player, m_DropTable.GetLevelupOptions(3, player));
            Services.Find<UI>().SetLevelData(player.LevelData);
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
