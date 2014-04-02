using UnityEngine;
using System.Collections;

public class RandomFact : MonoBehaviour
{

	public TextMesh text;

	void Start ()
	{
		text.text = facts [Random.Range (0, facts.Length)];
	}

	private string[]  facts = {
		"Sickle Cell Disease is the\nmost common inherited\nblood disorder in the\nUnited States\n",
		"Sickle Cell Disease or its\ntrait occurs in 5% of the\nworldwide population\n",
		"Approximately 100,000\npersons in the United\nStates are affected\nby Sickle Cell Disease\n",
		"Sickle hemoglobin affects\nthe shape and function of\nred blood cells\n",
		"People who have normal\nhemoglobin have round,\ndoughnut shaped red\nblood cells\n",
		"Red blood cells move\nthrough blood vessels\nto carry oxygen to all\nparts of the body\n",
		"Pain is a characteristic\nfeature of the sickle\ncell disease\n",
		"With sickle cell disease,\npain is the most common\nreason for emergency\nroom and hospital visits\n",
		"Sickle cells die early,\nwhich causes a constant\nshortage of red blood\ncells\n",
		"Sickle cell disease occurs\nin about 1 out of every\n500 Black or African-\nAmerican births\n",
		"Sickle cell disease occurs\nin about 1 out of every\n36,000 Hispanic-\nAmerican births\n",
		"Sickle cells travel through\nsmall blood vessels\ngetting stuck and clogging\nthe blood flow\n",
		"Sickle Cell Crisis often\noccurs suddenly and\nunpredictably\n",
		"A Sickle Cell Crisis can\nlast from hours to days\n",
		"Drinking plenty of water\ncan reduce pain episodes\n",
		"The pain can be so severe\nthat it requires treatment\nwith strong medications\nlike morphine\n",
		"Hydroxyurea is a\nmedication that can help\nreduce the number of\npain crises\n",
		"People with sickle cell\ndisease are more at risk\nfor infections\n",
		"Pneumonia is a leading\ncause of death in infants\nand young children with\nsickle cell disease\n",
		"Regular health checkups\nwith a primary care doctor\ncan help prevent some\nserious problems\n"
	};
}
