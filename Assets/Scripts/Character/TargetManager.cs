using System.Collections.Generic;
using UnityEngine;

public static class TargetManager
{
    private static List<Enemy> _enemys = new List<Enemy>();
    
    public static void AddEnemyToTarget(Enemy enemy)
    {
        _enemys.Add(enemy);
    }

    public static Enemy GetEnemy(Transform characterPosition)
    {
        for (int i = _enemys.Count; i > 0; --i)
        {
            if (_enemys[i - 1] == null)
            {
                _enemys.RemoveAt(i - 1);
            }
        }
        foreach (var enemy in _enemys)
        {
            if (enemy.gameObject.transform.position.y >
                characterPosition.position.y)
            {
                return enemy;
            }
        }
        return null;
    }
}
