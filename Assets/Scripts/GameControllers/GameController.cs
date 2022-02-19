using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<Area> m_EnemySpawns;
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

    [SerializeField]
    WaveData m_WaveData;

    public PlayerData PlayerData => m_PlayerData;

    float m_Timer;

    private void Awake()
    {
        m_Character.SetMoveSpeed(m_MoveSpeed);
        GetItem(m_Item);

        var tc = Services.Find<TimeController>();
        var wc = Services.Find<WaveController>();
        foreach(var w in m_WaveData.Waves)
        {
            switch (w.WaveType)
            {
                case WaveData.EWaveType.Instant:
                    tc.ScheduleEvent(w.TimeInSeconds, _ => wc.SpawnEnemyGroup(w));
                    break;
                case WaveData.EWaveType.SpawnPerMinute:
                    tc.ScheduleEvent(w.TimeInSeconds, _ => wc.SetSpawnWaveOverTime(w));
                    break;
                default:
                    break;
            }
        }
    }

    void Start()
    {
        var ui = Services.Find<UI>();

        ui.SetPickLevelupCallback(OnLevelUp);

        ui.SetCurrentHealth(m_MaxHealth);
        ui.SetMaxHealth(m_MaxHealth);

        ui.SetCurrentExperience(0);
        ui.SetLevelData(Services.Find<PlayerController>().LevelData);

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

        Services.Find<WaveController>().UpdateTime(dt);

        m_Timer += dt;
        if (m_Timer > m_HurtTime)
        {
            m_Timer -= m_HurtTime;

            foreach (var e in m_Character.TouchingEnemies)
            {
                player.DoDamage(e.Damage);
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

    public void SpawnEnemy(Enemy enemy)
    {
        var i = Random.Range(0, m_EnemySpawns.Count);
        var s = m_EnemySpawns[i].GetRandomPosition();
        var e = Instantiate(enemy);
        e.transform.position = s;
        e.gameObject.SetActive(true);
    }

    void OnLevelUp(Item item)
    {
        GetItem(item);
        Time.timeScale = 1;
    }
}
