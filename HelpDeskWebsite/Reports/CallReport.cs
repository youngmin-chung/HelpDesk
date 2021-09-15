using System;
using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using HelpDeskViewModel;

namespace HelpDeskWebsite.Reports
{
    public class CallReport
    {
        static Font catFont = new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD);
        static Font subFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD);
        static Font smallFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        static string mappedPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        static string IMG = "img/small_helpdesk.jpg";

        public void doIt()
        {
            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream(mappedPath + "Pdfs/Call.pdf", FileMode.Create));
                document.Open();
                Paragraph para = new Paragraph();
                Image image1 = Image.GetInstance(mappedPath + IMG);
                image1.SetAbsolutePosition(50f, 660f);
                para.Add(image1);
                para.Alignment = Element.ALIGN_RIGHT;
                // Lets write a big header
                addEmptyLine(para, 6);
                Paragraph mainHead = new Paragraph(String.Format("{0,8}", "Calls"), catFont);
                mainHead.Alignment = Element.ALIGN_CENTER;
                para.Add(mainHead);
                addEmptyLine(para, 1);

                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 100.00F;
                table.AddCell(addCell("Opened", "h"));
                table.AddCell(addCell("Lastname", "h"));
                table.AddCell(addCell("Tech", "h"));
                table.AddCell(addCell("Problem", "h"));
                table.AddCell(addCell("Status", "h"));
                table.AddCell(addCell("Closed", "h"));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                table.AddCell(addCell(" "));
                CallViewModel Call = new CallViewModel();
                List<CallViewModel> Calls = Call.GetAll();

                foreach (CallViewModel cal in Calls)
                {
                    string date = "";
                    table.AddCell(addCell(cal.DateOpened.ToShortDateString()));
                    table.AddCell(addCell(cal.EmployeeName));
                    table.AddCell(addCell(cal.TechName));
                    table.AddCell(addCell(cal.ProblemDescription));
                    var status = cal.OpenStatus ? "Open" : "Closed";
                    table.AddCell(addCell(status));
                    if (cal.OpenStatus)
                        date = "";
                    else
                        date = cal.DateClosed?.ToShortDateString();
                    table.AddCell(addCell(date));
                }

                para.Add(table);
                addEmptyLine(para, 6);
                para.Alignment = Element.ALIGN_CENTER;
                Paragraph footer = new Paragraph("Call report written on - " + DateTime.Now, smallFont);
                footer.Alignment = Element.ALIGN_CENTER;
                para.Add(footer);
                document.Add(para);
                document.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error " + ex.Message);
            }
        }

        private static void addEmptyLine(Paragraph paragragh, int number)
        {
            for (int i = 0; i < number; i++)
            {
                paragragh.Add(new Paragraph(" "));
            }
        }

        private PdfPCell addCell(string data, string celltype = "d")
        {
            PdfPCell cell;
            if (celltype == "h")
            {
                cell = new PdfPCell(new Phrase(data, smallFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }
            else
            {
                cell = new PdfPCell(new Phrase(data));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
            }
            return cell;
        }
    }
}