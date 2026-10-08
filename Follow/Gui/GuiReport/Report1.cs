using DevExpress.XtraReports.UI;
using Follow.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Follow.Gui.GuiReport
{
    public partial class Report1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Report1()
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
            xrTable1.FormattingRules.Add(evenRowRule);
            xrTable1.FormattingRules.Add(oddRowRule);
        }
        public void BindData(IEnumerable<ElementInfoView> data)
        {
            this.DataSource = data;
            xrTableCell2.DataBindings.Add("Text", null, "ElementName");
            xrTableCell1.DataBindings.Add("Text", null, "BirthDate", "{0:yyyy/MM/dd}"); // Format as needed
            xrTableCell11.DataBindings.Add("Text", null, "Address");
            xrTableCell12.DataBindings.Add("Text", null, "Notes");
            xrTableCell13.DataBindings.Add("Text", null, "DateFollowNow", "{0:yyyy/MM/dd}");
            xrTableCell14.DataBindings.Add("Text", null, "DateFollowNext", "{0:yyyy/MM/dd}");
        }
    }
}
