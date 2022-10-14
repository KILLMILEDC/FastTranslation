using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_Edited_version
{
    internal class TraCtoE
    {
        public string Word;
        public string TraEN;

        /****************************************
         *                                      *
         * TraCN                                *
         *                                      *
         ****************************************
         *                                      *
         * Word                                 *
         *                                      *
         ****************************************
         *                                      *
         * TraEN                                *
         *                                      *
         ****************************************/

        public string TtraCtoEWord(string text)
        {
            string prefix = "<TraCN>";
            string suffix = "</TraCN>";
            string cha = prefix + text + suffix;//Wait for retrieval
            string BasePath = AppDomain.CurrentDomain.BaseDirectory;//BasePath is:' FT Edited version\bin\Debug\net6.0-windows\'
            string UpLevelPath = BasePath.Substring(0, BasePath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));//UpLevelPath is:'FT Edited version'
            string DicPath = UpLevelPath + @"\resources\dictionaries\Dictionary.ltml";//DicPath is:'FT Edited version\resources\dictionaries\Dictionary.ltml

            if (!File.Exists(DicPath))
            {
                Word = "Can not find the Dictionary, please check the dictionary file.";
            }
            else
            {
                StreamReader FileVersion = File.OpenText(DicPath);//Open Dicpath (MUST UTF-8)
                string version = FileVersion.ReadLine();//Returns the file row of data

                if (version == "<!ltml version=\"1.0\">")
                {
//MARK ONE **********
                    StreamReader file = File.OpenText(DicPath);
                    string sections;
                    while ((sections = file.ReadLine()) != null)
                    {
                        bool re = sections.IndexOf(cha, StringComparison.OrdinalIgnoreCase) >= 0;//If cha in sections ==> TRUE
                        if (re == true)
                        {
                            int BeW = sections.LastIndexOf("<Word>");
                            int AfW = sections.LastIndexOf("</Word>");

                            int BeEN = sections.LastIndexOf("<TraEN>");
                            int AfEN = sections.LastIndexOf("</TraEN>");
                            Word = sections.Substring(BeW + 6, AfW - BeW - 6);//Intercept specified field (Word)
                            TraEN = sections.Substring(BeEN + 7, AfEN - BeEN - 7);//Intercept specified field (EN)
                        }
                        sections = file.ReadLine();
                    }
//MARK TWO **********
                }
            }
            return Word;
        }

        public string TtraCtoEEN(string text)
        {
            string prefix = "<TraCN>";
            string suffix = "</TraCN>";
            string cha = prefix + text + suffix;//Wait for retrieval
            string BasePath = AppDomain.CurrentDomain.BaseDirectory;//BasePath is:' FT Edited version\bin\Debug\net6.0-windows\'
            string UpLevelPath = BasePath.Substring(0, BasePath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));//UpLevelPath is:'FT Edited version'
            string DicPath = UpLevelPath + @"\resources\dictionaries\Dictionary.ltml";//DicPath is:'FT Edited version\resources\dictionaries\Dictionary.ltml

            if (!File.Exists(DicPath))
            {
                TraEN = "Can not find the Dictionary, please check the dictionary file.";
            }
            else
            {
                StreamReader FileVersion = File.OpenText(DicPath);//Open Dicpath (MUST UTF-8)
                string version = FileVersion.ReadLine();//Returns the file row of data

                if (version == "<!ltml version=\"1.0\">")
                {
//MARK ONE **********
                    StreamReader file = File.OpenText(DicPath);
                    string sections;
                    while ((sections = file.ReadLine()) != null)
                    {
                        bool re = sections.IndexOf(cha, StringComparison.OrdinalIgnoreCase) >= 0;//If cha in sections ==> TRUE
                        if (re == true)
                        {
                            int BeW = sections.LastIndexOf("<Word>");
                            int AfW = sections.LastIndexOf("</Word>");

                            int BeEN = sections.LastIndexOf("<TraEN>");
                            int AfEN = sections.LastIndexOf("</TraEN>");
                            Word = sections.Substring(BeW + 7, AfW - BeW - 7);//Intercept specified field (Word)
                            TraEN = sections.Substring(BeEN + 7, AfEN - BeEN - 7);//Intercept specified field (EN)
                        }
                        sections = file.ReadLine();
                    }
//MARK TWO **********
                }
            }
            return TraEN;
        }
    }
}
