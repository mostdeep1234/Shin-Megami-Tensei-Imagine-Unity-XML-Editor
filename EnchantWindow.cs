using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using Newtonsoft.Json.Linq;

public class EnchantWindow : MonoBehaviour
{
    public enum SoulTarot
    {
        soul, tarot
    };

    [Header("Tarot Window")]
    public GameObject tarotWindow;

    [Header("Soul Window")]
    public GameObject soulWindow;

    public SoulTarot soulTarot;

    [System.Serializable]
    public class EnchantTexts
    {
        [Header("Crystal ID")]
        public InputField CrystalID;

        [Header("Demon ID")]
        public InputField DemonID;

        [Header("Item ID")]
        public InputField ItemID;

        [Header("Difficulty ID")]
        public InputField difficultyID;

        [Header("Usage ID")]
        public InputField usageID; 
    }

    [System.Serializable]
    public class SoulTexts
    {
        [Header("Name Input Field")]
        public InputField name;

        [Header("Description Input Field")]
        public InputField desc;

        [Header("equipLevel Input Field")]
        public InputField equipLevel;

        [Header("difficulty Input Field")]
        public InputField difficulty;

        [Header("equipTypes Input Field")]
        public InputField equipTypes;

        [Header("Tokusei Input Field")]
        public InputField tokusei1;

        [Header("Tokusei2 Input Field")]
        public InputField tokusei2;
    }

    [System.Serializable]
    public class TarotTexts
    {
        [Header("Name Input Field")]
        public InputField name;

        [Header("Description Input Field")]
        public InputField desc;

        [Header("equipLevel Input Field")]
        public InputField equipLevel;

        [Header("difficulty Input Field")]
        public InputField difficulty;

        [Header("equipTypes Input Field")]
        public InputField equipTypes;

        [Header("Tokusei Input Field")]
        public InputField tokusei1;

        [Header("Tokusei2 Input Field")]
        public InputField tokusei2;
    }

    public EnchantTexts enchantTexts = new EnchantTexts();

    public SoulTexts soulTexts = new SoulTexts();

    public TarotTexts tarotTexts = new TarotTexts();

    private void OnEnable()
    {
        JSONConvertrManager.JSONConverterCentral.loadingWindow.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());

        enchantTexts.CrystalID.onEndEdit.AddListener(delegate { CrystalIDOnEndEdit(); });

        enchantTexts.DemonID.onEndEdit.AddListener(delegate { DemonIDOnEndEdit(); });

        enchantTexts.ItemID.onEndEdit.AddListener(delegate { ItemIDOnEndEdit(); });

        enchantTexts.difficultyID.onEndEdit.AddListener(delegate { DiffcultyIDOnEdit(); });

        enchantTexts.usageID.onEndEdit.AddListener(delegate { UsageIDOnEdit(); });

        //////////////////////////////////////////////////////////////////////////

        ///TAROT INPUT FIELD
        tarotTexts.name.onEndEdit.AddListener(delegate { TarotNameOnEndEdit(); });

        tarotTexts.desc.onEndEdit.AddListener(delegate { TarotDescOnEndEdit(); });

        tarotTexts.equipLevel.onEndEdit.AddListener(delegate { TarotEquipLevelOnEndEdit(); });

        tarotTexts.difficulty.onEndEdit.AddListener(delegate { TarotDifficultyOnEndEdit(); });

        tarotTexts.equipTypes.onEndEdit.AddListener(delegate { TarotEquipTypesOnEndEdit(); });

        tarotTexts.tokusei1.onEndEdit.AddListener(delegate { TarotTokusei1OnEndEdit(); });

        tarotTexts.tokusei2.onEndEdit.AddListener(delegate { TarotTokusei2OnEndEdit(); });

        ///SOUL INPUT FIELD
        soulTexts.name.onEndEdit.AddListener(delegate { SoulNameOnEndEdit(); });

        soulTexts.desc.onEndEdit.AddListener(delegate { SoulNameOnEndEdit(); });

        soulTexts.equipLevel.onEndEdit.AddListener(delegate { SoulEquipLevelOnEndEdit(); });

        soulTexts.difficulty.onEndEdit.AddListener(delegate { SoulDifficultyOnEndEdit(); });

        soulTexts.equipTypes.onEndEdit.AddListener(delegate { SoulEquipTypesOnEndEdit(); });

        soulTexts.tokusei1.onEndEdit.AddListener(delegate { SoulTokuse1OnEndEdit(); });

