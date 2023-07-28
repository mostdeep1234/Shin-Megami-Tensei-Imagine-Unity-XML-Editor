using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Newtonsoft.Json.Linq;

using System.Xml;

public class JSONConvertrManager : MonoBehaviour
{
    public int slide;

    string currentDirectory;

    string jsonText;

    [HideInInspector]
    public string jsonOutput;

    string testText;

    public string inputNameFile;

    public string jsonOutputFilename;

    public string jsonAfterChange;

    public Button buttonUI;

    public Text currentXMLText;

    [HideInInspector] public bool convert;

    public int currentIndex;

    [HideInInspector]
    public Newtonsoft.Json.Linq.JObject parsed;

    [Header("Loading Window")]
    public GameObject loadingWindow;

    [Header("Controller Object")]
    public GameObject controllerObject;

    [Header("CSkill Parser Object")]
    public GameObject cSkillParserObject;

    [Header("Enchant Parser Object")]
    public GameObject enchantParserObject;

    public static JSONConvertrManager JSONConverterCentral;

    public enum XMLState
    {
        CSkill, Enchant
    };

    public class CSkillProperties
    {
        public string skillIDText;

        public string skillNameText;

        public string skillDescText;

        public string skillIconText;

    }

    public class EnchantProperties
    {
        public string crystalNameID;

        public string demonID;

        public string itemID;

        public string difficultyID;

        public string usageID;


    }

    public class TarotProperties
    {
        public string name;

        public string desc;

        public string equipLevel;

        public string difficulty;

        public string equipTypes;

        public string tokusei1;

        public string tokusei2;
    }

    public class SoulProperties
    {
        public string name;

        public string desc;

        public string equipLevel;

        public string difficulty;

        public string equipTypes;

        public string tokusei1;

        public string tokusei2;
    }

    #region CSKILL DATA CLASS

    public CSkillProperties cSkillProperties = new CSkillProperties();

    #endregion

    #region ENCHANT DATA CLASS CHASSIS

    public EnchantProperties enchantProperties = new EnchantProperties();

    public TarotProperties tarotProperties = new TarotProperties();

    public SoulProperties soulProperties = new SoulProperties();

    #endregion

    public XMLState xmlState;

    private void OnEnable()
    {
        parsed = null;

        loadingWindow.SetActive(false);
    }

    private void Awake()
    {
        JSONConverterCentral = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
    }

    // Update is called once per frame
    void Update()
    {
        SlideCheck();
    }

    /// <summary>
    /// assign the operation in the button
    /// </summary>
    public void SetXmlState()
    {
        try
        {
            switch (xmlState)
            {
                case XMLState.CSkill:
                    loadingWindow.SetActive(false);
                    if (convert == false) ChangeState("cskill.xml", "CSkillData.json");
                    break;
                case XMLState.Enchant:
                    loadingWindow.SetActive(true);
                    if (convert == false) ChangeState("EnchantData.xml", "EnchantData.json");
                    break;
            }
        }
        catch
        {

        }

        return;
    }

    public void ChangeXML()
    {
        slide++;

        if (slide > 1) slide = 0;

        return;
    }

    public void SlideCheck()
    {
        switch (slide)
        {
            case 0:
                xmlState = XMLState.CSkill;
                currentXMLText.text = "CSKILL";
                break;
            case 1:
                xmlState = XMLState.Enchant;
                currentXMLText.text = "ENCHANT";
                break;
        }

        return;
    }

    public void ChangeState(string xmlname, string jsonname)
    {
        buttonUI.enabled = false;

        jsonOutputFilename = jsonname;

        string xmlPath = Path.Combine(currentDirectory, xmlname);

        JsonConvert(xmlPath, jsonOutputFilename);

        return;
    }

    public void JsonConvert(string xmlPath, string jsonOutputFileNameParams)
    {
        //reset value
        convert = true;

        //write file into the directory
        jsonOutput = Path.Combine(currentDirectory, jsonOutputFileNameParams);

        //create xml instance
        XmlDocument xml = new XmlDocument();

        //load xmls 
        xml.Load(xmlPath);

        //convert xml to json
        jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xml);

        //overwrite to the files directory
        File.WriteAllText(jsonOutput, jsonText);

        //do nothing
        StartCoroutine(PreviewJsonData());


