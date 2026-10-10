using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using Follow_Extremist.Core;

namespace Follow_Extremist.Code.Services
{
    public class ThermalPrintService
    {
        private PrintDocument printDoc;
        private AttendanceLog currentLog;
        private ElementInfo currentElement;
        private ElementWantedStatus currentWantedStatus;
        private PrintSetting printSetting;

        public ThermalPrintService()
        {
            printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        public void PrintAttendanceSlip(
            AttendanceLog log,
            ElementInfo element,
            ElementWantedStatus wantedStatus,
            PrintSetting settings,
            bool isPreview = false)
        {
            if (log == null || element == null) return;

            currentLog = log;
            currentElement = element;
            currentWantedStatus = wantedStatus;
            printSetting = settings ?? new PrintSetting();

            try
            {
                // Configure printer
                if (!string.IsNullOrEmpty(printSetting.PrinterName))
                {
                    printDoc.PrinterSettings.PrinterName = printSetting.PrinterName;
                }

                // Check paper width
                if (printSetting.PrinterType == "Thermal")
                {
                    int widthHundredsInch = printSetting.PaperWidthMm == 58 ? 228 : 315; // 58mm ~ 2.28", 80mm ~ 3.15"
                    printDoc.DefaultPageSettings.PaperSize = new PaperSize("CustomThermal", widthHundredsInch, 1200);
                }
                else
                {
                    // Laser A4
                    printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                }

                printDoc.PrinterSettings.Copies = (short)Math.Max(1, printSetting.PrintCopies);

                if (isPreview)
                {
                    using (PrintPreviewDialog previewDlg = new PrintPreviewDialog())
                    {
                        previewDlg.Document = printDoc;
                        previewDlg.RightToLeft = RightToLeft.Yes;
                        previewDlg.RightToLeftLayout = true;
                        previewDlg.WindowState = FormWindowState.Maximized;
                        previewDlg.ShowDialog();
                    }
                }
                else
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء الطباعة: {ex.Message}", "خطأ في الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            bool isThermal = printSetting?.PrinterType == "Thermal";
            float pageWidth = isThermal ? (printSetting.PaperWidthMm == 58 ? 210 : 280) : e.PageBounds.Width - 80;
            float marginX = isThermal ? 10 : 40;
            float currentY = 15;

            // Formats
            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat rtlFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionRightToLeft };
            StringFormat ltrFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            // Fonts
            using Font titleFont = new Font("Arial", isThermal ? 14 : 18, FontStyle.Bold);
            using Font subTitleFont = new Font("Arial", isThermal ? 10 : 13, FontStyle.Bold);
            using Font labelFont = new Font("Arial", isThermal ? 8.5f : 11, FontStyle.Regular);
            using Font boldFont = new Font("Arial", isThermal ? 9.5f : 12, FontStyle.Bold);
            using Font alertFont = new Font("Arial", isThermal ? 15 : 22, FontStyle.Bold);
            using Font smallFont = new Font("Arial", isThermal ? 7.5f : 9.5f, FontStyle.Regular);

            // Pens & Brushes
            using Pen solidPen = new Pen(Color.Black, 1.5f);
            using Pen dashedPen = new Pen(Color.Black, 1f) { DashStyle = DashStyle.Dash };
            using Pen alertPen = new Pen(Color.Black, 3f);

            bool isWanted = currentWantedStatus != null && currentWantedStatus.IsWanted;

            // 1. Header (حضور متابعة - NO LOGO)
            string headerText = !string.IsNullOrEmpty(printSetting?.HeaderText) ? printSetting.HeaderText : "حضور متابعة";
            g.DrawString(headerText, titleFont, Brushes.Black, new RectangleF(marginX, currentY, pageWidth, 28), centerFormat);
            currentY += 32;

            // Date & Time
            string dateStr = currentLog.AttendanceDateTime.ToString("yyyy/MM/dd  -  hh:mm tt");
            g.DrawString(dateStr, smallFont, Brushes.Black, new RectangleF(marginX, currentY, pageWidth, 18), centerFormat);
            currentY += 22;

            // Divider Line
            g.DrawLine(dashedPen, marginX, currentY, marginX + pageWidth, currentY);
            currentY += 10;

            // 2. WANTED ALERT BANNER (If Wanted)
            if (isWanted)
            {
                float bannerHeight = isThermal ? 45 : 60;
                RectangleF alertBox = new RectangleF(marginX + 5, currentY, pageWidth - 10, bannerHeight);
                g.FillRectangle(Brushes.White, alertBox);
                g.DrawRectangle(alertPen, alertBox.X, alertBox.Y, alertBox.Width, alertBox.Height);

                g.DrawString("!!! مــطــلــوب !!!", alertFont, Brushes.Black, alertBox, centerFormat);
                currentY += bannerHeight + 10;

                if (!string.IsNullOrEmpty(currentWantedStatus.WantedReason))
                {
                    g.DrawString($"السبب: {currentWantedStatus.WantedReason}", boldFont, Brushes.Black,
                        new RectangleF(marginX, currentY, pageWidth, 20), rtlFormat);
                    currentY += 22;
                }
                if (!string.IsNullOrEmpty(currentWantedStatus.WantedBy))
                {
                    g.DrawString($"الجهة الطالبة: {currentWantedStatus.WantedBy}", boldFont, Brushes.Black,
                        new RectangleF(marginX, currentY, pageWidth, 20), rtlFormat);
                    currentY += 22;
                }

                g.DrawLine(dashedPen, marginX, currentY, marginX + pageWidth, currentY);
                currentY += 10;
            }

            // 3. Element Details Table / Fields
            void DrawField(string label, string value, bool isHighlight = false)
            {
                float fieldH = isThermal ? 22 : 28;
                Font valueFont = isHighlight ? boldFont : labelFont;

                // Label on Right, Value on Left
                float labelWidth = pageWidth * 0.40f;
                float valueWidth = pageWidth * 0.60f;

                RectangleF labelRect = new RectangleF(marginX + valueWidth, currentY, labelWidth, fieldH);
                RectangleF valRect = new RectangleF(marginX, currentY, valueWidth, fieldH);

                g.DrawString(label + ":", boldFont, Brushes.Black, labelRect, rtlFormat);
                g.DrawString(value ?? "-", valueFont, Brushes.Black, valRect, rtlFormat);

                currentY += fieldH;
            }

            DrawField("رقم العنصر", currentElement.Id.ToString());
            DrawField("اسم العنصر", currentElement.ElementName, true);

            if (printSetting.ShowNationalId && !string.IsNullOrEmpty(currentElement.NationalId))
            {
                DrawField("الرقم القومي", currentElement.NationalId);
            }

            if (!string.IsNullOrEmpty(currentElement.Job))
            {
                DrawField("المهنة", currentElement.Job);
            }

            DrawField("نوع التحقق", currentLog.VerifyType == 1 ? "بصمة أصبع" : "تسجيل نظام");

            // Divider Line
            currentY += 5;
            g.DrawLine(solidPen, marginX, currentY, marginX + pageWidth, currentY);
            currentY += 10;

            // 4. NEXT FOLLOW DATE (ONLY IF NOT WANTED!)
            if (!isWanted)
            {
                if (printSetting.ShowNextFollowDate && currentLog.NextFollowDateAssigned.HasValue)
                {
                    float boxHeight = isThermal ? 55 : 70;
                    RectangleF nextDateBox = new RectangleF(marginX + 5, currentY, pageWidth - 10, boxHeight);
                    g.DrawRectangle(solidPen, nextDateBox.X, nextDateBox.Y, nextDateBox.Width, nextDateBox.Height);

                    g.DrawString("موعد المتابعة القادم", subTitleFont, Brushes.Black,
                        new RectangleF(nextDateBox.X, nextDateBox.Y + 4, nextDateBox.Width, 20), centerFormat);

                    string nextDateStr = currentLog.NextFollowDateAssigned.Value.ToString("yyyy / MM / dd");
                    string dayName = currentLog.NextFollowDateAssigned.Value.ToString("dddd", new System.Globalization.CultureInfo("ar-EG"));

                    g.DrawString($"{dayName}  {nextDateStr}", boldFont, Brushes.Black,
                        new RectangleF(nextDateBox.X, nextDateBox.Y + 24, nextDateBox.Width, 24), centerFormat);

                    currentY += boxHeight + 10;
                }
            }
            else
            {
                // WANTED WARNING TEXT (No next follow date shown!)
                float warnBoxH = isThermal ? 40 : 50;
                RectangleF warnRect = new RectangleF(marginX + 5, currentY, pageWidth - 10, warnBoxH);
                g.DrawRectangle(alertPen, warnRect.X, warnRect.Y, warnRect.Width, warnRect.Height);

                g.DrawString("يرجى مراجعة إدارة المتابعة فوراً", boldFont, Brushes.Black, warnRect, centerFormat);
                currentY += warnBoxH + 10;
            }

            // 5. Barcode (Simple Code 128 / Code 39 Style graphic bars)
            if (printSetting.ShowBarcode)
            {
                string codeText = !string.IsNullOrEmpty(currentElement.NationalId)
                    ? currentElement.NationalId
                    : $"LOG-{currentLog.Id:D6}";

                DrawSimpleBarcode(g, codeText, marginX + (pageWidth - 160) / 2, currentY, 160, isThermal ? 28 : 40);
                currentY += (isThermal ? 32 : 44);

                g.DrawString(codeText, smallFont, Brushes.Black, new RectangleF(marginX, currentY, pageWidth, 15), centerFormat);
                currentY += 18;
            }

            // 6. Footer Text
            if (!string.IsNullOrEmpty(printSetting.FooterText))
            {
                g.DrawLine(dashedPen, marginX, currentY, marginX + pageWidth, currentY);
                currentY += 6;

                g.DrawString(printSetting.FooterText, smallFont, Brushes.Black,
                    new RectangleF(marginX, currentY, pageWidth, 30), centerFormat);
                currentY += 32;
            }

            // Cut feed space for thermal
            if (isThermal)
            {
                currentY += 20;
            }

            e.HasMorePages = false;
        }

        private void DrawSimpleBarcode(Graphics g, string text, float x, float y, float width, float height)
        {
            if (string.IsNullOrEmpty(text)) return;

            // Pseudorandom pseudo-Code128 visual barcode pattern based on string hash for high readability
            int barsCount = Math.Min(text.Length * 7, 70);
            float barW = width / (barsCount * 1.5f);

            float curX = x;
            int hash = Math.Abs(text.GetHashCode());

            for (int i = 0; i < barsCount; i++)
            {
                bool isBar = ((hash >> (i % 31)) & 1) == 1 || (i % 3 == 0);
                float thickness = (i % 4 == 0) ? barW * 2f : barW;

                if (isBar)
                {
                    g.FillRectangle(Brushes.Black, curX, y, thickness, height);
                }
                curX += thickness + barW;
                if (curX > x + width) break;
            }
        }
    }
}
