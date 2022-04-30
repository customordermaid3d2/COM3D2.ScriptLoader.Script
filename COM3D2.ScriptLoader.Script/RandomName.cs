﻿using HarmonyLib;
using MaidStatus;
using scoutmode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.ScriptLoader.Script
{
    internal class RandomName
    {
        public static void Main()
        {
            try
            {
				// name list setting
				
				List<string> namesl = new List<string>();
				namesl.AddRange(names);
				namesl.AddRange(first_names);
				namesl.AddRange(first_names_en);
				namesl.AddRange(last_names);
				namesl.AddRange(last_names_en);				
				var nameArr = namesl.Distinct().ToArray();

				// set names to game

				var type = AccessTools.TypeByName("MaidRandomName");
				AccessTools.Field(type, "first_name_array").SetValue(null, nameArr);
				AccessTools.Field(type, "first_name_array_en").SetValue(null, nameArr);
				AccessTools.Field(type, "last_name_array").SetValue(null, nameArr);
				AccessTools.Field(type, "last_name_array_en").SetValue(null, nameArr);
				// Man
				//AccessTools.Field(type, "player_name_array").SetValue(null, nameArr);
				//AccessTools.Field(type, "player_name_array_en").SetValue(null, nameArr);
			}
			catch (Exception e)
            {
				Debug.LogError($"RandomName {e}");
            }
		}

		// my names

		private static string[] names = new string[]
		{
			"Lilly",
			"Olivia",
"Emma",
"Ava",
"Charlotte",
"Sophia",
"Amelia",
"Isabella",
"Mia",
"Evelyn",
"Harper",
"Camila",
"Gianna",
"Abigail",
"Luna",
"Ella",
"Elizabeth",
"Sofia",
"Emily",
"Avery",
"Mila",
"Scarlett",
"Eleanor",
"Madison",
"Layla",
"Penelope",
"Aria",
"Chloe",
"Grace",
"Ellie",
"Nora",
"Hazel",
"Zoey",
"Riley",
"Victoria",
"Lily",
"Aurora",
"Violet",
"Nova",
"Hannah",
"Emilia",
"Zoe",
"Stella",
"Everly",
"Isla",
"Leah",
"Lillian",
"Addison",
"Willow",
"Lucy",
"Paisley",
"Natalie",
"Naomi",
"Eliana",
"Brooklyn",
"Elena",
"Aubrey",
"Claire",
"Ivy",
"Kinsley",
"Audrey",
"Maya",
"Genesis",
"Skylar",
"Bella",
"Aaliyah",
"Madelyn",
"Savannah",
"Anna",
"Delilah",
"Serenity",
"Caroline",
"Kennedy",
"Valentina",
"Ruby",
"Sophie",
"Alice",
"Gabriella",
"Sadie",
"Ariana",
"Allison",
"Hailey",
"Autumn",
"Nevaeh",
"Natalia",
"Quinn",
"Josephine",
"Sarah",
"Cora",
"Emery",
"Samantha",
"Piper",
"Leilani",
"Eva",
"Everleigh",
"Madeline",
"Lydia",
"Jade",
"Peyton",
"Brielle",
"Adeline",
"Vivian",
"Rylee",
"Clara",
"Raelynn",
"Melanie",
"Melody",
"Julia",
"Athena",
"Maria",
"Liliana",
"Hadley",
"Arya",
"Rose",
"Reagan",
"Eliza",
"Adalynn",
"Kaylee",
"Lyla",
"Mackenzie",
"Alaia",
"Isabelle",
"Charlie",
"Arianna",
"Mary",
"Remi",
"Margaret",
"Iris",
"Parker",
"Ximena",
"Eden",
"Ayla",
"Kylie",
"Elliana",
"Josie",
"Katherine",
"Faith",
"Alexandra",
"Eloise",
"Adalyn",
"Amaya",
"Jasmine",
"Amara",
"Daisy",
"Reese",
"Valerie",
"Brianna",
"Cecilia",
"Andrea",
"Summer",
"Valeria",
"Norah",
"Ariella",
"Esther",
"Ashley",
"Emerson",
"Aubree",
"Isabel",
"Anastasia",
"Ryleigh",
"Khloe",
"Taylor",
"Londyn",
"Lucia",
"Emersyn",
"Callie",
"Sienna",
"Blakely",
"Kehlani",
"Genevieve",
"Alina",
"Bailey",
"Juniper",
"Maeve",
"Molly",
"Harmony",
"Georgia",
"Magnolia",
"Catalina",
"Freya",
"Juliette",
"Sloane",
"June",
"Sara",
"Ada",
"Kimberly",
"River",
"Ember",
"Juliana",
"Aliyah",
"Millie",
"Brynlee",
"Teagan",
"Morgan",
"Jordyn",
"London",
"Alaina",
"Olive",
"Rosalie",
"Alyssa",
"Ariel",
"Finley",
"Arabella",
"Journee",
"Hope",
"Leila",
"Alana",
"Gemma",
"Vanessa",
"Gracie",
"Noelle",
"Marley",
"Elise",
"Presley",
"Kamila",
"Zara",
"Amy",
"Kayla",
"Payton",
"Blake",
"Ruth",
"Alani",
"Annabelle",
"Sage",
"Aspen",
"Laila",
"Lila",
"Rachel",
"Trinity",
"Daniela",
"Alexa",
"Lilly",
"Lauren",
"Elsie",
"Margot",
"Adelyn",
"Zuri",
"Brooke",
"Sawyer",
"Lilah",
"Lola",
"Selena",
"Mya",
"Sydney",
"Diana",
"Ana",
"Vera",
"Alayna",
"Nyla",
"Elaina",
"Rebecca",
"Angela",
"Kali",
"Alivia",
"Raegan",
"Rowan",
"Phoebe",
"Camilla",
"Joanna",
"Malia",
"Vivienne",
"Dakota",
"Brooklynn",
"Evangeline",
"Camille",
"Jane",
"Nicole",
"Catherine",
"Jocelyn",
"Julianna",
"Lena",
"Lucille",
"Mckenna",
"Paige",
"Adelaide",
"Charlee",
"Mariana",
"Myla",
"Mckenzie",
"Tessa",
"Miriam",
"Oakley",
"Kailani",
"Alayah",
"Amira",
"Adaline",
"Phoenix",
"Milani",
"Annie",
"Lia",
"Angelina",
"Harley",
"Cali",
"Maggie",
"Hayden",
"Leia",
"Fiona",
"Briella",
"Journey",
"Lennon",
"Saylor",
"Jayla",
"Kaia",
"Thea",
"Adriana",
"Mariah",
"Juliet",
"Oaklynn",
"Kiara",
"Alexis",
"Haven",
"Aniyah",
"Delaney",
"Gracelynn",
"Kendall",
"Winter",
"Lilith",
"Logan",
"Amiyah",
"Evie",
"Alexandria",
"Gracelyn",
"Gabriela",
"Sutton",
"Harlow",
"Madilyn",
"Makayla",
"Evelynn",
"Gia",
"Nina",
"Amina",
"Giselle",
"Brynn",
"Blair",
"Amari",
"Octavia",
"Michelle",
"Talia",
"Demi",
"Alaya",
"Kaylani",
"Izabella",
"Fatima",
"Tatum",
"Makenzie",
"Lilliana",
"Arielle",
"Palmer",
"Melissa",
"Willa",
"Samara",
"Destiny",
"Dahlia",
"Celeste",
"Ainsley",
"Rylie",
"Reign",
"Laura",
"Adelynn",
"Gabrielle",
"Remington",
"Wren",
"Brinley",
"Amora",
"Lainey",
"Collins",
"Lexi",
"Aitana",
"Alessandra",
"Kenzie",
"Raelyn",
"Elle",
"Everlee",
"Haisley",
"Hallie",
"Wynter",
"Daleyza",
"Gwendolyn",
"Paislee",
"Ariyah",
"Veronica",
"Heidi",
"Anaya",
"Cataleya",
"Kira",
"Avianna",
"Felicity",
"Aylin",
"Miracle",
"Sabrina",
"Lana",
"Ophelia",
"Elianna",
"Royalty",
"Madeleine",
"Esmeralda",
"Joy",
"Kalani",
"Esme",
"Jessica",
"Leighton",
"Ariah",
"Makenna",
"Nylah",
"Viviana",
"Camryn",
"Cassidy",
"Dream",
"Luciana",
"Maisie",
"Stevie",
"Kate",
"Lyric",
"Daniella",
"Alicia",
"Daphne",
"Frances",
"Charli",
"Raven",
"Paris",
"Nayeli",
"Serena",
"Heaven",
"Bianca",
"Helen",
"Hattie",
"Averie",
"Mabel",
"Selah",
"Allie",
"Marlee",
"Kinley",
"Regina",
"Carmen",
"Jennifer",
"Jordan",
"Alison",
"Stephanie",
"Maren",
"Kayleigh",
"Angel",
"Annalise",
"Jacqueline",
"Braelynn",
"Emory",
"Rosemary",
"Scarlet",
"Amanda",
"Danielle",
"Emelia",
"Ryan",
"Carolina",
"Astrid",
"Kensley",
"Shiloh",
"Maci",
"Francesca",
"Rory",
"Celine",
"Kamryn",
"Zariah",
"Liana",
"Poppy",
"Maliyah",
"Keira",
"Skyler",
"Noa",
"Skye",
"Nadia",
"Addilyn",
"Rosie",
"Eve",
"Sarai",
"Edith",
"Jolene",
"Maddison",
"Meadow",
"Charleigh",
"Matilda",
"Elliott",
"Madelynn",
"Holly",
"Leona",
"Azalea",
"Katie",
"Mira",
"Ari",
"Kaitlyn",
"Danna",
"Cameron",
"Kyla",
"Bristol",
"Kora",
"Armani",
"Nia",
"Malani",
"Dylan",
"Remy",
"Maia",
"Dior",
"Legacy",
"Alessia",
"Shelby",
"Maryam",
"Sylvia",
"Yaretzi",
"Lorelei",
"Madilynn",
"Abby",
"Helena",
"Jimena",
"Elisa",
"Renata",
"Amber",
"Aviana",
"Carter",
"Emmy",
"Haley",
"Alondra",
"Elaine",
"Erin",
"April",
"Emely",
"Imani",
"Kennedi",
"Lorelai",
"Hanna",
"Kelsey",
"Aurelia",
"Colette",
"Jaliyah",
"Kylee",
"Macie",
"Aisha",
"Dorothy",
"Charley",
"Kathryn",
"Adelina",
"Adley",
"Monroe",
"Sierra",
"Ailani",
"Miranda",
"Mikayla",
"Alejandra",
"Amirah",
"Jada",
"Jazlyn",
"Jenna",
"Jayleen",
"Beatrice",
"Kendra",
"Lyra",
"Nola",
"Emberly",
"Mckinley",
"Myra",
"Katalina",
"Antonella",
"Zelda",
"Alanna",
"Amaia",
"Priscilla",
"Briar",
"Kaliyah",
"Itzel",
"Oaklyn",
"Alma",
"Mallory",
"Novah",
"Amalia",
"Fernanda",
"Alia",
"Angelica",
"Elliot",
"Justice",
"Mae",
"Cecelia",
"Gloria",
"Ariya",
"Virginia",
"Cheyenne",
"Aleah",
"Jemma",
"Henley",
"Meredith",
"Leyla",
"Lennox",
"Ensley",
"Zahra",
"Reina",
"Frankie",
"Lylah",
"Nalani",
"Reyna",
"Saige",
"Ivanna",
"Aleena",
"Emerie",
"Ivory",
"Leslie",
"Alora",
"Ashlyn",
"Bethany",
"Bonnie",
"Sasha",
"Xiomara",
"Salem",
"Adrianna",
"Dayana",
"Clementine",
"Karina",
"Karsyn",
"Emmie",
"Julie",
"Julieta",
"Briana",
"Carly",
"Macy",
"Marie",
"Oaklee",
"Christina",
"Malaysia",
"Ellis",
"Irene",
"Anne",
"Anahi",
"Mara",
"Rhea",
"Davina",
"Dallas",
"Jayda",
"Mariam",
"Skyla",
"Siena",
"Elora",
"Marilyn",
"Jazmin",
"Megan",
"Rosa",
"Savanna",
"Allyson",
"Milan",
"Coraline",
"Johanna",
"Melany",
"Chelsea",
"Michaela",
"Melina",
"Angie",
"Cassandra",
"Yara",
"Kassidy",
"Liberty",
"Lilian",
"Avah",
"Anya",
"Laney",
"Navy",
"Opal",
"Amani",
"Zaylee",
"Mina",
"Sloan",
"Romina",
"Ashlynn",
"Aliza",
"Liv",
"Malaya",
"Blaire",
"Janelle",
"Kara",
"Analia",
"Hadassah",
"Hayley",
"Karla",
"Chaya",
"Cadence",
"Kyra",
"Alena",
"Ellianna",
"Katelyn",
"Kimber",
"Laurel",
"Lina",
"Capri",
"Braelyn",
"Faye",
"Kamiyah",
"Kenna",
"Louise",
"Calliope",
"Kaydence",
"Nala",
"Tiana",
"Aileen",
"Sunny",
"Zariyah",
"Milana",
"Giuliana",
"Eileen",
"Elodie",
"Rayna",
"Monica",
"Galilea",
"Journi",
"Lara",
"Marina",
"Aliana",
"Harmoni",
"Jamie",
"Holland",
"Emmalyn",
"Lauryn",
"Chanel",
"Tinsley",
"Jessie",
"Lacey",
"Elyse",
"Janiyah",
"Jolie",
"Ezra",
"Marleigh",
"Roselyn",
"Lillie",
"Louisa",
"Madisyn",
"Penny",
"Kinslee",
"Treasure",
"Zaniyah",
"Estella",
"Jaylah",
"Khaleesi",
"Alexia",
"Dulce",
"Indie",
"Maxine",
"Waverly",
"Giovanna",
"Miley",
"Saoirse",
"Estrella",
"Greta",
"Rosalia",
"Mylah",
"Teresa",
"Bridget",
"Kelly",
"Adalee",
"Aubrie",
"Lea",
"Harlee",
"Anika",
"Itzayana",
"Hana",
"Kaisley",
"Mikaela",
"Naya",
"Avalynn",
"Margo",
"Sevyn",
"Florence",
"Keilani",
"Lyanna",
"Joelle",
"Kataleya",
"Royal",
"Averi",
"Kallie",
"Winnie",
"Baylee",
"Martha",
"Pearl",
"Alaiya",
"Rayne",
"Sylvie",
"Brylee",
"Jazmine",
"Ryann",
"Kori",
"Noemi",
"Haylee",
"Julissa",
"Celia",
"Laylah",
"Rebekah",
"Rosalee",
"Aya",
"Bria",
"Adele",
"Aubrielle",
"Tiffany",
"Addyson",
"Kai",
"Bellamy",
"Leilany",
"Princess",
"Chana",
"Estelle",
"Selene",
"Sky",
"Dani",
"Thalia",
"Ellen",
"Rivka",
"Amelie",
"Andi",
"Kynlee",
"Raina",
"Vienna",
"Alianna",
"Livia",
"Madalyn",
"Mercy",
"Novalee",
"Ramona",
"Vada",
"Berkley",
"Gwen",
"Persephone",
"Milena",
"Paula",
"Clare",
"Kairi",
"Linda",
"Paulina",
"Kamilah",
"Amoura",
"Hunter",
"Isabela",
"Karen",
"Marianna",
"Sariyah",
"Theodora",
"Annika",
"Kyleigh",
"Nellie",
"Scarlette",
"Keyla",
"Kailey",
"Mavis",
"Lilianna",
"Rosalyn",
"Sariah",
"Tori",
"Yareli",
"Aubriella",
"Bexley",
"Bailee",
"Jianna",
"Keily",
"Annabella",
"Azariah",
"Denisse",
"Promise",
"August",
"Hadlee",
"Halle",
"Fallon",
"Oakleigh",
"Zaria",
"Jaylin",
"Paisleigh",
"Crystal",
"Ila",
"Aliya",
"Cynthia",
"Giana",
"Maleah",
"Rylan",
"Aniya",
"Denise",
"Emmeline",
"Scout",
"Simone",
"Noah",
"Zora",
"Meghan",
"Landry",
"Ainhoa",
"Lilyana",
"Noor",
"Belen",
"Brynleigh",
"Cleo",
"Meilani",
"Karter",
"Amaris",
"Frida",
"Iliana",
"Violeta",
"Addisyn",
"Nancy",
"Denver",
"Leanna",
"Braylee",
"Kiana",
"Wrenley",
"Barbara",
"Khalani",
"Aspyn",
"Ellison",
"Judith",
"Robin",
"Valery",
"Aila",
"Deborah",
"Cara",
"Clarissa",
"Iyla",
"Lexie",
"Anais",
"Kaylie",
"Nathalie",
"Alisson",
"Della",
"Addilynn",
"Elsa",
"Zoya",
"Layne",
"Marlowe",
"Jovie",
"Kenia",
"Samira",
"Jaylee",
"Jenesis",
"Etta",
"Shay",
"Amayah",
"Avayah",
"Egypt",
"Flora",
"Raquel",
"Whitney",
"Zola",
"Giavanna",
"Raya",
"Veda",
"Halo",
"Paloma",
"Nataly",
"Whitley",
"Dalary",
"Drew",
"Guadalupe",
"Kamari",
"Esperanza",
"Loretta",
"Malayah",
"Natasha",
"Stormi",
"Ansley",
"Carolyn",
"Corinne",
"Paola",
"Brittany",
"Emerald",
"Freyja",
"Zainab",
"Artemis",
"Jillian",
"Kimora",
"Zoie",
"Aislinn",
"Emmaline",
"Ayleen",
"Queen",
"Jaycee",
"Murphy",
"Nyomi",
"Elina",
"Hadleigh",
"Marceline",
"Marisol",
"Yasmin",
"Zendaya",
"Chandler",
"Emani",
"Jaelynn",
"Kaiya",
"Nathalia",
"Violette",
"Joyce",
"Paityn",
"Elisabeth",
"Emmalynn",
"Luella",
"Yamileth",
"Aarya",
"Luisa",
"Zhuri",
"Araceli",
"Harleigh",
"Madalynn",
"Melani",
"Laylani",
"Magdalena",
"Mazikeen",
"Belle",
"Kadence",
		};

		// game names

		private static string[] first_names = new string[]
		{
		"澪",
		"愛乃",
		"愛莉",
		"亜葵",
		"明日香",
		"綾",
		"彩華",
		"ありさ",
		"伊織",
		"いくみ",
		"苺",
		"麗",
		"絵美香",
		"恵実里",
		"乙女",
		"楓",
		"香里",
		"香澄",
		"一紗",
		"和美",
		"佳奈",
		"夏鈴",
		"桔梗",
		"霧香",
		"心",
		"冴香",
		"咲菜",
		"咲耶",
		"彩世",
		"鈴",
		"千奈",
		"千沙登",
		"月美",
		"翼",
		"敏美",
		"友佳",
		"奈央",
		"渚沙",
		"菜月",
		"鳴海",
		"音々",
		"乃亜",
		"花",
		"早瀬",
		"陽彩子",
		"一見",
		"風歌",
		"星見",
		"穂乃香",
		"摩衣",
		"舞優",
		"茉莉",
		"未央",
		"実樹",
		"美紅",
		"翠里",
		"美優",
		"睦月",
		"萌香",
		"桃",
		"弥生",
		"唯奈",
		"雪音",
		"夢華",
		"百合子",
		"好美",
		"藍",
		"莉亜",
		"里穂",
		"留依",
		"怜湖",
		"さつき",
		"里実",
		"泉",
		"ひなた"
		};

		private static string[] first_names_en = new string[]
		{
		"Mary",
		"Patricia",
		"Linda",
		"Barbara",
		"Elizabeth",
		"Jennipher",
		"Maria",
		"Margaret",
		"Dorothy",
		"Lisa",
		"Madison",
		"Hannah",
		"Samantha",
		"Ashley",
		"Emma",
		"Olivia",
		"Ava",
		"Sophia",
		"Isabella",
		"Mia",
		"Charlotte",
		"Abigail",
		"Emily",
		"Harper",
		"Amelia",
		"Evelyn",
		"Harper"
		};

		private static string[] last_names = new string[]
	{
		"赤木",
		"青柳",
		"菊池",
		"栗山",
		"栗田",
		"桑原",
		"朝比奈",
		"五十嵐",
		"伊集院",
		"朝日奈",
		"和泉",
		"一文字",
		"岸",
		"今井",
		"栗原",
		"高野",
		"永野",
		"飯田",
		"加藤",
		"神楽坂",
		"篠原",
		"白石",
		"岩崎",
		"園田",
		"白鳥",
		"岩根",
		"瀬田",
		"橘",
		"谷山",
		"千原",
		"月城",
		"伊東",
		"稲葉",
		"菅原",
		"中野",
		"西村",
		"高畑",
		"渡辺",
		"山本",
		"伊吹",
		"内海",
		"一条",
		"一ノ瀬",
		"井上",
		"宮本",
		"柿崎",
		"橋本",
		"高瀬",
		"関口",
		"木村",
		"前原",
		"黒木",
		"水無月",
		"神崎",
		"新井",
		"五月女",
		"早乙女",
		"桂木",
		"深見",
		"桜井",
		"西園寺",
		"七瀬",
		"浅井",
		"飯島",
		"近藤",
		"臼井",
		"関谷",
		"永井",
		"竹田",
		"熊谷",
		"山田",
		"田邉",
		"濱尾",
		"佐々木",
		"勅使河原"
	};

		private static string[] last_names_en = new string[]
		{
		"Smith",
		"Johnson",
		"William",
		"Jones",
		"Brown",
		"Davis",
		"Miller",
		"Willson",
		"Moore",
		"Taylor",
		"Martinez",
		"Anderson",
		"Taylor",
		"Thomas",
		"Hernandez",
		"Moore",
		"Martin",
		"Jackson",
		"Thompson",
		"White",
		"Lopez",
		"Harris",
		"Clark",
		"Walker",
		"Gonzalez",
		"Allen",
		"Wright"
		};
		
		private static string[] player_names = new string[]
		{
		"翔太",
		"蓮",
		"翔",
		"陸",
		"颯太",
		"悠斗",
		"大翔",
		"樹",
		"翼",
		"大和",
		"奏太",
		"大輝",
		"悠",
		"隼人",
		"大輔",
		"健太",
		"駿",
		"優",
		"陽斗",
		"悠人",
		"陽",
		"誠",
		"仁",
		"拓海",
		"悠真",
		"悠太",
		"健",
		"大地",
		"遼",
		"大樹",
		"諒",
		"太一",
		"響",
		"一郎",
		"亮",
		"優斗",
		"海斗",
		"颯",
		"亮太",
		"匠",
		"陽太",
		"航",
		"瑛太",
		"直樹",
		"光",
		"空",
		"太郎",
		"輝",
		"一輝",
		"蒼",
		"剛",
		"拓真",
		"涼太",
		"潤",
		"達也",
		"葵",
		"龍之介",
		"聡",
		"健太郎",
		"隼",
		"悠生",
		"太陽",
		"雄大",
		"奏",
		"啓太",
		"陽向",
		"一樹",
		"楓",
		"涼",
		"悠希",
		"陸斗",
		"勇人",
		"蒼太",
		"拓哉",
		"新",
		"和也",
		"淳",
		"優希",
		"虎太郎",
		"悠翔",
		"和真",
		"拓也",
		"渉",
		"蒼空",
		"優太",
		"伊織",
		"智也",
		"歩",
		"直人",
		"大智",
		"司",
		"俊介",
		"優真",
		"晃",
		"琉生",
		"巧",
		"慧",
		"碧",
		"大",
		"凌",
		"諒太",
		"一",
		"陽翔",
		"湊",
		"優輝",
		"真",
		"一真",
		"颯真",
		"聡太",
		"日向",
		"哲也",
		"陽大",
		"慶太",
		"朝陽",
		"怜",
		"大貴",
		"徹",
		"拓実",
		"学",
		"漣",
		"翔大",
		"凛",
		"雄太",
		"海",
		"陽仁",
		"優人",
		"遥斗",
		"幹太",
		"凛太郎",
		"慶",
		"颯汰",
		"優汰",
		"大介",
		"龍",
		"晴",
		"颯人",
		"大雅",
		"聖",
		"伊吹",
		"心",
		"豊",
		"圭吾",
		"健人",
		"俊輔",
		"和輝",
		"航太",
		"櫂",
		"海翔",
		"至恩",
		"初珠",
		"桜深",
		"澄海",
		"九珠",
		"頼音",
		"今鹿",
		"碧",
		"歩木鈴",
		"枕鈴",
		"車車車",
		"金銀",
		"振門体",
		"世歩玲",
		"射夢",
		"冬",
		"愛人",
		"乃絵瑠",
		"新一",
		"燃志",
		"野生",
		"騎士",
		"黄熊",
		"精飛愛",
		"万華",
		"望己利",
		"聖帝"
		};

		private static string[] player_names_en = new string[]
		{
		"James",
		"John",
		"Robert",
		"David",
		"Richard",
		"Charles",
		"Joseph",
		"Thomas",
		"Jacob",
		"Joshua",
		"Matthew",
		"Ethan",
		"Andrew",
		"Daniel",
		"Anthony",
		"Christopher",
		"Joseph",
		"Noah",
		"Liam",
		"William",
		"Mason",
		"James",
		"Benjamin",
		"Jacob",
		"Michael",
		"Elijah",
		"Ethan",
		"Logan",
		"Oliver",
		"Alexander"
		};
	}
}