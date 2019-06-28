using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Optimum
{
    static class Program
    {
        // Localization
        private static LocalizedText _localizedText;

        /// <summary>
        /// Localized text
        /// </summary>
        public static LocalizedText LocalizedText { get => _localizedText; set { _localizedText = value; LocalizationChanged(); } }

        /// <summary>
        /// Localization changed event
        /// </summary>
        public static event Action LocalizationChanged = () => {};

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Properties.Settings.Default.language == 0)
                LocalizedText = new LocalizedText();
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LocalizedText));
                using (MemoryStream ms = new MemoryStream(Properties.Resources.russian))
                {
                    LocalizedText = (LocalizedText)serializer.ReadObject(ms);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Optimum());
        }
    }
}
