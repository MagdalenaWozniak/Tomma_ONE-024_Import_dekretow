using Hydra;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

[assembly: CallbackAssemblyDescription(Name: "ONE-024 Import dekretów",
                                           Description: "Excel -> Dekrety",
                                           Author: "OneSupport",
                                           Version: "1.0",
                                           SystemVersion: "2022.0.2",
                                           Created: "12-05-2023")]
namespace Przycisk
{
    [SubscribeProcedure(Procedures.DZKLista, "Excel->Dekrety")]
    public class DodajFakture : Callback
    {
        private ClaWindow RapButton;
        private ClaWindow zakladkaTabWewnetrzne;
        private ClaWindow filtrBtn;
        public override void Init()
        {
            AddSubscription(true, 0, Events.OpenWindow, new TakeEventDelegate(OnOpenWindow));
            AddSubscription(false, 0, Events.ResizeWindow, new TakeEventDelegate(Resize));
        }



        private bool OnOpenWindow(Procedures ProcId, int ControlId, Events Event)
        {

            ClaWindow Parent = GetWindow();
            Rectangle WndBnd = GetWindow().Bounds;
            //zakladkaTabWewnetrzne = GetWindow().AllChildren["?TabFS"];
            RapButton = GetWindow().Children.Add(ControlTypes.button);
            RapButton.Visible = true;
            RapButton.TextRaw = "XLS";
            RapButton.ToolTipRaw = "Dodaj Dekret";
            RapButton.OnAfterMouseDown += new TakeEventDelegate(BtnOnMouseDown);

            filtrBtn = GetWindow().AllChildren["?insertPK"];                            //pobiera współrzędne przycisku zapisz
            RapButton.Bounds = new Rectangle(filtrBtn.Bounds.X - 20, filtrBtn.Bounds.Y, 17, 15);
            //listID = GetWindow().AllChildren["?Handlowe"].Id;      //tu sprawdzic

            //pobrane z pola w zakładce Raporty
            return true;
        }

        public bool Resize(Procedures ProcId, int ControlId, Events Event)
        {
            filtrBtn = GetWindow().AllChildren["?insertPK"];                            //pobiera współrzędne przycisku zapisz
            RapButton.Bounds = new Rectangle(filtrBtn.Bounds.X-20, filtrBtn.Bounds.Y , 17, 15);
            return true;
        }

        private bool BtnOnMouseDown(Procedures ProcId, int ControlId, Events Event)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 16) == "ERPXL_Tomma_test")
            {
                // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA_test", "Komunikat",
                //MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportDekretow\\test\\ImportDekretow.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 15) == "ERPXL_TOMMA-ERP")
            {
               // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
               // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportDekretow\\Produkcja\\ImportDekretow.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 18) == "ERPXL_BONUS_2_test")
            {
                // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportDekretow\\Bonus_test\\ImportDekretow.exe");
            }
            else if (Runtime.ConfigurationDictionary.Baza.ToString().Substring(0, 13) == "ERPXL_BONUS_2")
            {
                // MessageBox.Show("Po wybraniu pliku nastąpi dodawanie danych do bazy ERPXL_TOMMA-ERP", "Komunikat",
                // MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("C:\\OneSupport\\ImportDekretow\\Bonus_Produkcja\\ImportDekretow.exe");
            }
            else
            {
               MessageBox.Show("Dla bazy "+ Runtime.ConfigurationDictionary.Baza.ToString()+" dodatek nie posiada funkcjonalności", "Komunikat",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            return true;
        }
        #region
        public override void Cleanup()
        {
        }
    }
}

#endregion