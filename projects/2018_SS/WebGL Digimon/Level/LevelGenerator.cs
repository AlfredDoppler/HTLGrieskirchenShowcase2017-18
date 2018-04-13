using Assets.Detection;
using Assets.Level;
using Assets.Level.Blocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public DullBlock defaultBlock;
    public CheckpointBlock checkpointBlock;
    public AirBlock airBlock;
    public LeapBoostBlock leapBoostBlock;
    public FinishBlock finishBlock;
    public SpeedBlock speedBlock;
    public CrumblyBlock crumblyBlock;
    public AutoLeapBoostBlock autoLeapBoostBlock;
    public SignDialogue signDialogue;

    public EnemyControl sukamon;
    public EnemyControl kingetemon;
    public EnemyControl giganticSukamon;

    public Player player;
    public static Player currentPlayerInstance;
    public readonly Vector3[] BOUNDRY_BLOCKS = new Vector3[4];

    private readonly string LEVEL_BASE_PATH = Directory.GetCurrentDirectory() + "\\Assets\\Level\\";

    public readonly List<LevelGeneratorObserver> OBSERVERS = new List<LevelGeneratorObserver>();

    private readonly List<string> LEVEL_IDS = new List<string>();

    private readonly int LEVEL_WIDTH = 100;

    private bool canSpawn = true;

    private readonly int LEVEL_HEIGHT = 30;

    private Texture2D currentLevelTex;
    private int currentLevelId;

    private Level currentLevel;

    private void Start()
    {
        DetectAllLevels();
    }

    public void ResistSpawnRequests()
    {
        canSpawn = false;
    }

    private void DetectAllLevels()
    {
        foreach (var file in Directory.GetFiles(LEVEL_BASE_PATH))
        {
            if (file.EndsWith(".png"))
            {
                int index = file.LastIndexOf("\\");
                int period = file.LastIndexOf(".");
                LEVEL_IDS.Add(file.Substring(index, period - index));
            }
        }

        if (LEVEL_IDS.Count == 0)
        {
            // notify the observer that no level files were found
            foreach (var observer in OBSERVERS)
            {
                observer.OnNoLevelFilesFound();
            }
        }
        else
        {
            // load the first level
            LoadNextLevel();
        }
    }

    private void PurgeLevel()
    {
        int blockLayer = LayerMask.NameToLayer("Block");
        int disposableLayer = LayerMask.NameToLayer("Disposable");
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.layer == blockLayer || obj.layer == disposableLayer)
            {
                Destroy(obj);
            }
        }
    }

    public void LoadNextLevel()
    {
        // purge scene of blocks
        PurgeLevel();

        CheckpointManager.ClearCheckpoints();
        CheckpointManager.ActiveCheckpoint = null;

        // load next level
        LoadLevel(currentLevelId++, LEVEL_IDS, true);
    }


    // no new level however rescued by a checkpoint
    public void ResetLevel()
    {
        if (!canSpawn)
            return;

        PurgeLevel();
        LoadLevel(currentLevelId - 1, LEVEL_IDS, true);
    }

    private void LoadLevel(int levelId, List<string> levelIds, bool spawnPlayer)
    {
        currentLevelTex = new Texture2D(LEVEL_WIDTH, LEVEL_HEIGHT);
        byte[] image = File.ReadAllBytes(LEVEL_BASE_PATH + "/" + levelIds[levelId] + ".png");
        currentLevelTex.LoadImage(image);
        PrepareLevel(spawnPlayer);
    }

    private void PrepareLevel(bool spawnPlayer)
    {
        Level level = new Level(LEVEL_WIDTH, LEVEL_HEIGHT);

        BOUNDRY_BLOCKS[0] = new Vector3(0, 0);
        BOUNDRY_BLOCKS[1] = new Vector3(LEVEL_WIDTH * 2.65f, 0);
        BOUNDRY_BLOCKS[2] = new Vector3(0, LEVEL_HEIGHT * 2.65f);
        BOUNDRY_BLOCKS[3] = new Vector3(0, 0);

        for (int x = 0; x < LEVEL_WIDTH; x++)
        {
            for (int y = 0; y < LEVEL_HEIGHT; y++)
            {
                Color color = currentLevelTex.GetPixel(x, y);
                if (ColorMap.MatchesColor(color, ColorMap.THEME_FOREST)) // read in theme
                {
                    ThemeLoader.LoadForestTheme();
                }
                else if (ColorMap.MatchesColor(color, ColorMap.THEME_DESERT))
                {
                    ThemeLoader.LoadDesertTheme();
                }
                else if (ColorMap.MatchesColor(color, ColorMap.DULL_BLOCK))
                {
                    DullBlock block = Instantiate(defaultBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.CHECKPOINT))
                {
                    CheckpointBlock block = Instantiate(checkpointBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                    CheckpointManager.AddCheckpoint(block);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.AIR_BLOCK))
                {
                    AirBlock block = Instantiate(airBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.SPEED_BLOCK))
                {
                    SpeedBlock block = Instantiate(speedBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.CRUMBLY_BLOCK))
                {
                    CrumblyBlock block = Instantiate(crumblyBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.FINISH))
                {
                    FinishBlock block = Instantiate(finishBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.LEAP_BOOST_BLOCK))
                {
                    LeapBoostBlock block = Instantiate(leapBoostBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.AUTO_LEAP_BOOST_BLOCK))
                {
                    AutoLeapBoostBlock block = Instantiate(autoLeapBoostBlock);
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.DISGUISED_CRUMBLY_BLOCK))
                {
                    CrumblyBlock block = Instantiate(crumblyBlock);
                    Disguise disguise = block.gameObject.AddComponent<Disguise>();
                    disguise.identity = crumblyBlock.GetComponent<SpriteRenderer>().sprite;
                    disguise.camouflage = defaultBlock.GetComponent<SpriteRenderer>().sprite;
                    disguise.HideIdentity();
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.DISGUISED_AUTO_LEAP_BOOST_BLOCK))
                {
                    AutoLeapBoostBlock block = Instantiate(autoLeapBoostBlock);
                    Disguise disguise = block.gameObject.AddComponent<Disguise>();
                    disguise.identity = autoLeapBoostBlock.GetComponent<SpriteRenderer>().sprite;
                    disguise.camouflage = defaultBlock.GetComponent<SpriteRenderer>().sprite;
                    disguise.HideIdentity();
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.DISGUISED_DULL_BLOCK))
                {
                    DullBlock block = Instantiate(defaultBlock);
                    Disguise disguise = block.gameObject.AddComponent<Disguise>();
                    disguise.identity = defaultBlock.GetComponent<SpriteRenderer>().sprite;
                    disguise.camouflage = null;
                    disguise.HideIdentity();
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.GHOST_BLOCK))
                {
                    DullBlock block = Instantiate(defaultBlock);
                    Disguise disguise = block.gameObject.AddComponent<Disguise>();
                    disguise.identity = null;
                    disguise.camouflage = null;
                    disguise.HideIdentity();
                    block.block = Block.init(x, y, level, block.gameObject);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.ENEMY_SUKAMON))
                {
                    // spawn sukamon
                    EnemyControl ec = Instantiate(sukamon);
                    ec.transform.position = new Vector3(x * 2.65f, y * 2.65f + 0.5f);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.DIGIVICE))
                {

                }
                else if (ColorMap.MatchesColor(color, ColorMap.ENEMY_KINGETEMON))
                {
                    EnemyControl ec = Instantiate(kingetemon);
                    ec.transform.position = new Vector3(x * 2.65f, y * 2.65f + 0.5f);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.ENEMY_SUKAMON_STATIC))
                {
                    EnemyControl ec = Instantiate(sukamon);
                    ec.blockRadius = 0;
                    ec.transform.position = new Vector3(x * 2.65f, y * 2.65f + 0.5f);
                } else if(ColorMap.MatchesColor(color, ColorMap.ENEMY_GIGANTIC_SUKAMON))
                {
                    EnemyControl ec = Instantiate(giganticSukamon);
                    ec.blockRadius = 20;
                    ec.moveSpeed = 8;
                    //ec.health *= 5;
                    ec.transform.position = new Vector3(x * 2.65f, y * 2.65f + 0.5f);
                }
                else if (ColorMap.MatchesColor(color, ColorMap.WOODEN_SIGN))
                {
                    SignDialogue sd = Instantiate(signDialogue);
                    sd.transform.position = new Vector3(x * 2.65f, y * 2.65f + 0.5f);
                }
            }
        }

        currentLevel = level;

        foreach (var observer in OBSERVERS)
        {
            observer.OnLevelLoaded(level);
        }

        if (spawnPlayer)
        {
            int score = 1;
            if(currentPlayerInstance != null)
            {
                score = currentPlayerInstance.score;
            }
            Vector3 pos = CheckpointManager.ActiveCheckpoint.transform.position;
            Player p = Instantiate(player);
            p.transform.position = new Vector3(pos.x, pos.y + 5, pos.z);
            currentPlayerInstance = p;
            p.score = score;
            Debug.Log(p.score);

            Camera.main.GetComponent<CamCapture>().player = p;
            Camera.main.GetComponent<CamCapture>().boundryBlocks = BOUNDRY_BLOCKS;
        }
    }

}
