using Follow_Extremist.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Follow_Extremist.Code
{
  public static class MessageCollections
    {
        // Message 
        public static void ShowEmptyDataMessage()
        {
            MessageBox.Show(Resources.EmptyMessageText, Resources.EmptyMessageCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorServer()
        {
            MessageBox.Show(Resources.ServerErrorText, Resources.ServerErrorCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowFieldsRequired()
        {
            MessageBox.Show(Resources.FieldRequiredText, Resources.FieldRequiredCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowRequiredDeleteRow()
        {
            MessageBox.Show(Resources.ShowRequiredDeleteFieldText, Resources.ShowRequiredDeleteFieldCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // Dialog 
        public static bool ShowDeletDialog()
        {
            var result = MessageBox.Show(Resources.DeletDialogText, Resources.DeleteDialogCaption,
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Notification 
        public static void ShoAddNotification()
        {
            Gui.GuiNotification.NotificationForm notificationForm = new Gui.GuiNotification.NotificationForm();
            notificationForm.labelTitle.Text = "تمت عملية الاضافة بنجاح";
            notificationForm.Show();
        }

        public static void ShowUpdateNotification()
        {
            Gui.GuiNotification.NotificationForm notificationForm = new Gui.GuiNotification.NotificationForm();
            notificationForm.labelTitle.Text = "تمت عملية التعديل بنجاح";
            notificationForm.Show();
        }
        public static void ShowDeleteNotification()
        {
            Gui.GuiNotification.NotificationForm notificationForm = new Gui.GuiNotification.NotificationForm();
            notificationForm.labelTitle.Text = "تمت عملية الحذف بنجاح";
            notificationForm.Show();
        }
    }
}
