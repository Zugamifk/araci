using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    List<Area> m_EnemySpawns;

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

        Services.Find<WaveController>().SetWaveData(m_WaveData);
        Services.Find<DropController>().SetDrops(m_DropTable);
    }

    void Start()
    {
        var ui = Services.Find<UI>();

        ui.SetPickLevelupCallback(OnLevelUp);

        ui.SetCurrentHealth(m_MaxHealth);
        ui.SetMaxHealth(m_MaxHealth);

        ui.SetCurrentExperience(0);
        ui.SetLevelData(Services.Find<PlayerController>().LevelData);
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

    void GetItem(Item item)
    {
        var player = Services.Find<PlayerController>();
        player.PickUp(item);
        Services.Find<UI>().AddItem(item, player.GetItemLevel(item));
    }

    public void LevelUp()
    {
        var player = Services.Find<PlayerController>();
        var options = Services.Find<DropController>().GetLevelupOptions(3, player);
        Services.Find<UI>().ShowLevelupPanel(player, options);
        Services.Find<UI>().SetLevelData(player.LevelData);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        Debug.LogError("GAME OVER!!");
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
