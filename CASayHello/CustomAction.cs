
using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;

namespace CASayHello
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult SayHello(Session session)
        {
            session.Log("Begin Custom Action SayHello");

            Record r = new Record(2);
            r.SetString(0, "[1][2]");
            r.SetString(1, "This dialog displayed via ");
            r.SetString(2, "session.Message(). \n\nWATCH OUT!\nNext message via MessageBox.Show()\nwill hide behind the install dialog!");
            session.Message((InstallMessage)((int)(InstallMessage.User) +
                                             (int)(MessageBoxButtons.OK)),
                                             r);

            /* Lesson Learned: Can't see the messagebox if you do it like this,
             * which is what was listed on p. 149 of the Nick Ramirez text! */
            Record invisible = new Record(0);
            invisible[0] = "You won't see me!  Must format as above!";
            session.Message(InstallMessage.Info, invisible);

            /* Lesson Learned: Must set DisplayInternalUI="yes" in the Bundle's MsiPackage
             *                 or you won't see anything, including the Custom Action popup dialog! */

            /* Lesson Learned: Can use MessageBox.Show()
             * but it HIDES BEHIND the install dialogs,
             * user won't notice it! */
            MessageBox.Show("This dialog displayed via MessageBox.Show()");

            return ActionResult.Success;
        }
    }
}