        return;
    }

    IEnumerator PreviewJsonData()
    {
        yield return new WaitUntil(() => jsonOutput != null);

        PreviewJsonDataManager(jsonOutput);

        yield return new WaitUntil(() => testText != "");

        buttonUI.enabled = true;

        yield break;
    }

    public void PreviewJsonDataManager(string JSON)
    {
        switch (xmlState)
        {
            case XMLState.CSkill:
                try
                {
                    if (parsed == null)
                    {
                        parsed = JObject.Parse(File.ReadAllText(JSON));

                        JObject skillNameID = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][0];

                        JObject skillName = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][1];

                        JObject skillDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][2];

                        JObject skillIcon = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][3];

                        cSkillProperties.skillIDText = (string)skillNameID["#text"];

                        cSkillProperties.skillNameText = (string)skillName["#cdata-section"].ToString();

                        cSkillProperties.skillDescText = (string)skillDesc["#cdata-section"].ToString();

                        cSkillProperties.skillIconText = (string)skillIcon["#text"];

                        cSkillParserObject.SetActive(true);

                        controllerObject.SetActive(false);

                        convert = false;
                    }
                    else
                    {
                        JObject skillNameID = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][0];

                        JObject skillName = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][1];

                        JObject skillDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][2];

                        JObject skillIcon = (JObject)parsed["objects"]["object"][currentIndex]["member"][0]["object"]["member"][3];

                        cSkillProperties.skillIDText = (string)skillNameID["#text"];

                        cSkillProperties.skillNameText = (string)skillName["#cdata-section"].ToString();

                        cSkillProperties.skillDescText = (string)skillDesc["#cdata-section"].ToString();

                        cSkillProperties.skillIconText = (string)skillIcon["#text"];

                        cSkillParserObject.SetActive(true);

                        controllerObject.SetActive(false);

                        convert = false;
                    }
                }
                catch
                {
                    //reset parse
                    parsed = null;

                    //reset the convert
                    convert = false;
                }
                break;
            case XMLState.Enchant:
                try
                {
                    if (parsed == null)
                    {
                        parsed = JObject.Parse(File.ReadAllText(JSON));


                        JObject CrystalID = (JObject)parsed["objects"]["object"][currentIndex]["member"][0];

                        JObject DemonID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][0];

                        JObject itemID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][1];

                        JObject difficultyID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][2];

                        JObject usage = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][3];

                        //=====================================================================================================//


                        //tarots
                        JObject tarotName = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][0];
                        JObject tarotDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][1];
                        JObject tarotEquipLevel = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][2];
                        JObject tarotDifficulty = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][3];
                        JObject tarotEquipTypes = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][4];
                        JObject tarotTokusei = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][5];


                        tarotProperties.name = (string)tarotName["#cdata-section"].ToString();

                        tarotProperties.desc = (string)tarotDesc["#cdata-section"].ToString();

                        tarotProperties.equipLevel = (string)tarotEquipLevel["#text"].ToString();

                        tarotProperties.difficulty = (string)tarotDifficulty["#text"].ToString();

                        tarotProperties.equipTypes = (string)tarotEquipTypes["#text"].ToString();

                        string arrTokuseiJson = (string)tarotTokusei["element"].ToString();

                        JArray arrTokuseiJsonParsed = JArray.Parse(arrTokuseiJson);

                        tarotProperties.tokusei1 = (string)arrTokuseiJsonParsed[0].ToString();

                        tarotProperties.tokusei2 = (string)arrTokuseiJsonParsed[1].ToString();

                        ////////////////////////////////////////////////////////////////////////
                        ///SOUL
                        JObject soulName = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][0];
                        JObject soulDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][1];
                        JObject soulEquipLevel = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][2];
                        JObject soulDifficulty = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][3];
                        JObject soulEquipTypes = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][4];
                        JObject soulTokusei = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][5];

                        soulProperties.name = (string)soulName["#cdata-section"].ToString();

                        soulProperties.desc = (string)soulDesc["#cdata-section"].ToString();

                        soulProperties.equipLevel = (string)soulEquipLevel["#text"].ToString();

                        soulProperties.difficulty = (string)soulDifficulty["#text"].ToString();

                        soulProperties.equipTypes = (string)soulEquipTypes["#text"].ToString();

                        string arrTokuseiJsonSoul = (string)soulTokusei["element"].ToString();

                        JArray arrTokuseiJsonParsedSoul = JArray.Parse(arrTokuseiJson);

                        soulProperties.tokusei1 = (string)arrTokuseiJsonParsedSoul[0].ToString();

                        soulProperties.tokusei2 = (string)arrTokuseiJsonParsedSoul[1].ToString();

                        ////////////////////////////////////////////////////////////////////////


                        ////////////////////////////////////////////////////////////////////////////////////////

                        enchantProperties.crystalNameID = CrystalID["#text"].ToString();

                        enchantProperties.demonID = DemonID["#text"].ToString();

                        enchantProperties.itemID = itemID["#text"].ToString();

                        enchantProperties.difficultyID = difficultyID["#text"].ToString();

                        enchantProperties.usageID = usage["#text"].ToString();

                        enchantParserObject.SetActive(true);

                        controllerObject.SetActive(false);

                        convert = false;
                    }
                    else
                    {
                        JObject CrystalID = (JObject)parsed["objects"]["object"][currentIndex]["member"][0];

                        JObject DemonID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][0];

                        JObject itemID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][1];

                        JObject difficultyID = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][2];

                        JObject usage = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][3];

                        //=====================================================================================================//


                        //tarots
                        JObject tarotName = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][0];
                        JObject tarotDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][1];
                        JObject tarotEquipLevel = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][2];
                        JObject tarotDifficulty = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][3];
                        JObject tarotEquipTypes = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][4];
                        JObject tarotTokusei = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][4]["object"]["member"][5];


                        tarotProperties.name = (string)tarotName["#cdata-section"].ToString();

                        tarotProperties.desc = (string)tarotDesc["#cdata-section"].ToString();

                        tarotProperties.equipLevel = (string)tarotEquipLevel["#text"].ToString();

                        tarotProperties.difficulty = (string)tarotDifficulty["#text"].ToString();

                        tarotProperties.equipTypes = (string)tarotEquipTypes["#text"].ToString();

                        string arrTokuseiJson = (string)tarotTokusei["element"].ToString();

                        JArray arrTokuseiJsonParsed = JArray.Parse(arrTokuseiJson);

                        tarotProperties.tokusei1 = (string)arrTokuseiJsonParsed[0].ToString();

                        tarotProperties.tokusei2 = (string)arrTokuseiJsonParsed[1].ToString();

                        ////////////////////////////////////////////////////////////////////////
                        ///SOUL
                        JObject soulName = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][0];
                        JObject soulDesc = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][1];
                        JObject soulEquipLevel = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][2];
                        JObject soulDifficulty = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][3];
                        JObject soulEquipTypes = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][4];
                        JObject soulTokusei = (JObject)parsed["objects"]["object"][currentIndex]["member"][1]["object"]["member"][5]["object"]["member"][5];

                        soulProperties.name = (string)soulName["#cdata-section"].ToString();

                        soulProperties.desc = (string)soulDesc["#cdata-section"].ToString();

                        soulProperties.equipLevel = (string)soulEquipLevel["#text"].ToString();

                        soulProperties.difficulty = (string)soulDifficulty["#text"].ToString();

                        soulProperties.equipTypes = (string)soulEquipTypes["#text"].ToString();

                        string arrTokuseiJsonSoul = (string)soulTokusei["element"].ToString();

                        JArray arrTokuseiJsonParsedSoul = JArray.Parse(arrTokuseiJson);

                        soulProperties.tokusei1 = (string)arrTokuseiJsonParsedSoul[0].ToString();

                        soulProperties.tokusei2 = (string)arrTokuseiJsonParsedSoul[1].ToString();

                        ////////////////////////////////////////////////////////////////////////
                        enchantProperties.crystalNameID = CrystalID["#text"].ToString();

                        enchantProperties.demonID = DemonID["#text"].ToString();

                        enchantProperties.itemID = itemID["#text"].ToString();

                        enchantProperties.difficultyID = difficultyID["#text"].ToString();

                        enchantProperties.usageID = usage["#text"].ToString();

                        enchantParserObject.SetActive(true);

                        controllerObject.SetActive(false);

                        convert = false;
                    }
                }
                catch
                {
                    //reset parse
                    parsed = null;

                    //reset the convert
                    convert = false;
                }
                break;
        }




        return;
    }

    public void ConvertXmlBack()
    {
        if (parsed != null && Application.platform == RuntimePlatform.WindowsPlayer)
        {
            //change json
            jsonText = parsed.ToString();

            //To convert JSON text contained in string json into an XML node
            XmlDocument doc = (XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonText);

            //check the xml state
            switch (xmlState)
            {
                case XMLState.CSkill:

                    string path = Path.Combine(currentDirectory, "cskill.xml");

                    doc.Save(path);

                    break;
                case XMLState.Enchant:
                    string path2 = Path.Combine(currentDirectory, "EnchantData.xml");

                    doc.Save(path2);

                    break;
            }

            //reset the scene
            SceneManager.LoadScene(0);
        }



        return;
    }
}
