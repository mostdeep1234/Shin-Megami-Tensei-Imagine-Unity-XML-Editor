using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSkillWindow : MonoBehaviour
{
    [System.Serializable]
    public class Texts
    {
        [Header("Text CSkill ID")]
        public Text textCSkillID;

        [Header("Text CSkill ID Real")]
        public InputField textCSkillIDReal;

        [Header("Text Skill Name")]
        public InputField textSkillName;

        [Header("Text Skill Desc")]
        public InputField textSkillDesc;

        [Header("Text Skill Icon ID")]
        public InputField textSkillIconID;
    }

    public Texts texts = new Texts();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());

        texts.textCSkillIDReal.onEndEdit.AddListener(delegate { IDSkillEndEdit(); });

        texts.textSkillName.onEndEdit.AddListener(delegate { NameSkillEndEdit(); });

        texts.textSkillDesc.onEndEdit.AddListener(delegate { DescSkillEndEdit(); });

        texts.textSkillIconID.onEndEdit.AddListener(delegate { IconSkillEndEdit(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Setup ()
    {
        yield return new WaitUntil(() => JSONConvertrManager.JSONConverterCentral.cSkillProperties.skillNameText != "");

        texts.textCSkillIDReal.text = JSONConvertrManager.JSONConverterCentral.cSkillProperties.skillIDText;

        texts.textSkillName.text = JSONConvertrManager.JSONConverterCentral.cSkillProperties.skillNameText;

        texts.textSkillDesc.text = JSONConvertrManager.JSONConverterCentral.cSkillProperties.skillDescText;

        texts.textSkillIconID.text = JSONConvertrManager.JSONConverterCentral.cSkillProperties.skillIconText;

        yield break;
    }

    public void Right ()
    {
        JSONConvertrManager.JSONConverterCentral.currentIndex++;

        texts.textCSkillID.text = JSONConvertrManager.JSONConverterCentral.currentIndex.ToString();

        JSONConvertrManager.JSONConverterCentral.PreviewJsonDataManager(JSONConvertrManager.JSONConverterCentral.jsonOutput);

        StartCoroutine(Setup());

        return;
    }

    public void Left ()
    {
        if(JSONConvertrManager.JSONConverterCentral.currentIndex > 0)JSONConvertrManager.JSONConverterCentral.currentIndex--;

        texts.textCSkillID.text = JSONConvertrManager.JSONConverterCentral.currentIndex.ToString();

        JSONConvertrManager.JSONConverterCentral.PreviewJsonDataManager(JSONConvertrManager.JSONConverterCentral.jsonOutput);

        StartCoroutine(Setup());

        return;
    }

    public void AddFromThisData ()
    {


        return;
    }

    public void BackToInitalizeWindow ()
    {
        JSONConvertrManager.JSONConverterCentral.controllerObject.SetActive(true);

        this.gameObject.SetActive(false);

        return;
    }

    public void IDSkillEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][0]["object"]["member"][2]["#cdata-section"] = texts.textCSkillIDReal.text;

        return;
    }

    public void NameSkillEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][0]["object"]["member"][1]["#cdata-section"] = texts.textSkillName.text;

        return;
    }

    public void DescSkillEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][0]["object"]["member"][2]["#cdata-section"] = texts.textSkillDesc.text;

        return;
    }

    public void IconSkillEndEdit ()
    {
        JSONConvertrManager.JSONConverterCentral.parsed["objects"]["object"][JSONConvertrManager.JSONConverterCentral.currentIndex]["member"][0]["object"]["member"][2]["#cdata-section"] = texts.textSkillIconID.text;

        return;
    }
}

