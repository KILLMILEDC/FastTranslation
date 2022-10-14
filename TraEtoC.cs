using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FT_Edited_version
{
    internal class TraEtoC
    {
        public string TraEN;
        public string TraCN;

        /****************************************
         *                                      *
         * Word                                 *
         *                                      *
         ****************************************
         *                                      *
         * TraEN                                *
         *                                      *
         ****************************************
         *                                      *
         * TraCN                                *
         *                                      *
         ****************************************/

        public string TtraEtoCEN(string text)
        {
            string prefix = "<Word>";
            string suffix = "</Word>";
            string cha = prefix + text + suffix;//Wait for retrieval
            string BasePath = AppDomain.CurrentDomain.BaseDirectory;//BasePath is:' FT Edited version\bin\Debug\net6.0-windows\'
            string UpLevelPath = BasePath.Substring(0, BasePath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));//UpLevelPath is:'FT Edited version'
            string DicPath = UpLevelPath + @"\resources\dictionaries\Dictionary.ltml";//DicPath is:'FT Edited version\resources\dictionaries\Dictionary.ltml

            if(!File.Exists(DicPath))
            {
                TraEN = "Can not find the Dictionary, please check the dictionary file.";
            }
            else 
            {
                StreamReader FileVersion = File.OpenText(DicPath);//Open Dicpath (MUST UTF-8)
                string version = FileVersion.ReadLine();//Returns the file row of data

                if(version == "<!ltml version=\"1.0\">")
                {
//MARK ONE **********
                    StreamReader file = File.OpenText(DicPath);
                    string sections;
                    while ((sections = file.ReadLine()) != null)
                    {
                        bool re = sections.IndexOf(cha, StringComparison.OrdinalIgnoreCase) >= 0;//If cha in sections ==> TRUE
                        if (re == true)
                        {
                            //TraEN = Regex.Match(sections, "(?<= <TraEN>).*?(?= </TraEN>)").Value;

                            //int strlength = sections.Length;
                            //TraCN = sections.Substring(sections.IndexOf("<TraCN>") + strlength, sections.IndexOf("</TraCN>") - sections.IndexOf("<TraCN>") - strlength);

                            int BeEN = sections.LastIndexOf("<TraEN>");
                            int AfEN = sections.LastIndexOf("</TraEN>");

                            int BeCN = sections.LastIndexOf("<TraCN>");
                            int AfCN = sections.LastIndexOf("</TraCN>");
                            TraEN = sections.Substring(BeEN + 7, AfEN - BeEN - 7);//Intercept specified field (EN)
                            TraCN = sections.Substring(BeCN + 7, AfCN - BeCN -7);//Intercept specified field (CN)
                        }
                        sections = file.ReadLine();
                        //if (re == false)
                        //{
                        //    TraEN = "The query failed, check the dictionary file.";
                        //    break;
                        //}
                    }
//MARK TWO **********
                }
            }
            return TraEN;
        }

        public string TtraEtoCCN(string text)
        {
            string prefix = "<Word>";
            string suffix = "</Word>";
            string cha = prefix + text + suffix;//Wait for retrieval
            string BasePath = AppDomain.CurrentDomain.BaseDirectory;//BasePath is:' FT Edited version\bin\Debug\net6.0-windows\'
            string UpLevelPath = BasePath.Substring(0, BasePath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));
                   //UpLevelPath = UpLevelPath.Substring(0, UpLevelPath.LastIndexOf('\\'));//UpLevelPath is:'FT Edited version'
            string DicPath = UpLevelPath + @"\resources\dictionaries\Dictionary.ltml";//DicPath is:'FT Edited version\resources\dictionaries\Dictionary.ltml

            if (!File.Exists(DicPath))
            {
                TraCN = "Can not find the Dictionary, please check the dictionary file.";
            }
            else
            {
                StreamReader FileVersion = File.OpenText(DicPath);//Open Dicpath (MUST UTF-8)
                string version = FileVersion.ReadLine();//Returns the file row of data

                if (version == "<!ltml version=\"1.0\">")
                {
                    StreamReader file = File.OpenText(DicPath);
                    string sections;
                    while ((sections = file.ReadLine()) != null)
                    {
                        bool re = sections.IndexOf(cha, StringComparison.OrdinalIgnoreCase) >= 0;
                        if (re == true)
                        {
                            //TraEN = Regex.Match(sections, "(?<= <TraEN>).*?(?= </TraEN>)").Value;

                            //int strlength = sections.Length;
                            //TraCN = sections.Substring(sections.IndexOf("<TraCN>") + strlength, sections.IndexOf("</TraCN>") - sections.IndexOf("<TraCN>") - strlength);

                            int BeEN = sections.LastIndexOf("<TraEN>");
                            int AfEN = sections.LastIndexOf("</TraEN>");

                            int BeCN = sections.LastIndexOf("<TraCN>");
                            int AfCN = sections.LastIndexOf("</TraCN>");
                            TraEN = sections.Substring(BeEN + 7, AfEN - BeEN - 7);//Intercept specified field (EN)
                            TraCN = sections.Substring(BeCN + 7, AfCN - BeCN - 7);//Intercept specified field (CN)
                        }
                        sections = file.ReadLine();
                        //if (re == false)
                        //{
                        //    TraCN = "The query failed, check the dictionary file.";
                        //    break;
                        //}
                    }
                }
            }
            return TraCN;
        }
    }
}
