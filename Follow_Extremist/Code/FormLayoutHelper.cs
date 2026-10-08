using System;
using System.Drawing;
using System.Windows.Forms;

namespace Follow_Extremist.Code
{
    /// <summary>
    /// مساعد مركزي لضبط التخطيط والتناسب مع دقة الشاشة (DPI / Screen Resolution)
    /// </summary>
    public static class FormLayoutHelper
    {
        /// <summary>
        /// يُطبَّق على Forms الرئيسية والشاشات التي تعتمد على الجداول (GridControl) للتوسع الكامل
        /// </summary>
        public static void ApplyResponsiveLayout(Form form, bool startMaximized = true)
        {
            if (form == null) return;

            var screen = Screen.FromControl(form).WorkingArea;

            int minWidth  = Math.Min(900, (int)(screen.Width  * 0.6));
            int minHeight = Math.Min(550, (int)(screen.Height * 0.6));
            form.MinimumSize = new Size(minWidth, minHeight);

            if (startMaximized && form.WindowState != FormWindowState.Maximized)
            {
                form.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// يُطبَّق على Forms الحوارية (Dialogs) - يحافظ على التصميم المصمم مسبقاً ويتمركز في الشاشة
        /// ويتعامل بمرونة مع الشاشات الصغيرة دون خلق مساحات فارغة
        /// </summary>
        public static void ApplyDialogLayout(Form form)
        {
            if (form == null) return;

            var screen = Screen.FromControl(form).WorkingArea;

            // إذا كان الفورم أكبر من مساحة الشاشة الحالية (شاشات صغيرة أو DPI مرتفع)، نضبط الحجم ونفعل التمرير
            if (form.Width > screen.Width - 20 || form.Height > screen.Height - 30)
            {
                form.AutoScroll = true;
                form.AutoScrollMinSize = form.ClientSize;
                form.Size = new Size(
                    Math.Min(form.Width, screen.Width - 20),
                    Math.Min(form.Height, screen.Height - 30)
                );
            }

            form.StartPosition = FormStartPosition.CenterScreen;
            form.Location = new Point(
                screen.Left + Math.Max(0, (screen.Width - form.Width) / 2),
                screen.Top + Math.Max(0, (screen.Height - form.Height) / 2)
            );
        }

        /// <summary>
        /// زيادة التوافق: يتجاهل النسب الاصطناعية التي كانت تسبب فراغات كبيرة ويطبق التخطيط السليم
        /// </summary>
        public static void ApplyDialogLayout(Form form, double widthRatio, double heightRatio)
        {
            ApplyDialogLayout(form);
        }

        /// <summary>
        /// يُطبَّق على UserControls داخل الـ panelContainer - يجعلها Dock=Fill تلقائياً
        /// </summary>
        public static void ApplyUserControlLayout(UserControl control)
        {
            if (control == null) return;
            if (control.Dock != DockStyle.Fill)
            {
                control.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// يتحقق إذا كانت الشاشة الحالية أقل من 1280px عرض
        /// </summary>
        public static bool IsSmallScreen()
        {
            return Screen.PrimaryScreen?.WorkingArea.Width < 1280;
        }

        /// <summary>
        /// يُرجع معامل القياس للشاشة (DPI Scale Factor)
        /// </summary>
        public static float GetScaleFactor(Control control)
        {
            using var g = control.CreateGraphics();
            return g.DpiX / 96f;
        }
    }
}
