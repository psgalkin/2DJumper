using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.UIElements;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelData _data;

    private ObjectsFactory _factory;

    private Camera _camera;
    private float _lvlWidth;
    private float _lvlHeight;
    private Vector3 _platformSize;

    private float _deltaPlatformEnd = 2.0f;

    private List<GameObject> _platforms = new List<GameObject>();
    private List<GameObject>_allObjects = new List<GameObject>();

    private void Start()
    {
        _factory = new ObjectsFactory();
        _platformSize = _factory.PlatformSize();

        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _lvlWidth = _camera.orthographicSize * 2.0f * _camera.aspect;
        _lvlHeight = _camera.orthographicSize * 2.0f;

        _camera.transform.position = new Vector3(_lvlWidth / 2, _lvlHeight / 2, _camera.transform.position.z);
        //Debug.Log($"LWLWIDTH: {_lvlWidth}, ORT SIZE: {camera.orthographicSize * 2.0f}, ASPECT: {camera.aspect}");

        Generate();
    }

    private void Generate()
    {
        GenerateCharacter();
        GeneratePlatforms();
        GenerateBoosts();
        GenerateEnemys();
        GenerateCoins();
        GenerateBorders();
    }

    private void GeneratePlatforms()
    {
        GameObject firstPlatform = _factory.GetPlatform(PlatformType.Simple);
        Vector3 firstPosition = new Vector3(_lvlWidth / 2, _platformSize.y);
        firstPlatform.transform.position = firstPosition;
        _platforms.Add(firstPlatform);

        float startPos = _platforms[0].transform.position.y + _platformSize.y * 1.5f;
        foreach (PlatformInterval platformInterval in _data.PlatformIntervals)
        {
            Dictionary<PlatformType, float> ratios = ComputeRatios(platformInterval.PlatformsRatio);

            for (float currentHeight = platformInterval.IntervalStart + startPos; 
                currentHeight < platformInterval.IntervalEnd; 
                currentHeight += (Random.Range(platformInterval.MinDist, platformInterval.MaxDist) + _platformSize.y))
            {
                PlatformType type = GetPlatformType(ratios);
                GameObject platform = _factory.GetPlatform(type);

                if (type == PlatformType.Moving) {
                    platform.GetComponent<MovingPlatform>().SetSpeed(platformInterval.MovingPlatformSpeed);
                }


                Vector3 position = new Vector3(Random.Range(_platformSize.x / 2, _lvlWidth - _platformSize.x / 2), currentHeight);
                platform.transform.position = position;
                _platforms.Add(platform);
                _allObjects.Add(platform);
            }                 
        }
    }

    private Dictionary<PlatformType, float> ComputeRatios(PlatformData[] input)
    {
        int sum = 0;
        foreach (var element in input)
        {
            sum += element.Ratio;
        }

        float coef = 100f / sum;
        float computed = 0;

        Dictionary<PlatformType, float> output = new Dictionary<PlatformType, float>();
        foreach (var element in input)
        {
            output[element.Type] = computed + element.Ratio * coef;
            computed += (element.Ratio * coef);
        }

        return output;
    }


    private PlatformType GetPlatformType(Dictionary<PlatformType, float> ratios)
    {
        float probability = Random.Range(0f, 100f);

        foreach (var element in ratios)
        {
            if (probability < element.Value) { return element.Key; }    
        }

        return PlatformType.Simple;
    }

    private void GenerateBoosts()
    {
        if (_data.BoostIntervals.Length == 0) { return; }
        float boostFixedSize = 0.5f;
        float deltaBoostPlatform = 0.02f;
        foreach (BoostInterval boostInterval in _data.BoostIntervals)
        {
            List<Vector3> positionsInInterval = new List<Vector3>();
            for (int i = 0; i < _platforms.Count; ++i)
            {
                if (_platforms[i].transform.position.y > boostInterval.IntervalStart &&
                    _platforms[i].transform.position.y < boostInterval.IntervalEnd &&
                    (_platforms[i + 1].transform.position.y - _platforms[i].transform.position.y) > (boostFixedSize + _platformSize.y))
                {
                    positionsInInterval.Add(_platforms[i].transform.position);
                }
            }

            foreach (BoostIntervalData data in boostInterval.TypesNumbersProbabilitys)
            {
                for (int i = 0; i < data.Number; ++i)
                {
                    if (Random.Range(0, 100) < data.Probability)
                    {
                        int platformNum = Random.Range(0, positionsInInterval.Count);

                        Transform  transform = _platforms[0].transform;
                        foreach (GameObject platform in _platforms)
                        {
                            if (platform.transform.position.y == positionsInInterval[platformNum].y)
                            {
                                transform = platform.transform;
                                break;
                            }
                        }

                        GameObject boost = _factory.GetBoost(data.BoostType, transform);
                        boost.transform.localScale = new Vector3(boost.transform.localScale.x / 0.3f, boost.transform.localScale.y / 0.3f);
                        
                        // TODO: bostFisedSize
                        Vector2 boostSize = boost.GetComponent<BoxCollider2D>().size * boost.GetComponent<BoxCollider2D>().transform.localScale;

                        boost.transform.position = new Vector3(
                            positionsInInterval[platformNum].x,
                            positionsInInterval[platformNum].y + _platformSize.y / 2 + deltaBoostPlatform + boostSize.y / 2 * 0.3f);

                        _allObjects.Add(boost);
                        positionsInInterval.RemoveAt(platformNum);
                    }
                }
            }
        }
    }

    private void GenerateEnemys()
    {
        if (_data.EnemyIntervals.Length == 0) { return; }

        foreach (EnemyInterval enemyInterval in _data.EnemyIntervals)
        {
            // Ceate list of all intervals by height in interval from data-enemy
            List<float[]> rawHeightIntervals = new List<float[]>();
            for (int i = 0; i < _platforms.Count - 1; ++i)
            {
                if (_platforms[i].transform.position.y > enemyInterval.IntervalStart &&
                    _platforms[i].transform.position.y < enemyInterval.IntervalEnd)
                {
                    rawHeightIntervals.Add(new float[] { _platforms[i].transform.position.y + _platformSize.y / 2,
                        _platforms[i + 1].transform.position.y - _platformSize.y / 2 });
                }
            }

            foreach (EnemyIntervalData data in enemyInterval.TypesNumbersProbabilitys)
            {
                for (int i = 0; i < data.Number; ++i)
                {
                    if (Random.Range(0, 100) < data.Probability)
                    {
                        GameObject enemy = _factory.GetEnemy(data.EnemyType);
                        Vector2 enemySize = enemy.GetComponent<BoxCollider2D>().size * enemy.GetComponent<BoxCollider2D>().transform.localScale;

                        List<float[]> heightIntervals = new List<float[]>();
                        foreach (float[] interval in rawHeightIntervals){
                            if (interval[1] - interval[0] > enemySize.y) {
                                heightIntervals.Add(new float[] { interval[0], interval[1] });
                            }
                        }

                        if (heightIntervals.Count == 0) { continue; }
                         int platformNum = Random.Range(0, heightIntervals.Count);
                        
                        enemy.transform.position = new Vector3(
                            Random.Range(enemySize.x / 2, _lvlWidth - enemySize.x / 2),
                            Random.Range((heightIntervals[platformNum][0] + enemySize.y / 2),
                                (heightIntervals[platformNum][1] - enemySize.y / 2)));
                        
                        _allObjects.Add(enemy);
                        
                        for (int j = 0; j < rawHeightIntervals.Count; ++j)
                        {
                            if (rawHeightIntervals[j][0] == heightIntervals[platformNum][0] &&
                                rawHeightIntervals[j][1] == heightIntervals[platformNum][1])
                            {
                                rawHeightIntervals.RemoveAt(j);
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }

    private static int CompareByTransformHeight(GameObject a, GameObject b)
    {
        if (a.transform.position.y > b.transform.position.y) { return 1; }
        return -1;
    }

    private void GenerateCoins()
    {
        float coinRadius = _factory.CoinRadius();
        int firstNum = 0;
        int lastNum = 0;
        float deltaPlatformCoin = 0.2f;

        _allObjects.Sort(CompareByTransformHeight);

        foreach (CoinInterval coinInterval in _data.CoinIntervals)
        {
            // Find free space intervals between 
            List<float[]> rawIntervals = new List<float[]>();
            firstNum = lastNum;
            for (int i = firstNum; (_allObjects[i].transform.position.y < coinInterval.IntervalEnd) && (i < _allObjects.Count - 1); i ++)
            {
                if (_allObjects[i + 1].transform.position.y - _allObjects[i].transform.position.y - _platformSize.y - 2 * deltaPlatformCoin > coinRadius * 2)
                {
                    rawIntervals.Add(new float[] { _allObjects[i].transform.position.y + _platformSize.y / 2 + deltaPlatformCoin, 
                        _allObjects[i + 1].transform.position.y - _platformSize.y / 2 - deltaPlatformCoin});
                }

                lastNum++;
            }

            // Find list of approximately equal space intervals between platforms
            List<float[]> equalIntervals = new List<float[]>();
            foreach (float[] interval in rawIntervals)
            {
                double n = Math.Round((interval[1] - interval[0]) / coinRadius / 2);
                float delta = (interval[1] - interval[0]) / (float)n;
                float start = interval[0], end;
                for (int i = 0; i < n; ++i)
                {
                    end = start + delta;
                    equalIntervals.Add(new float[] { start, end });
                    start = end;
                }
            }

            // Generate coin in random equal space interval
            if (coinInterval.Number > equalIntervals.Count)
            {
                Debug.Log($"Wrong number of coins");
            }
            
            int currentMaxNum = equalIntervals.Count;

            
            for (int i = 0; i < coinInterval.Number; ++i)
            {
                int num = Random.Range(0, currentMaxNum);

                GameObject coin = _factory.GetCoin();
                Vector3 position = new Vector3(
                    Random.Range(coinRadius, _lvlWidth - coinRadius),
                    Random.Range(equalIntervals[num][0] + coinRadius, equalIntervals[num][1] - coinRadius));
                coin.transform.position = position;

                currentMaxNum--;
                equalIntervals.RemoveAt(num);
            }
        }
    }

    private void GenerateCharacter()
    {
        GameObject character = _factory.GetCharacter();
        character.transform.position = new Vector3(_lvlWidth / 2, 1.5f);
    }

    private void GenerateBorders()
    {
        GameObject bottomBorder = _factory.GetBorder(BorderType.Bottom);
        bottomBorder.transform.position = _camera.transform.position;
        Vector2[] bottomPoints = new Vector2[] { new Vector2(-_lvlWidth / 2.0f, -_lvlHeight / 2.0f), new Vector2(_lvlWidth / 2.0f, -_lvlHeight / 2.0f) };
        bottomBorder.GetComponent<EdgeCollider2D>().points = bottomPoints;

        GameObject upperBorder = _factory.GetBorder(BorderType.Upper);
        upperBorder.transform.position = _camera.transform.position;
        Vector2[] upperPoints = new Vector2[] { new Vector2(-_lvlWidth / 2.0f, _lvlHeight / 2.0f), new Vector2(_lvlWidth / 2.0f, _lvlHeight / 2.0f) };
        upperBorder.GetComponent<EdgeCollider2D>().points = upperPoints;

        GameObject rightSideBorder = _factory.GetBorder(BorderType.Side); ;
        rightSideBorder.transform.position = _camera.transform.position;
        Vector2[] rightPoints = new Vector2[] { new Vector2(-_lvlWidth / 2.0f, -_lvlHeight / 2.0f), new Vector2(-_lvlWidth / 2.0f, _lvlHeight / 2.0f) };
        rightSideBorder.GetComponent<EdgeCollider2D>().points = rightPoints;

        GameObject leftSideBorder = _factory.GetBorder(BorderType.Side); ;
        leftSideBorder.transform.position = _camera.transform.position;
        Vector2[] leftPoints = new Vector2[] { new Vector2(_lvlWidth / 2.0f, -_lvlHeight / 2.0f), new Vector2(_lvlWidth / 2.0f, _lvlHeight / 2.0f) };
        leftSideBorder.GetComponent<EdgeCollider2D>().points = leftPoints;

        GameObject endLevelBorder = _factory.GetBorder(BorderType.EndLevel);
        endLevelBorder.transform.position = new Vector3(_camera.transform.position.x, _platforms[_platforms.Count - 1].transform.position.y + _deltaPlatformEnd);
        Vector2[] endPoints = new Vector2[] { new Vector2(-_lvlWidth / 2.0f, 0.0f), new Vector2(_lvlWidth / 2.0f, 0.0f) };
        endLevelBorder.GetComponent<EdgeCollider2D>().points = endPoints;
    }
}     