        soulTexts.tokusei2.onEndEdit.AddListener(delegate { SoulTokusei2OnEndEdit(); });
    }

    // Update is called once per frame
    void Update()
    {
        SoulTarotManager();
    }

    void SoulTarotManager ()
    {
        switch(soulTarot)
        {
            case SoulTarot.tarot:
                tarotWindow.SetActive(true);
                soulWindow.SetActive(false);
                break;
            case SoulTarot.soul:
                tarotWindow.SetActive(false);
                soulWindow.SetActive(true);
                break;
        }

        return;
    }

    IEnumerator Setup ()
    {
        yield return new WaitUntil(() => JSONConvertrManager.JSONConverterCentral.enchantProperties.crystalNameID != "");

        enchantTexts.CrystalID.text = JSONConvertrManager.JSONConverterCentral.enchantProperties.crystalNameID;

        enchantTexts.DemonID.text = JSONConvertrManager.JSONConverterCentral.enchantProperties.demonID;

        enchantTexts.ItemID.text = JSONConvertrManager.JSONConverterCentral.enchantProperties.itemID;

        enchantTexts.difficultyID.text = JSONConvertrManager.JSONConverterCentral.enchantProperties.difficultyID;

        enchantTexts.usageID.text = JSONConvertrManager.JSONConverterCentral.enchantProperties.usageID;


        //tarots
        tarotTexts.name.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.name;
        tarotTexts.desc.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.desc;
        tarotTexts.equipLevel.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.equipLevel;
        tarotTexts.difficulty.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.difficulty;
        tarotTexts.equipTypes.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.equipTypes;
        tarotTexts.tokusei1.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.tokusei1;
        tarotTexts.tokusei2.text = JSONConvertrManager.JSONConverterCentral.tarotProperties.tokusei2;

        //soul
        soulTexts.name.text = JSONConvertrManager.JSONConverterCentral.soulProperties.name;
        soulTexts.desc.text = JSONConvertrManager.JSONConverterCentral.soulProperties.desc;
        soulTexts.equipLevel.text = JSONConvertrManager.JSONConverterCentral.soulProperties.equipLevel;
        soulTexts.difficulty.text = JSONConvertrManager.JSONConverterCentral.soulProperties.difficulty;
        soulTexts.equipTypes.text = JSONConvertrManager.JSONConverterCentral.soulProperties.equipTypes;
        soulTexts.tokusei1.text = JSONConvertrManager.JSONConverterCentral.soulProperties.tokusei1;
        soulTexts.tokusei2.text = JSONConvertrManager.JSONConverterCentral.soulProperties.tokusei2;

        yield break;
    }

    public void CrystalIDOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][0]["#text"] = enchantTexts.CrystalID.text;

        return;
    }

    public void DemonIDOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][0]["#text"] = enchantTexts.DemonID.text;

        return;
    }

    public void ItemIDOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][1]["#text"] = enchantTexts.ItemID.text;

        return;
    }

    public void DiffcultyIDOnEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][2]["#text"] = enchantTexts.difficultyID.text;

        return;
    }

    public void UsageIDOnEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][3]["#text"] = enchantTexts.usageID.text;

        return;
    }

    #region TAROT DELEGATION METHODS
    public void TarotNameOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][0]["#cdata-section"] = tarotTexts.name.text;

        return;
    }

    public void TarotDescOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][1]["#cdata-section"] = tarotTexts.desc.text;

        return;
    }

    public void TarotEquipLevelOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][2]["#text"] = tarotTexts.equipLevel.text;

        return;
    }

    public void TarotDifficultyOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][3]["#text"] = tarotTexts.difficulty.text;

        return;
    }

    public void TarotEquipTypesOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][4]["#text"] = tarotTexts.equipTypes.text;

        return;
    }

    public void TarotTokusei1OnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][5]["element"][0] = tarotTexts.tokusei1.text;

        return;
    }

    public void TarotTokusei2OnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][5]["element"][1]= tarotTexts.tokusei2.text;

        return;
    }

    #endregion

    #region SOUL DELEGATION METHOD
    public void SoulNameOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][0]["#cdata-section"] = soulTexts.name.text;

        return;
    }
    public void SoulDescOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][1]["#cdata-section"] = soulTexts.desc.text;

        return;
    }
    public void SoulEquipLevelOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][2]["#text"] = soulTexts.equipLevel.text;

        return;
    }
    public void SoulDifficultyOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][3]["#text"] = soulTexts.difficulty.text;

        return;
    }
    public void SoulEquipTypesOnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][4]["#text"] = soulTexts.equipTypes.text;

        return;
    }
    public void SoulTokuse1OnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
            ["object"]["member"][5]["object"]["member"][5]["element"][0] = soulTexts.tokusei1.text;

        return;
    }
    public void SoulTokusei2OnEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][1]
           ["object"]["member"][5]["object"]["member"][5]["element"][1] = soulTexts.tokusei2.text;

        return;
    }

    #endregion

    public void Right()
    {
        JSONConvertrManager.JSONConverterCentral.currentIndex++;

        JSONConvertrManager.JSONConverterCentral.PreviewJsonDataManager(JSONConvertrManager.JSONConverterCentral.jsonOutput);

        StartCoroutine(Setup());

        return;
    }

   

    public void Left()
    {
        if (JSONConvertrManager.JSONConverterCentral.currentIndex > 0) JSONConvertrManager.JSONConverterCentral.currentIndex--;

        JSONConvertrManager.JSONConverterCentral.PreviewJsonDataManager(JSONConvertrManager.JSONConverterCentral.jsonOutput);

        StartCoroutine(Setup());

        return;
    }

    public void RightPhase ()
    {
        if(soulTarot == SoulTarot.tarot)soulTarot = SoulTarot.soul;
        
        return;
    }

    public void LeftPhase ()
    {
        if(soulTarot == SoulTarot.soul)soulTarot = SoulTarot.tarot;

        return;
    }

    public void EnchantWindowBack ()
    {
        SceneManager.LoadScene(0);

        return;
    }
}
