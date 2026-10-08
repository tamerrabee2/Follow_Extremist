using DevExpress.XtraReports.UI;
using Follow_Extremist.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Follow_Extremist.Gui.GuiReport.GuiReportElementInfo
{
    public partial class ReportElementInfo : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportElementInfo()
        {
            InitializeComponent();
            ApplyFormattingRules();
        }
        private void ApplyFormattingRules()
        {
            // Create a formatting rule for even rows
            var evenRowRule = new DevExpress.XtraReports.UI.FormattingRule
            {
                Name = "EvenRowRule",
                Condition = "[DataSource.CurrentRowIndex] % 2 == 0",
                Formatting = { BackColor = System.Drawing.Color.LightGray }
            };

            // Create a formatting rule for odd rows
            var oddRowRule = new DevExpress.XtraReports.UI.FormattingRule
            {
                Name = "OddRowRule",
                Condition = "[DataSource.CurrentRowIndex] % 2 != 0",
                Formatting = { BackColor = System.Drawing.Color.White }
            };

            // Add the formatting rules to the report's collection of formatting rules
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] { evenRowRule, oddRowRule });

            // Apply the formatting rules to the table rows
            foreach (XRTableRow row in xrTable2.Rows)
            {
                row.FormattingRules.Add(evenRowRule);
                row.FormattingRules.Add(oddRowRule);
            }
        }
        public void BindData(IEnumerable<ElementInfo> data)
        {
            this.DataSource = data;
            ElementName.DataBindings.Add("Text", null, "ElementName");
            BirthDate.DataBindings.Add("Text", null, "BirthDate", "{0:yyyy/MM/dd}"); // Format as needed
            Address.DataBindings.Add("Text", null, "Address");
            Notes.DataBindings.Add("Text", null, "Notes");
            DateFollowStart.DataBindings.Add("Text", null, "DateFollowNow", "{0:yyyy/MM/dd}");
            FollowDaysCount.DataBindings.Add("Text", null, "FollowDaysCount");
            DateFollowNow.DataBindings.Add("Text", null, "DateFollowNext", "{0:yyyy/MM/dd}");
        }
    }
}
