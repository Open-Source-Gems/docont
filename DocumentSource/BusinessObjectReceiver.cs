
using Microsoft.AspNetCore.Components;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using System.Diagnostics;
using JsonFormatting = Newtonsoft.Json.Formatting;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.ConstrainedExecution;

namespace DocumentSource
{ 
    public class BusinessObjectReceiver
    {
        public static async Task GenerateReport(MemoryStream ms, Dictionary<string, object> args,
            Stream templateFileStream)
        {           

            Счет
                счет = args.ContainsKey("bobj") ? (Счет)args["bobj"] : null;
            Dictionary<string, object>?
                parameters = args.ContainsKey("prms") ? (Dictionary<string, object>)args["prms"] : null;

            using (WordDocument document = new WordDocument(templateFileStream, FormatType.Automatic))
                {
                    // Console.WriteLine("1) Opened!");
                    
                    if (счет != null)
                    {
                        Prepare(счет, parameters, document);
                    }

                    // Console.WriteLine("2) Modified! Saving...");

                    document.Save(ms, FormatType.Docx);

                    // Console.WriteLine("3) Saved!");

                }            
        }

        static void Prepare(Счет счет, Dictionary<string, object> parameters, WordDocument document)
        {
            WTable table;
            table = document.Sections[0].Tables[0] as WTable;

            WTableRow row;
            row = table.Rows[0];
            RowText(row, 1, $"{счет.Номер}");
            RowText(row, 3, $"{счет.Дата.ToString("dd.MM.yyyy")}");
            row = table.Rows[1];
            RowText(row, 1, $"«{счет.Поставщик}»");
            row = table.Rows[2];
            RowText(row, 1, $"{счет.Сумма} руб.");
            if(счет.Отмена) 
            {
                row = table.Rows[3];
                RowText(row, 1, $"СЧЁТ ОТМЕНЁН");
            }

            table = document.Sections[0].Tables[1] as WTable;
            //Clone the row.
            row = table.Rows[1];

            for (int j = 0; j < счет.Товары.Товар.Count; j++)
            {               
                var товар = счет.Товары.Товар[j];

                //Insert new paragraph to the first cell.
                RowText(row, 0, товар.Наименование);
                RowText(row, 1, товар.Количество.ToString());
                table.Rows.Add(row);
                row = row.Clone();
            }

        }

        static void RowText(WTableRow row, int cell, string text, bool clear = true)
        {
            WParagraph cellParagraph = row.Cells[cell].ChildEntities[0] as WParagraph;
            cellParagraph.Text = text;
        }

        /*
        public static async Task GenerateReportTest(
            MemoryStream ms, 
            Dictionary<string, object> args,
            string templateFilePath)
        {
            var di = templateFilePath ?? "";

            if (!File.Exists(di))
            {
              throw new Exception("Nonexistent file!");
            }
                        
            using (FileStream fi = new FileStream(di, FileMode.Open, FileAccess.ReadWrite))
            {
              GenerateReport(ms, args, fi);
            }
        }
        */

    }
}