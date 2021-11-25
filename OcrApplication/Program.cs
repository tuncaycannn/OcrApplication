using IronOcr;
using System;
using System.Linq;

namespace OcrApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\user\Desktop\FindeksRapor_15092021.pdf"; //file address

            var ocr = new IronTesseract();
            ocr.Configuration.BlackListCharacters = "`ë|^";
            ocr.Configuration.PageSegmentationMode = TesseractPageSegmentationMode.Auto;
            ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
            ocr.Configuration.EngineMode = TesseractEngineMode.TesseractAndLstm;
            ocr.Language = OcrLanguage.TurkishBest;
            ocr.AddSecondaryLanguage(OcrLanguage.EnglishBest);


            using (var input = new OcrInput(filePath))
            {
                OcrResult result = ocr.Read(input);


                int pageNumber = result.Pages.Count(); 

                for (int i = 2; i <= pageNumber; i++)
                {
                    if (result.Pages[i].PdfExtractedText.Contains("Grup") == true)
                    {
                        int blocks = result.Pages[i].Blocks.Count();
                        for (int a = 0; a < blocks; a++)
                        {
                            if (result.Pages[i].Blocks[a].Text.Contains("Grup") == true)
                            {
                                int lines = result.Pages[i].Blocks[a].Lines.Count();
                                Console.Write("\r\n\r\n\r\n\r\n");
                                for (int p = 1; p < lines; p++)
                                {
                                    if (p == 7 || p == 13)
                                    {
                                        Console.Write("\r\n\r\n\r\n\r\n");
                                    }
                                    string bank = result.Pages[i].Blocks[a].Lines[p].Text;
                                    Console.WriteLine(bank);
                                }
                            }
                        }
                    }
                }
                Console.ReadKey();
            }
        }
    }

}

